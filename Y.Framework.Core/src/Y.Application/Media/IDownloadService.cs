using Microsoft.AspNetCore.Http;

namespace Y.Services
{
    public interface IDownloadService
    {
        byte[] GetDownloadBits(IFormFile file);
    }
}
