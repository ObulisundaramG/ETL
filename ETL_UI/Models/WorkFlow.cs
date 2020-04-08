using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ETL_UI.Models
{
    public class WorkFlow
    {
        public int Id { get; set; }
        public string WorkFlowName { get; set; }
        public string WorkFlowDescription { get; set; }
        public string WorkFlowColumn { get; set; }
    }
}
