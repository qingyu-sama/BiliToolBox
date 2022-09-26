using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiliToolBox.Entitys
{
    public class Result<T>
    {
        public int Code { get; set; }

        public string? Message { get; set; }

        public int TTL { get; set; }

        public T? Data { get; set; }
    }
}
