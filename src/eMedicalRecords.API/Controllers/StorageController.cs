using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace eMedicalRecords.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class StorageController : ControllerBase
    {
        private readonly IHostEnvironment _hostEnvironment;

        public StorageController(IHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
        }
        
        [HttpGet("{fileId}")]
        public IActionResult Get(string fileId)
        {
            var year = fileId.Substring(0, 4);
            var monthDate = fileId.Substring(4, 4);
            var fileName = fileId.Substring(8);
            
            var dictionary = Path.Combine(_hostEnvironment.ContentRootPath, "images", Path.Combine(year, monthDate, fileName));
            var file = System.IO.File.OpenRead(dictionary);
            return File(file, "image/jpeg");
        }
    }
}