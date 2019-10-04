using System.IO.Compression;
using Microsoft.AspNetCore.Mvc;
using static System.String;

namespace FileUpload.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        [HttpPost]
        public void Post()
        {
            var file = Request.Form.Files.GetFile("some_name");
            var name = Empty;

            using (var zip = new ZipArchive(file.OpenReadStream(), ZipArchiveMode.Read))
                foreach (var entry in zip.Entries)
                    name = entry.Name;

            Ok(name);
        }
    }
}