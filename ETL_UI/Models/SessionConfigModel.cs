using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ETL_UI.Models
{
    public class SessionConfigModel
    {
        public int WorkFlowId { get; set; }
        public int SessionId { get; set; }
        public string SessionName { get; set; }
        public string XMLTag { get; set; }
        public string ConfigAttributeName { get; set; }
        public string ConfigAttributeDescValue { get; set; }
        public string IsDefault { get; set; }
    }
}
