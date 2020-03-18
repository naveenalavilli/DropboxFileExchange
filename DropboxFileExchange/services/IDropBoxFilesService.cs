using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DropboxFileExchange.services
{
   public interface IDropBoxFilesService
    {
        Task<byte[]> GetFile(string Folder,string File);
        Task WriteFile(string Folder, string File, byte[] Content);
    }
}
