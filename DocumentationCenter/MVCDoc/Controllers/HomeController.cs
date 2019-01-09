using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DocumentationCenter.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.EntityFrameworkCore;

namespace DocumentationCenter.Controllers
{
    public class HomeController : Controller
    {
        ApplicationContext _context;
        IHostingEnvironment _appEnvironment;

        public HomeController(ApplicationContext context, IHostingEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Files.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile File, string description)
        {
            if (File != null)
            {
                string[] Ext = File.FileName.Split('.');                                        
                if (Ext[1] == "doc" || Ext[1] == "docx" || Ext[1] == "xls" || Ext[1] == "xlsx") 
                {
                    string path = "/Documents/" + File.FileName;

                    using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                    {
                        await File.CopyToAsync(fileStream);
                    }
                    FileModel fileModel = new FileModel { Name = File.FileName, Description = description, Path = path };
                    _context.Files.Add(fileModel);
                    _context.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Download(string path)
        {           
            path = _appEnvironment.WebRootPath + path;

            var Memory = new MemoryStream();
            using (var Stream = new FileStream(path, FileMode.Open))
            {
                await Stream.CopyToAsync(Memory);
            }
            Memory.Position = 0;

            return File(Memory, "application/vnd.ms-word", Path.GetFileName(path));
        }

        public async Task<IActionResult> Delete(string path, int? id)
        {
            if (id != null)
            {
                FileModel file = await _context.Files.FirstOrDefaultAsync(f => f.Id == id);
                if (file != null)
                {
                    _context.Files.Remove(file);
                    System.IO.File.Delete(_appEnvironment.WebRootPath + path);
                    await _context.SaveChangesAsync();
                }
            }
            return RedirectToAction("Index");
        }
    }
}
