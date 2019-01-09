using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DocumentationCenter.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DocumentationCenter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private ApplicationContext _context;
        private IHostingEnvironment _hostingEnvironment;

        public FilesController(ApplicationContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpPost]
        public IActionResult Upload()
        {
            try
            {
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }    
    }
}