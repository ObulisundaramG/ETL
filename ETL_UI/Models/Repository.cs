using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ETL_UI.Models
{
    public class Repository
    {
        public int Id { get; set; }
        public string RepositoryName { get; set; }
        public string RepositoryDescription { get; set; }
        public string RepositoryDataBaseType { get; set; }
    }
}
