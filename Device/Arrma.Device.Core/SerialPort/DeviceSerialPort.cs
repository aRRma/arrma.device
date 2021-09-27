using System;
using System.IO.Ports;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Arrma.Device.Interfaces.Logger;
using Arrma.Device.Enum;

namespace Arrma.Device.Core.SerialPort
{
    public class DeviceSerialPort : IDisposable
    {
        protected readonly System.IO.Ports.SerialPort _port;
        protected readonly SerialPortConfig _config;
        protected readonly ILogger _logger;

        private static readonly Timer _tmrGetPorts;

        /// <summary>
        /// Доступные в системе COM порты. Если порт true, то он уже занят устройством.
        /// </summary>
        public static Dictionary<string, bool> AvailableComPorts { get; }
        /// <summary>
        /// Статус подключения устройства
        /// </summary>
        public bool IsConnected => _port?.IsOpen ?? false;
        /// <summary>
        /// Название COM порта устройства
        /// </summary>
        public string PortName => _port?.PortName ?? "";

        static DeviceSerialPort()
        {
            AvailableComPorts = new Dictionary<string, bool>();
            _tmrGetPorts = new Timer(GetPortsCallback, null, 0, 5000);
        }

        public DeviceSerialPort()
        {
            _config = new SerialPortConfig();
            _port = new System.IO.Ports.SerialPort()
            {
                BaudRate = _config.BaudRate,
                DataBits = _config.DataBits,
                Parity = _config.Parity,
                StopBits = _config.StopBits,
                ReadTimeout = _config.ReadTimeout,
                WriteTimeout = _config.WriteTimeout
            };
        }
        public DeviceSerialPort(DeviceSerialPort port)
        {
            _port = port?._port ?? new System.IO.Ports.SerialPort();
            _config = port?._config ?? new SerialPortConfig();
            _logger = port?._logger;
        }
        public DeviceSerialPort(SerialPortConfig config, ILogger logger = null)
        {
            _config = config;
            _port = new System.IO.Ports.SerialPort()
            {
                PortName = _config.Name,
                BaudRate = _config.BaudRate,
                DataBits = _config.DataBits,
                Parity = _config.Parity,
                StopBits = _config.StopBits,
                ReadTimeout = _config.ReadTimeout,
                WriteTimeout = _config.WriteTimeout
            };
            _logger = logger;
        }

        /// <summary>
        /// Статический метод для заполнения словаря доступных в системе COM портов, а так-же установки флага свободен ли порт или нет.
        /// </summary>
        /// <param name="logger"></param>
        public static void GetPortNames(ILogger logger = null)
        {
            //TODO тут конкурентный доступ!!!
            try
            {
                string[] ports = System.IO.Ports.SerialPort.GetPortNames();
                Array.Sort(ports);
                // заполняем словарь новыми портами
                foreach (var item in ports)
                    if (!AvailableComPorts.ContainsKey(item))
                        AvailableComPorts.Add(item, false);
                // удаляем из словаря пропавшие порты
                foreach (var item in AvailableComPorts)
                    if (!ports.Contains(item.Key))
                        AvailableComPorts.Remove(item.Key);
                logger?.Information($"Find {ports.Length} com ports: {string.Join("; ", ports)}", LogSource.SERIAL_PORT);

                Debug.WriteLine(new string('=', 100) + "\n");
                Debug.WriteLine($"Find {ports.Length} com ports: {string.Join("; ", ports)}");
                Debug.WriteLine(new string('=', 100) + "\n");
            }
            catch (Exception ex)
            {
                logger?.Error("Error finding com ports.", LogSource.SERIAL_PORT, ex);

                Debug.WriteLine(new string('=', 100) + "\n");
                Debug.WriteLine("Error finding com ports.");
                Debug.WriteLine(new string('=', 100) + "\n");

                return;
            }
        }

        private static void GetPortsCallback(object? state) => GetPortNames();

