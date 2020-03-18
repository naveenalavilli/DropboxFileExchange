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
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DropboxFileExchange.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DropBoxFilesController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<DropBoxFilesController> _logger;
        private IDropBoxFilesService _dropBoxFilesService;

        public DropBoxFilesController(ILogger<DropBoxFilesController> logger, IDropBoxFilesService DropBoxFilesService)
        {
            _logger = logger;
            _dropBoxFilesService = DropBoxFilesService;
        }

     



        [HttpGet]
        [Route(@"~/GetDocument")]
        public async Task<HttpResponseMessage> GetDocumentAsync(string fileUrl)
        {
            string _contentType;

            try
            {
                string _fileExtension = Path.GetExtension(fileUrl);
                //only pdf or word documents will ever get uploaded/downloaded
                if (_fileExtension.ToLower() == ".pdf")
                {
                    _contentType = "application/pdf";
                }
                else
                {
                    _contentType = "application/msword";
                }

                byte[] fileContent =await _dropBoxFilesService.GetFile("files","file.pdf") ;//= new byte(); //= await _siteJobService.GetDocument(fileUrl, iwasUserID);

                HttpResponseMessage response = new HttpResponseMessage();
                var contentLength = fileContent.Length;
                response.Content = new StreamContent(new MemoryStream(fileContent));
                response.Content.Headers.ContentType = new MediaTypeHeaderValue(_contentType);
                response.Content.Headers.ContentLength = contentLength;
                ContentDispositionHeaderValue contentDisposition = null;
                if (ContentDispositionHeaderValue.TryParse("inline; filename=" + "Document" + ".pdf", out contentDisposition))
                {
                    response.Content.Headers.ContentDisposition = contentDisposition;
                }

                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}