using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Data
{
    public class AppLog
    {
        public int Id { get; set; }

        public string LogType { get; set; }
        public string Details { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

    }
}
