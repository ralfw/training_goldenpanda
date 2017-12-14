using System;
using appcfg;
using sc.administration.Data;
using sc.contracts;

namespace sc.administration.Providers
{
    public class ConfigProvider
    {
        public static dynamic GetConfig(string[] args)
        {

            var cfgSchema = new AppCfgSchema(string.Empty,
                new Route("cu", isDefault: false)
                    .Param("email", valueType: ValueTypes.String, isRequired: true)
                    .Param("role", valueType: ValueTypes.String, isRequired: true)
                    .Param("serveruri", valueType: ValueTypes.String, isRequired: true));

            var cfgcomp = new AppCfgCompiler(cfgSchema);

            return cfgcomp.Compile(args);

        }
    }
}
