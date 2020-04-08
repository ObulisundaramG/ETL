using System;
using System.Collections.Generic;

namespace ETL_Service.Models
{
    public partial class InfaWorkflow
    {
        public InfaWorkflow()
        {
            InfaConnector = new HashSet<InfaConnector>();
            InfaSession = new HashSet<InfaSession>();
        }

        public int WkfId { get; set; }
        public int? FldrId { get; set; }
        public string WkfName { get; set; }
        public string WkfDesc { get; set; }
        public string WkfCol { get; set; }
        public string Status { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual InfaFolder Fldr { get; set; }
        public virtual ICollection<InfaConnector> InfaConnector { get; set; }
        public virtual ICollection<InfaSession> InfaSession { get; set; }
    }
}
