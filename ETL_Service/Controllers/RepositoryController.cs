using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ETL_Service.Managers.IRepositoryManager;
using ETL_UI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ETL_Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RepositoryController : ControllerBase
    {
        public RepositoryController()
        {
        }
        public IActionResult Create([FromBody]List<InfomaticaModel> InfomaticaModel)
        {
            ETL_Service.Managers.RepositoryManager.Repository repo = new ETL_Service.Managers.RepositoryManager.Repository();
            string str = repo.CreateRepository(InfomaticaModel);
            return Ok(str);
        }
    }
}