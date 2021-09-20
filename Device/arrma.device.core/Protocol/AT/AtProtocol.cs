using System.Collections.Generic;
using arrma.device.interfaces;

namespace arrma.device.core
{
    class AtProtocol : DeviceSerialPort, IProtocol
    {
        protected int SendAttempt { get; set; } = 2;

        private Dictionary<AtCommand, string> _commands;
        private Dictionary<AtCommandEnd, string> _commandsEnd;
        private Dictionary<AtModemAnswer, string> _modemAnswer;
        
        public AtProtocol(SerialPortConfig config, ILogger logger = null) : base(config, logger)
        {
            _commands = new Dictionary<AtCommand, string>()
            {
                { AtCommand.AT_, "AT\r" },
                { AtCommand.AT_E0, "ATE0\r" },
                { AtCommand.AT_S0, "ATS0=0\r" },
                { AtCommand.AT_CLIP, "AT+CLIP=1\r" },
                { AtCommand.AT_CMGF, "AT+CMGF=1\r" },
                { AtCommand.AT_CREG, "AT+CREG?\r" },
                { AtCommand.AT_CSQ, "AT+CSQ\r" },
                { AtCommand.AT_GMI, "AT+GMI\r" },
                { AtCommand.AT_GMM, "AT+GMM\r" },
                { AtCommand.AT_GMR, "AT+GMR\r" },
                { AtCommand.AT_CGSN, "AT+CGSN\r" },
                { AtCommand.AT_CCID, "AT+CCID\r" },
                { AtCommand.AT_COPS, "AT+COPS?\r" },
                { AtCommand.AT_CIMI, "AT+CIMI\r" },
                { AtCommand.AT_CUSD, "AT+CUSD=1," },
                { AtCommand.AT_REBOOT, "AT#REBOOT\r" },
                { AtCommand.AT_CPMS, "AT+CPMS?\r" },
                { AtCommand.AT_CMGL, "AT+CMGL=\"ALL\"\r" },
                { AtCommand.AT_CMGR, "AT+CMGR=1\r" },
                { AtCommand.AT_CMGD, "AT+CMGD=1,4\r" },
                { AtCommand.AT_CMGS, "AT+CMGS=" },
                { AtCommand.AT_H0, "ATH$\r" }
            };
            _commandsEnd = new Dictionary<AtCommandEnd, string>()
            {
                { AtCommandEnd.CR, "\r" },
                { AtCommandEnd.SUB, "\x1A" },
                { AtCommandEnd.SPACE, "\x20" },
                { AtCommandEnd.LF, "\n" },
                { AtCommandEnd.CRLF, "\r\n"},
            };
            _modemAnswer = new Dictionary<AtModemAnswer, string>()
            {
                { AtModemAnswer.NONE, "" },
                { AtModemAnswer.OK, "\r\nOK\r\n" },
                { AtModemAnswer.ERROR, "\r\nERROR\r\n" },
                { AtModemAnswer.ENTER_SMS, "\r\n>" },
            };
        }
        
        public bool SearchPort(IRequest request, out string name)
        {
            throw new System.NotImplementedException();
        }

        public IResponse SendCommand(IResponse response, int byteAnswer)
        {
            throw new System.NotImplementedException();
        }
    }
}
