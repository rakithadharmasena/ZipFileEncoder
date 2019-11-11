using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataManagementSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DataManagementSystem.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/File")]
    public class FileController : Controller
    {
        private readonly IZipFileService _zipFileService;

        public FileController(IZipFileService zipFileService)
        {
            _zipFileService = zipFileService;
        }

        [HttpPost]
        public async Task<JsonResult> Post([FromBody]Dictionary<string, byte[]> file)
        {
            var addedFile = await _zipFileService.SaveFile(file["Content"]);
            return Json(new {
                Message = (file==null)? "Error saving file in DMS database" : "ZIP File Saved Successfully in DMS database",
                FileId = addedFile.FileId
            });
        }
    }
}