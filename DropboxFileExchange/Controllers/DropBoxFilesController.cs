using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using DropboxFileExchange.services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DropboxFileExchange.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DropBoxFilesController : ControllerBase
    {
       
        private readonly ILogger<DropBoxFilesController> _logger;
        private IDropBoxFilesService _dropBoxFilesService;

        public DropBoxFilesController(ILogger<DropBoxFilesController> logger, IDropBoxFilesService DropBoxFilesService)
        {
            _logger = logger;
            _dropBoxFilesService = DropBoxFilesService;
        }





        [HttpGet]
        [Route(@"~/GetDocument")]
        public async Task<FileResult> GetDocumentAsync(string filename)
        {

            try
            {
                string _fileExtension = Path.GetExtension(filename);

                byte[] fileContent = await _dropBoxFilesService.GetFile(filename);


                if (_fileExtension.ToLower() == ".pdf")
                {
                    return File(fileContent, System.Net.Mime.MediaTypeNames.Application.Pdf, filename);
                }
                else
                {
                    return File(fileContent, System.Net.Mime.MediaTypeNames.Application.Octet, filename);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public class FileUploadModel
        {
            public string FileName { get; set; }
            public IFormFile File { get; set; }

        }


        ///// <summary>
        ///// Save Document
        ///// </summary>
        ///// <param name="uploadedFile"></param>
        ///// <returns></returns>
        [HttpPost]        
        [Route(@"~/SaveDocument")]
        public async Task<ActionResult> SaveUploadedDocumentAsync([FromForm] FileUploadModel uploadedFile)
        {

            using (var reader = new StreamReader(uploadedFile.File.OpenReadStream()))
            {

                var bytes = default(byte[]);
                using (var memstream = new MemoryStream())
                {
                    reader.BaseStream.CopyTo(memstream);
                    bytes = memstream.ToArray();
                }
                await _dropBoxFilesService.WriteFile(uploadedFile.FileName, bytes);
            }
            return Ok(new { status = true, message = "File Uploaded Successfully" });
        }
                     
    }
}