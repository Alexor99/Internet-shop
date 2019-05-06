using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ts.DatabaseEngine.Interfaces;

namespace sirena.Infrustructure
{
    public class Config : IConfig
    {
        public Config(string connection)
        {
            this.ConnString = connection;
        }

        public string ConnString { get; set; }
    }
}
