using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace Old.Controllers
{
    public class ValuesController : Controller
    {
        public ActionResult Get(string songName)
        {
            var fileLocation = Path.Combine(Server.MapPath("~/App_Data/" + Path.Combine("Songs", songName + ".mp3")));
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
