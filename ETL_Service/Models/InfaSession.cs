using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ETL_Service.Models
{
    public partial class InfaSession
    {
        public InfaSession()
        {
            InfaConnector = new HashSet<InfaConnector>();
            InfaMapping = new HashSet<InfaMapping>();
        }

        [Key]
        public int SessionId { get; set; }
        public int? WkfId { get; set; }
        public string SessionName { get; set; }
        public string SessionDesc { get; set; }
        public string Status { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual InfaWorkflow Wkf { get; set; }
        public virtual ICollection<InfaConnector> InfaConnector { get; set; }
        public virtual ICollection<InfaMapping> InfaMapping { get; set; }
    }
}
