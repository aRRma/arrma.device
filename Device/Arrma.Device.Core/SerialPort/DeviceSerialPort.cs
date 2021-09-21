using System;
using System.IO.Ports;
using System.Collections.Generic;
using System.Diagnostics;
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

        public static Dictionary<string, bool> ComPortsDictionary { get; }
        public bool IsConnected => _port?.IsOpen ?? false;
        public string PortName => _port?.PortName ?? "";

        static DeviceSerialPort()
        {
            ComPortsDictionary = new Dictionary<string, bool>();
            GetPortNames();
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

        public static void GetPortNames(ILogger logger = null)
        {
            try
            {
                string[] ports = System.IO.Ports.SerialPort.GetPortNames();
                Array.Sort(ports);
                foreach (var item in ports)
                    if (!ComPortsDictionary.ContainsKey(item))
                        ComPortsDictionary.Add(item, false);
                logger?.Information($"Find {ports.Length} com ports: {string.Join("; ", ports)}", LogSource.SERIAL_PORT);
            }
            catch (Exception ex)
            {
                logger?.Error("Error finding com ports.", LogSource.SERIAL_PORT, ex);
                return;
            }
        }

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
