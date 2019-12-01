using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dot.Net.WebApi.Domain
{
    public class Log
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public string LogTypeId { get; set; }

        public LogType Type { get; set; }
    }

    public class LogType
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
