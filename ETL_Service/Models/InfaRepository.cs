using System;
using System.Collections.Generic;

namespace ETL_Service.Models
{
    public partial class InfaRepository
    {
        public InfaRepository()
        {
            InfaConnector = new HashSet<InfaConnector>();
            InfaFolder = new HashSet<InfaFolder>();
        }

        public int RepId { get; set; }
        public string RepName { get; set; }
        public string RepDesc { get; set; }
        public string DatabaseType { get; set; }
        public string Status { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual ICollection<InfaConnector> InfaConnector { get; set; }
        public virtual ICollection<InfaFolder> InfaFolder { get; set; }
    }
}
