using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DropboxFileExchange.services
{
    public class DropBoxFilesService : IDropBoxFilesService
    {
        public Task<byte[]> GetFile(string Folder, string File)
        {
            return Task.FromResult( Encoding.ASCII.GetBytes("testString"));
        }

        public Task WriteFile(string Folder, string File, byte[] Content)
        {
            throw new NotImplementedException();
        }
    }
}
