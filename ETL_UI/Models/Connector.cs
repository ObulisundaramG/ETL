using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ETL_UI.Models
{
    public class Connector
    {
        //public int Id { get; set; }
        //public int RepositoryId { get; set; }
        //public int FolderId { get; set; }
        //public int WorkFlowId { get; set; }
        //public int SessionId { get; set; }
        //public int MappingId { get; set; }
        public int MappingId { get; set; }
        public int SessionId { get; set; }
        public string XmlTag { get; set; }
        public string FromField { get; set; }
        public string FromInstance { get; set; }
        public string FromInstanceType { get; set; }
        public string ToField { get; set; }
        public string ToInstance { get; set; }
        public string ToInstanceType { get; set; }
        public bool IsExecuted { get; set; }

    }
}
