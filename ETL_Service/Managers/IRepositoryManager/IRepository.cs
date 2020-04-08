using ETL_UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ETL_Service.Managers.IRepositoryManager
{
    public interface IRepository
    {
        string CreateRepository(List<InfomaticaModel> InfomaticaModelList);
    }
}
