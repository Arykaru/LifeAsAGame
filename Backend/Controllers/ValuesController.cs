using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public ValuesController(IHostingEnvironment environment)
        {
            _hostingEnvironment = environment;
        }

        [HttpGet("")]
        public ActionResult Get(string songName)
        {
            var fileLocation = Path.Combine(_hostingEnvironment.WebRootPath, Path.Combine("Songs", songName + ".mp3"));
            var bytes = new byte[0];

            using (var fs = new FileStream(fileLocation, FileMode.Open, FileAccess.Read))
            {
                var br = new BinaryReader(fs);
                long numBytes = new FileInfo(fileLocation).Length;
                bytes = br.ReadBytes((int)numBytes);
            }

            return File(bytes, "audio/mpeg", "recording.mp3");
        }
    }
}
