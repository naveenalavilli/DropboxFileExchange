using Dropbox.Api;
using Dropbox.Api.Files;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DropboxFileExchange.services
{
    public class DropBoxFilesService : IDropBoxFilesService
    {
        IConfiguration _IConfiguration;
        public DropBoxFilesService(IConfiguration IConfiguration)
        {
            _IConfiguration = IConfiguration;
        }
        public async Task<byte[]> GetFile(string File)
        {
            string AccessToken = _IConfiguration.GetSection("DropBoxAccessToken").Value;

            using (var _dropBox = new DropboxClient(AccessToken))
            using (var response = await _dropBox.Files.DownloadAsync("/" + File))
            {
                return await response.GetContentAsByteArrayAsync();
            }
        }

        public async Task WriteFile(string FileName, byte[] Content)
        {
            string AccessToken = _IConfiguration.GetSection("DropBoxAccessToken").Value;
            using (var _dropBox = new DropboxClient(AccessToken))
            using (var _memoryStream = new MemoryStream(Content))
            {
                var updated = await _dropBox.Files.UploadAsync(
                     "/" + FileName,
                    WriteMode.Overwrite.Instance,
                    body: _memoryStream);
            }
        }
    }
}
