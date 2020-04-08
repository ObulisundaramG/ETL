using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ETL_UI.Models
{
    public class Folder
    {
        public int Id { get; set; }
        public string FolderName { get; set; }
        public string FolderDescription { get; set; }
        public string Shared { get; set; }
        public string Owner { get; set; }
    }
}
