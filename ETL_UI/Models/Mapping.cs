using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ETL_UI.Models
{
    public class Mapping
    {
        public int Id { get; set; }
        public int FolderId { get; set; }
        public int SessionId { get; set; }
        public string MappingName { get; set; }
        public string MappingDescription { get; set; }
        public string MappingIsValid { get; set; }
        public string TransType { get; set; }
        public string TransName { get; set; }
        public string TransReusable { get; set; }
        public string FieldName { get; set; }
        public string FieldDescription { get; set; }
        public string DataType { get; set; }
        public string PortType { get; set; }
        public int? Prescision { get; set; }
        public int? Scale { get; set; }
        public string Expression { get; set; }
        public string ExpressionType { get; set; }
        public string TableAttributeName { get; set; }
        public string TableAttributeValue { get; set; }
        public string SortKey { get; set; }
        public string SortDirection { get; set; }
        public string InstanceName { get; set; }
        public string InstanceType { get; set; }
        public string XMLTag { get; set; }
    }
}
