using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ETL_UI.Models
{
    public class Connector
    {
        public int Id { get; set; }
        public int RepositoryId { get; set; }
        public int FolderId { get; set; }
        public int WorkFlowId { get; set; }
        public int SessionId { get; set; }
        public int MappingId { get; set; }

    }
}
