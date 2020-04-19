using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ETL_Service.Models
{
    public class InfaSessionConfig
    {
        [Key]
        public int SessionConfigId { get; set; }
        public int WorkFlowId { get; set; }
        public int SessionId { get; set; }
        public string SessionName { get; set; }
        public string XMLTag { get; set; }
        public string ConfigAttributeName { get; set; }
        public string ConfigAttributeDescValue { get; set; }
        public string IsDefault { get; set; }

        public virtual InfaWorkflow WorkFlow { get; set; }
        public virtual InfaSession Session { get; set; }
    }
}
