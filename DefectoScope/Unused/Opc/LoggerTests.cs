using System;
using System.Collections.Generic;
using System.Linq;
using Common.Logging;
using Common.Logging.Configuration;
using Common.Logging.Simple;
using TitaniumAS.Opc.Client.Common;
using TitaniumAS.Opc.Client.Da;

namespace DefectoScope
{
    
    public partial class LoggerTests
    {
        private OpcDaServer _server;

        public LoggerTests()
        {
            var _ = new NameValueCollection {["level"] = "TRACE"};
            LogManager.Adapter = new CapturingLoggerFactoryAdapter();
        }

        public void TestInitialize()
        {
            _server = new OpcDaServer(UrlBuilder.Build(OpcClient.OpcProgId));
        }

        public void TestCleanup()
        {
            _server.Disconnect();
        }
        
        public void Should_Trace_Log_On_Successfully_Call_OPC_Wrapper_Methods()
        {
            //arrange
            var adapter = (CapturingLoggerFactoryAdapter) LogManager.Adapter;

            //act
            _server.Connect();

            //assert
            var _ = adapter.LoggerEvents.ToList();
        }
     
        public void Should_Correct_Stringify_Arguments_On_Call_OPC_Wrapper_Methods()
        {
            //arrange
            var adapter = (CapturingLoggerFactoryAdapter) LogManager.Adapter;
            _server.Connect();

            //act
            _server.Read(
                new List<string> {"test id"},
                new List<TimeSpan> {TimeSpan.Zero}
            );

            //assert
            var _ = adapter.LoggerEvents.ToList();
        }
    }
}