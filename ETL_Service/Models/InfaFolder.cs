using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ETL_Service.Models
{
    public partial class InfaFolder
    {
        public InfaFolder()
        {
            InfaConnector = new HashSet<InfaConnector>();
            InfaMapping = new HashSet<InfaMapping>();
            InfaWorkflow = new HashSet<InfaWorkflow>();
        }

        [Key]
        public int FldrId { get; set; }
        public int? RepId { get; set; }
        public string FldrName { get; set; }
        public string FldrDesc { get; set; }
        public string Shared { get; set; }
        public string Status { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual InfaRepository Rep { get; set; }
        public virtual ICollection<InfaConnector> InfaConnector { get; set; }
        public virtual ICollection<InfaMapping> InfaMapping { get; set; }
        public virtual ICollection<InfaWorkflow> InfaWorkflow { get; set; }
    }
}
