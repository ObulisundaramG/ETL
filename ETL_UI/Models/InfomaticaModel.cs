using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ETL_UI.Models
{
    public class InfomaticaModel
    {
        public Repository Repository { get; set; } = new Repository();
        public List<Folder> Folders { get; set; } = new List<Folder>();
        public WorkFlow WorkFlow { get; set; } = new WorkFlow();
        public Session Session { get; set; } = new Session();
        public List<Mapping> MappingList { get; set; } = new List<Mapping>();
    }
}
