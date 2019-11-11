using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ControlPanel.Models;
using ControlPanel.ViewModels;
using System.IO;
using System.IO.Compression;
using ControlPanel.Helpers;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using ControlPanel.Services;

namespace ControlPanel.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDMSService _dmsService;

        public HomeController(IDMSService dmsService)
        {
            _dmsService = dmsService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UploadZip(FormDataViewModel formData)
        {
            if (ModelState.IsValid)
            {
                IFormFile file = formData.File;
                //copy file to server
                if (file.Length > 0)
                {
                    try
                    {
                        //get a temp location path to copy the file
                        var filePath = Path.GetTempFileName();
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await formData.File.CopyToAsync(stream);
                        }
                        //unzip it
                        string unzipedPath = Path.GetTempPath();
                        ZipFile.ExtractToDirectory(filePath, unzipedPath, true);
                        //get JSON
                        string targetPath = string.Concat(unzipedPath, Path.GetFileNameWithoutExtension(file.FileName));
                        TreeNode root = new TreeNode(new DirectoryInfo(targetPath));
                        var json = JsonConvert.SerializeObject(root);
                        //post the file
                        StatusDTO response = await _dmsService.PostFile(json,formData.Username,formData.Password);

                        ViewData[(response.IsSuccess) ? "Success" : "Error"] = response.Message;
                    }
                    catch (Exception)
                    {

                        ViewData["Error"] = "Error occurred while submitting zip file.";
                    }
                }
            }

            return View("Index", formData);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
