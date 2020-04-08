using System;
using System.Collections.Generic;

namespace ETL_Service.Models
{
    public partial class InfaMapping
    {
        public InfaMapping()
        {
            InfaConnector = new HashSet<InfaConnector>();
        }

        public int MappingId { get; set; }
        public int? SessionId { get; set; }
        public int? FldrId { get; set; }
        public string MappingName { get; set; }
        public string MappingDesc { get; set; }
        public string MappingIsvalid { get; set; }
        public string TransType { get; set; }
        public string TransName { get; set; }
        public string TransReusable { get; set; }
        public string FieldName { get; set; }
        public string FieldDesc { get; set; }
        public string Datatype { get; set; }
        public string Porttype { get; set; }
        public int? Precision { get; set; }
        public int? Scale { get; set; }
        public string Expression { get; set; }
        public string ExpressionType { get; set; }
        public string TableattributeName { get; set; }
        public string TableattributeValue { get; set; }
        public string SortKey { get; set; }
        public string SortDirection { get; set; }
        public string InstanceName { get; set; }
        public string InstanceType { get; set; }
        public string AssociatedSourceInstance { get; set; }
        public string TargetInstance { get; set; }
        public string TargetLoadorder { get; set; }
        public string Flag { get; set; }
        public DateTime? LoadTimestamp { get; set; }
        public DateTime? UpdatedTimestamp { get; set; }
        public string Status { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual InfaFolder Fldr { get; set; }
        public virtual InfaSession Session { get; set; }
        public virtual ICollection<InfaConnector> InfaConnector { get; set; }
    }
}