        /// <summary>
        /// Подключится к устройству. Настройки COM порта берутся из конфига объекта.
        /// </summary>
        /// <returns></returns>
        public bool Connect()
        {
            if (string.IsNullOrWhiteSpace(_config?.Name) || (bool)!_config?.Name.StartsWith("COM"))
            {
                _logger?.Error($"Incorrect or empty com_port name: {_config?.Name}", LogSource.SERIAL_PORT);
                return false;
            }
            if (_port?.IsOpen == true)
            {
                _logger?.Error($"Try connect to busy com_port: {_config?.Name}", LogSource.SERIAL_PORT);
                return false;
            }

            Debug.Assert(_port != null, nameof(_port) + " != null");

            _port.PortName = _config.Name;
            _port.BaudRate = _config.BaudRate;
            _port.DataBits = _config.DataBits;
            _port.Parity = _config.Parity;
            _port.StopBits = _config.StopBits;
            _port.ReadTimeout = _config.ReadTimeout;
            _port.WriteTimeout = _config.WriteTimeout;

            lock (_port)
            {
                try
                {
                    _port.Open();
                    _logger?.Information($"Com port {_port.PortName} is open.", LogSource.SERIAL_PORT);
                    return true;

                }
                catch (UnauthorizedAccessException e)
                {
                    // Порт занят
                    _logger?.Error($"Com port {_port.PortName} is busy.", LogSource.SERIAL_PORT, e);
                    return false;
                }
                catch (ArgumentException e)
                {
                    // Неверная конфигурация порта
                    _logger?.Error($"Invalid com port {_port.PortName} configuration.", LogSource.SERIAL_PORT, e);
                    return false;
                }
                catch (Exception e)
                {
                    // Ошибка ввода-вывода
                    _logger?.Error($"Error opening com port {_port.PortName}.", LogSource.SERIAL_PORT, e);
                    return false;
                }
            }
        }
        /// <summary>
        /// Подключится к устройству на конкретном COM порту. Настройки COM порта берутся из конфига объекта. Название порта обновляется в конфиге объекта если удачно подключились.
        /// </summary>
        /// <param name="port"></param>
        /// <returns></returns>
        public bool Connect(string port)
        {
            if (string.IsNullOrWhiteSpace(_config?.Name) || (bool)!_config?.Name.StartsWith("COM"))
            {
                _logger?.Error($"Incorrect or empty com_port name: {_config?.Name}", LogSource.SERIAL_PORT);
                return false;
            }
            if (_port?.IsOpen == true)
            {
                _logger?.Error($"Try connect to busy com_port: {_config?.Name}", LogSource.SERIAL_PORT);
                return false;
            }

            Debug.Assert(_port != null, nameof(_port) + " != null");

            _port.PortName = port;
            _port.BaudRate = _config.BaudRate;
            _port.DataBits = _config.DataBits;
            _port.Parity = _config.Parity;
            _port.StopBits = _config.StopBits;
            _port.ReadTimeout = _config.ReadTimeout;
            _port.WriteTimeout = _config.WriteTimeout;

            lock (_port)
            {
                try
                {
                    _port.Open();
                    // upd config
                    _config.Name = _port.PortName;
                    _logger?.Information($"Com port {_port.PortName} is open.", LogSource.SERIAL_PORT);
                    return true;

                }
                catch (UnauthorizedAccessException e)
                {
                    // Порт занят
                    _logger?.Error($"Com port {_port.PortName} is busy.", LogSource.SERIAL_PORT, e);
                    return false;
                }
                catch (ArgumentException e)
                {
                    // Неверная конфигурация порта
                    _logger?.Error($"Invalid com port {_port.PortName} configuration.", LogSource.SERIAL_PORT, e);
                    return false;
                }
                catch (Exception e)
                {
                    // Ошибка ввода-вывода
                    _logger?.Error($"Error opening com port {_port.PortName}.", LogSource.SERIAL_PORT, e);
                    return false;
                }
            }
        }
        /// <summary>
        /// Подключится к устройству используя конфиг. Конфиг объекта обновляется если удачно подключились.
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        public bool Connect(SerialPortConfig config)
        {
            if (string.IsNullOrWhiteSpace(_config?.Name) || (bool)!_config?.Name.StartsWith("COM"))
            {
                _logger?.Error($"Incorrect or empty com_port name: {_config?.Name}", LogSource.SERIAL_PORT);
                return false;
            }
            if (_port?.IsOpen == true)
            {
                _logger?.Error($"Try connect to busy com_port: {_config?.Name}", LogSource.SERIAL_PORT);
                return false;
            }

            Debug.Assert(_port != null, nameof(_port) + " != null");

            _port.PortName = config.Name;
            _port.BaudRate = config.BaudRate;
            _port.DataBits = config.DataBits;
            _port.Parity = config.Parity;
            _port.StopBits = config.StopBits;
            _port.ReadTimeout = config.ReadTimeout;
            _port.WriteTimeout = config.WriteTimeout;

            lock (_port)
            {
                try
                {
                    _port.Open();
                    // upd config
                    _config.Name = config.Name;
                    _config.BaudRate = config.BaudRate;
                    _config.DataBits = config.DataBits;
                    _config.Parity = config.Parity;
                    _config.StopBits = config.StopBits;
                    _config.ReadTimeout = config.ReadTimeout;
                    _config.WriteTimeout = config.WriteTimeout;
                    _logger?.Information($"Com port {_port.PortName} is open.", LogSource.SERIAL_PORT);
                    return true;

                }
                catch (UnauthorizedAccessException e)
                {
                    // Порт занят
                    _logger?.Error($"Com port {_port.PortName} is busy.", LogSource.SERIAL_PORT, e);
                    return false;
                }
                catch (ArgumentException e)
                {
                    // Неверная конфигурация порта
                    _logger?.Error($"Invalid com port {_port.PortName} configuration.", LogSource.SERIAL_PORT, e);
                    return false;
                }
                catch (Exception e)
                {
                    // Ошибка ввода-вывода
                    _logger?.Error($"Error opening com port {_port.PortName}.", LogSource.SERIAL_PORT, e);
                    return false;
                }
            }
        }
        /// <summary>
        /// Отключиться от устройства. Имя порта сбрасывается на "" пустое.
        /// </summary>
        /// <returns></returns>
        public bool Disconnect()
        {
            if (_port?.IsOpen == false)
            {
                _logger?.Information($"Com port {_port.PortName} is already closed.", LogSource.SERIAL_PORT);
                return true;
            }

            Debug.Assert(_port != null, nameof(_port) + " != null");

            lock (_port)
            {
                try
                {
                    _port.Close();
                    _port.PortName = "";
                    _logger?.Information($"Com port {_port.PortName} is closed.", LogSource.SERIAL_PORT);
                    return true;
                }
                catch (Exception e)
                {
                    _logger?.Error($"Error close com port {_port.PortName}", LogSource.SERIAL_PORT, e);
                    return false;
                }
                finally
                {
                    _port.Dispose();
                }
            }
        }
        /// <summary>
        /// Переподключиться к устройству с текущим конфигом.
        /// </summary>
        /// <returns></returns>
        public async Task<bool> Reconnect()
        {
            try
            {
                lock (_port)
                    _port.Close();
                await Task.Delay(100);
                if (_port.IsOpen == true)
                    throw new Exception($"Com port {_port.PortName} is not closed. Error reconnect");
                lock (_port)
                    _port.Open();
                return true;
            }
            catch (Exception e)
            {
                _logger?.Error(e.Message, LogSource.SERIAL_PORT, e);
                return false;
            }
        }
        public void Dispose()
        {
            _port?.Dispose();
        }
    }
}
