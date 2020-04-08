using System;
using System.Collections.Generic;

namespace ETL_Service.Models
{
    public partial class InfaConnector
    {
        public int ConnectorId { get; set; }
        public int? RepId { get; set; }
        public int? FldrId { get; set; }
        public int? WkfId { get; set; }
        public int? SessionId { get; set; }
        public int? MappingId { get; set; }
        public string Status { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual InfaFolder Fldr { get; set; }
        public virtual InfaMapping Mapping { get; set; }
        public virtual InfaRepository Rep { get; set; }
        public virtual InfaSession Session { get; set; }
        public virtual InfaWorkflow Wkf { get; set; }
    }
}
