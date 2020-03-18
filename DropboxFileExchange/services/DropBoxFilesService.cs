using Dropbox.Api;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
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
        public async Task<byte[]> GetFile(string Folder, string File)
        {
            string AccessToken = _IConfiguration.GetSection("DropBoxAccessToken").Value;

            using (var dbx = new DropboxClient(AccessToken))
            using (var response = await dbx.Files.DownloadAsync(Folder + "/" + File))
            {
              return await response.GetContentAsByteArrayAsync();
            }           
        }

        public Task WriteFile(string Folder, string File, byte[] Content)
        {
            throw new NotImplementedException();
        }
    }
}
