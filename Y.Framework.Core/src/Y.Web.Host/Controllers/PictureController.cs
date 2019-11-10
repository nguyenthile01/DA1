using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Abp;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using QRCoder;
using Y.Controllers;
using Y.Services;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using Y.Core;


namespace Y.Web.Host.Controllers
{
    [Route("api/services/app/[controller]/[action]")]
    public class PictureController : YControllerBase
    {
        private readonly IPictureAppService pictureAppService;
        private readonly IDownloadService downloadService;
        private readonly INopFileProvider fileProvider;
        private IHostingEnvironment hostingEnvironment;
        public PictureController(IHostingEnvironment hostingEnvironment,
            IDownloadService downloadService,
            INopFileProvider fileProvider,
            IPictureAppService pictureAppService)
        {
            this.hostingEnvironment = hostingEnvironment;
            this.downloadService = downloadService;
            this.fileProvider = fileProvider;
            this.pictureAppService = pictureAppService;
        }

        [HttpGet]
        public string GetImageUrl(int id)
        {            
            return  pictureAppService.GetPictureUrl(id, 400) ;
        }

        [HttpGet]
        public List<object> GetImageUrls(string ids)
        {
            var listImages = new List<object>();
            if (!string.IsNullOrEmpty(ids))
            {
                var listIds = ids.Split(',').Select(p => int.Parse(p)).ToList();
                foreach (var item in listIds)
                {
                    listImages.Add(new { Id = item, Path = pictureAppService.GetPictureUrl(item, 400) });
                }
            }

            return listImages;
        }

        [HttpPost]
        [Produces("application/json")]
        [RequestSizeLimit(100_000_000)]
        public virtual object UploadImage(IFormFile file)
        {
            if (file == null)
            {
                throw new AbpException("No file uploaded");

            }

            var fileBinary = downloadService.GetDownloadBits(file);

            const string qqFileNameParameter = "qqfilename";
            var fileName = file.FileName;
            if (string.IsNullOrEmpty(fileName) && Request.Form.ContainsKey(qqFileNameParameter))
                fileName = Request.Form[qqFileNameParameter].ToString();
            //remove path (passed in IE)
            fileName = fileProvider.GetFileName(fileName);

            var contentType = file.ContentType;

            var fileExtension = fileProvider.GetFileExtension(fileName);
            if (!string.IsNullOrEmpty(fileExtension))
                fileExtension = fileExtension.ToLowerInvariant();

            if (string.IsNullOrEmpty(contentType))
            {
                switch (fileExtension)
                {
                    case ".bmp":
                        contentType = MimeTypes.ImageBmp;
                        break;

                    case ".gif":
                        contentType = MimeTypes.ImageGif;
                        break;

                    case ".jpeg":
                    case ".jpg":
                    case ".jpe":
                    case ".jfif":
                    case ".pjpeg":
                    case ".pjp":
                        contentType = MimeTypes.ImageJpeg;
                        break;

                    case ".png":
                        contentType = MimeTypes.ImagePng;
                        break;

                    case ".tiff":
                    case ".tif":
                        contentType = MimeTypes.ImageTiff;
                        break;

                    default:
                        break;
                }
            }

            var picture = pictureAppService.InsertPicture(fileBinary, contentType, null);

            return new { Id = picture.Id, Path = pictureAppService.GetPictureUrl(picture.Id, 200) };
        }

        [HttpPost]
        public void AddPictureWaterMark()
        {
            string webRootPath = hostingEnvironment.WebRootPath;
            var baseImage = Path.Combine(webRootPath, "ticket/ticketprototype.png");
            for (int i = 0; i < 5; i++)
            {
                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                QRCodeData qrCodeData = qrGenerator.CreateQrCode("nguye van ana hoan" + i, QRCodeGenerator.ECCLevel.Q);
                QRCode qrCode = new QRCode(qrCodeData);
                var qrCodeImage = qrCode.GetGraphic(20, Color.Black, Color.White, iconBorderWidth: 0, iconSizePercent: 100, drawQuietZones: false).ImageToByte();

                var outputPath = Path.Combine(webRootPath, $"ticket/output/ticket_{i}.png");

                SixLabors.Fonts.Font font = SixLabors.Fonts.SystemFonts.CreateFont("Arial", 12); // for scaling water mark size is largly ignored.
                using (var img = SixLabors.ImageSharp.Image.Load(baseImage))
                {
                    using (var img2 = img.Clone(ctx => ctx.ApplyScalingLogoWaterMark(qrCodeImage)))
                    {
                        img2.Save(outputPath);
                    }
                    //using (var img2 = img.Clone(ctx => ctx.ApplyScalingWaterMark(font, "A short piece of text", Rgba32.HotPink, 5, false)))
                    //{
                    //    img2.Save("output/simple.png");
                    //}
                }

            }
        }
        [HttpPost]
        public void ZipTicket()
        {
            string webRootPath = hostingEnvironment.WebRootPath;
            var baseImage = Path.Combine(webRootPath, "ticket/output");
            var output = Path.Combine(webRootPath, $"ticket/zip/Sample_{CommonHelpers.RandomString(12)}.zip");
            baseImage.ZipFolder(output);

        }


    }
}
