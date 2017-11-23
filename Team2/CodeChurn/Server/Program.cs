using System;
using System.Threading;
using appcfg;

namespace Server
{
    class Program
    {
        #region Private methods

        static void Main(string[] args)
        {
            SetParameter(args);

            StartChurnLogServer();

            WaitForKeyPressToExit();
        }

        private static void SetParameter(string[] args)
        {
            var cfg = GetConfigFromArgs(args);
            _rootPath = cfg.root;
            _protocolFilePath = cfg.protocol;
            _intervall = (int) cfg.interval * 1000;
        }

        private static void StartChurnLogServer()
        {
            _timer = new Timer(Execute, null, 0, _intervall);
        }

        private static void WaitForKeyPressToExit()
        {
            Console.ReadLine();
        }

        private static void Execute(object state)
        {
            if (!_locked)
            {
                _locked = true;
                ChurnLogServer.Execute(_rootPath, _protocolFilePath);
                _locked = false;
            }
        }

        private static dynamic GetConfigFromArgs(string[] args)
        {
            var cfgSchema = new AppCfgSchema(null,
                new Route("params", "", isDefault: true)
                    .Param("root", "rootPath", valueType: ValueTypes.String, isRequired: true)
                    .Param("protocol", "protocolFilePath", valueType: ValueTypes.String, isRequired: true)
                    .Param("interval", "intervalInSeconds", valueType: ValueTypes.Number, isRequired: true)
            );

            var cfgcomp = new AppCfgCompiler(cfgSchema);

            var cfg = cfgcomp.Compile(args);
            return cfg;
        }

        #endregion

        #region Fields

        private static string _rootPath;
        private static string _protocolFilePath;
        private static bool _locked;
        private static int _intervall;
        private static Timer _timer;

        #endregion
    }
}