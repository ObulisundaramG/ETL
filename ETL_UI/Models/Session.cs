using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ETL_UI.Models
{
    public class Session
    {
        public int Id { get; set; }
        public string SessionName { get; set; }
        public string SessionDescription { get; set; }
        public string MappingName { get; set; }
    }
}
