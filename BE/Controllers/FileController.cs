using AutoMapper;
using Common.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : BaseController
    {
        private IMapper _mapper;
        public FileController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Save([FromForm] List<IFormFile> files)
        {
            IActionResult result;
            //List<String> urls = new List<string>();
            var filePaths = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Files");
            var urlPath = Path.Combine("https://localhost:44309", "Files");
            List<Domain.Entities.File> datas = new List<Domain.Entities.File>();
            if (!Directory.Exists(filePaths))
            {
                //Directory.Delete(filePath, true);
                Directory.CreateDirectory(filePaths);
            }
            if (files != null && files.Count > 0)
            {
                datas = _mapper.Map<List<IFormFile>, List<Domain.Entities.File>>(files);
                for (int i = 0; i < files.Count(); i++)
                {
                    var guildId = Guid.NewGuid();
                    var fileName = guildId.ToString() + Path.GetExtension(files[i].FileName);
                    var filePath = Path.Combine(filePaths, fileName);
                    datas[i].Id = guildId;
                    datas[i].Url = Path.Combine(urlPath, fileName);
                    if (files[i].Length > 0)
                    {
                        using (var stream = System.IO.File.Create(filePath))
                        {
                            //stream.Write();
                            await files[i].CopyToAsync(stream);
                        }
                    }
                }
            }
            result = CommonResponse(0, datas);
            return result;
        }
    }
}
