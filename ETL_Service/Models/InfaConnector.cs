using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ETL_Service.Models
{
    public partial class InfaConnector
    {
        [Key]
        public int ConnectorId { get; set; }
        public int MappingId { get; set; }
        public int SessionId { get; set; }
        public string XmlTag { get; set; }
        public string FromField { get; set; }
        public string FromInstance { get; set; }
        public string FromInstanceType { get; set; }
        public string ToField { get; set; }
        public string ToInstance { get; set; }
        public string ToInstanceType { get; set; }
        public string Status { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual InfaMapping Mapping { get; set; }
        public virtual InfaSession Session { get; set; }
        
    }
}
