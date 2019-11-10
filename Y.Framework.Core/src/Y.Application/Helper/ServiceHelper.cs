using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using Abp.Application.Services;
using Abp.AutoMapper;
using Abp.Configuration;
using Abp.Domain.Entities;
using Abp.Localization;
using AutoMapper;
using Castle.Core.Internal;
using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip;
using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.Primitives;
using Y.Core;
using Y.Dto;


namespace Y.Services
{
    public static class ServiceHelper

    {
        public static void ZipFolder(this string folderName, string outPathname, string password = null)
        {

            FileStream fsOut = File.Create(outPathname);
            ZipOutputStream zipStream = new ZipOutputStream(fsOut);

            zipStream.SetLevel(3); //0-9, 9 being the highest level of compression

            if (password != null)
                zipStream.Password = password;	// optional. Null is the same as not setting. Required if using AES.

            // This setting will strip the leading part of the folder path in the entries, to
            // make the entries relative to the starting folder.
            // To include the full path for each entry up to the drive root, assign folderOffset = 0.
            int folderOffset = folderName.Length + (folderName.EndsWith("\\") ? 0 : 1);

            CompressFolder(folderName, zipStream, folderOffset);

            zipStream.IsStreamOwner = true;	// Makes the Close also Close the underlying stream
            zipStream.Close();
        }

        private static void CompressFolder(string path, ZipOutputStream zipStream, int folderOffset)
        {
            string[] files = Directory.GetFiles(path);

            foreach (string filename in files)
            {

                FileInfo fi = new FileInfo(filename);

                string entryName = filename.Substring(folderOffset); // Makes the name in zip based on the folder
                entryName = ZipEntry.CleanName(entryName); // Removes drive from name and fixes slash direction
                ZipEntry newEntry = new ZipEntry(entryName);
                newEntry.DateTime = fi.LastWriteTime; // Note the zip format stores 2 second granularity

                // Specifying the AESKeySize triggers AES encryption. Allowable values are 0 (off), 128 or 256.
                // A password on the ZipOutputStream is required if using AES.
                //   newEntry.AESKeySize = 256;

                // To permit the zip to be unpacked by built-in extractor in WinXP and Server2003, WinZip 8, Java, and other older code,
                // you need to do one of the following: Specify UseZip64.Off, or set the Size.
                // If the file may be bigger than 4GB, or you do not need WinXP built-in compatibility, you do not need either,
                // but the zip will be in Zip64 format which not all utilities can understand.
                //   zipStream.UseZip64 = UseZip64.Off;
                newEntry.Size = fi.Length;

                zipStream.PutNextEntry(newEntry);

                // Zip the file in buffered chunks
                // the "using" will close the stream even if an exception occurs
                byte[] buffer = new byte[4096];
                using (FileStream streamReader = File.OpenRead(filename))
                {
                    StreamUtils.Copy(streamReader, zipStream, buffer);
                }

                zipStream.CloseEntry();
            }
        }

        public static IImageProcessingContext<TPixel> ApplyScalingLogoWaterMark<TPixel>(
                this IImageProcessingContext<TPixel> processingContext,
                byte[] watermark)
                where TPixel : struct, IPixel<TPixel>
        {
            return processingContext.Apply(
                image =>
                {
                    // Dispose of logo once used.
                    using (var logo = Image.Load<TPixel>(watermark))
                    {
                        bool worh = image.Width > image.Height;
                        double coff = worh
                            ? image.Width * 0.127 / Math.Min(logo.Width, logo.Height)
                            : image.Height * 0.127 / Math.Max(logo.Width, logo.Height);

                        var size = new Size((int)(logo.Width * coff), (int)(logo.Height * coff));
                        var location = new Point(image.Width - size.Width - 57, image.Height - size.Height - 271);

                        // Resize the logo
                        logo.Mutate(i => i.Resize(size));

                        image.Mutate(i => i.DrawImage(logo, location, PixelColorBlendingMode.Normal, PixelAlphaCompositionMode.SrcOver, 1f));
                    }
                });
        }

        public static byte[] ImageToByte(this System.Drawing.Image img)
        {
            using (var stream = new MemoryStream())
            {
                img.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                return stream.ToArray();
            }
        }

        public static IImageProcessingContext<TPixel> ApplyScalingWaterMark<TPixel>(this IImageProcessingContext<TPixel> processingContext, Font font, string text, TPixel color, float padding, bool wordwrap)
            where TPixel : struct, IPixel<TPixel>
        {
            if (wordwrap)
            {
                return processingContext.ApplyScalingWaterMarkWordWrap(font, text, color, padding);
            }
            else
            {
                return processingContext.ApplyScalingWaterMarkSimple(font, text, color, padding);
            }
        }

        public static IImageProcessingContext<TPixel> ApplyScalingWaterMarkSimple<TPixel>(this IImageProcessingContext<TPixel> processingContext, Font font, string text, TPixel color, float padding)
            where TPixel : struct, IPixel<TPixel>
        {
            return processingContext.Apply(img =>
            {
                float targetWidth = img.Width - (padding * 2);
                float targetHeight = img.Height - (padding * 2);

                // measure the text size
                SizeF size = TextMeasurer.Measure(text, new RendererOptions(font));

                //find out how much we need to scale the text to fill the space (up or down)
                float scalingFactor = Math.Min(img.Width / size.Width, img.Height / size.Height);

                //create a new font
                Font scaledFont = new Font(font, scalingFactor * font.Size);

                var center = new PointF(img.Width / 2, img.Height / 2);

                var textGraphicsOptions = new TextGraphicsOptions(true)
                {
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };
                img.Mutate(i => i.DrawText(textGraphicsOptions, text, scaledFont, color, center));
            });
        }

        public static IImageProcessingContext<TPixel> ApplyScalingWaterMarkWordWrap<TPixel>(this IImageProcessingContext<TPixel>
            processingContext, Font font, string text, TPixel color, float padding)
            where TPixel : struct, IPixel<TPixel>
        {
            return processingContext.Apply(img =>
            {
                float targetWidth = img.Width - (padding * 2);
                float targetHeight = img.Height - (padding * 2);

                float targetMinHeight = img.Height - (padding * 3); // must be with in a margin width of the target height

                // now we are working i 2 dimensions at once and can't just scale because it will cause the text to
                // reflow we need to just try multiple times

                var scaledFont = font;
                SizeF s = new SizeF(float.MaxValue, float.MaxValue);

                float scaleFactor = (scaledFont.Size / 2);// everytime we change direction we half this size
                int trapCount = (int)scaledFont.Size * 2;
                if (trapCount < 10)
                {
                    trapCount = 10;
                }

                bool isTooSmall = false;

                while ((s.Height > targetHeight || s.Height < targetMinHeight) && trapCount > 0)
                {
                    if (s.Height > targetHeight)
                    {
                        if (isTooSmall)
                        {
                            scaleFactor = scaleFactor / 2;
                        }

                        scaledFont = new Font(scaledFont, scaledFont.Size - scaleFactor);
                        isTooSmall = false;
                    }

                    if (s.Height < targetMinHeight)
                    {
                        if (!isTooSmall)
                        {
                            scaleFactor = scaleFactor / 2;
                        }
                        scaledFont = new Font(scaledFont, scaledFont.Size + scaleFactor);
                        isTooSmall = true;
                    }
                    trapCount--;

                    s = TextMeasurer.Measure(text, new RendererOptions(scaledFont)
                    {
                        WrappingWidth = targetWidth
                    });
                }

                var center = new PointF(padding, img.Height / 2);
                var textGraphicsOptions = new TextGraphicsOptions(true)
                {
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Center,
                    WrapTextWidth = targetWidth
                };
                img.Mutate(i => i.DrawText(textGraphicsOptions, text, scaledFont, color, center));
            });
        }

        public static string GenerateRandomTicketNumber()
        {
            return CommonHelpers.RandomNumberString(13);
        }

        public static void GetDefaultTranslation<U>(this IHasTranlationDto<U> input) where U : ITranslationDto
        {
            var defaultLocalize = input.Translations
                .FirstOrDefault(p => p.Language == CultureInfo.CurrentUICulture.Name);
            if (defaultLocalize != null)
            {
                defaultLocalize.MapTo(input);
            }

        }

        public static CreateMultiLingualMapResult<TMultiLingualEntity, TTranslation, TDestination> YCreateMultiLingualMap<TMultiLingualEntity, TMultiLingualEntityPrimaryKey, TTranslation, TDestination>(
           this IMapperConfigurationExpression configuration, MultiLingualMapContext multiLingualMapContext)
           where TTranslation : class, IEntityTranslation<TMultiLingualEntity, TMultiLingualEntityPrimaryKey>
           where TMultiLingualEntity : IMultiLingualEntity<TTranslation>
        {
            var result = new CreateMultiLingualMapResult<TMultiLingualEntity, TTranslation, TDestination>();

            result.TranslationMap = configuration.CreateMap<TTranslation, TDestination>()
                .ForMember("Id", p => p.Ignore());
            //.ForMember("CreationTime", p => p.Ignore());
            result.EntityMap = configuration.CreateMap<TMultiLingualEntity, TDestination>().AfterMap((source, destination, context) =>
            {
                var translation = source.Translations.FirstOrDefault(pt => pt.Language == CultureInfo.CurrentUICulture.Name);
                if (translation != null)
                {
                    context.Mapper.Map(translation, destination);
                    return;
                }

                var defaultLanguage = multiLingualMapContext.SettingManager
                                                            .GetSettingValue(LocalizationSettingNames.DefaultLanguage);

                translation = source.Translations.FirstOrDefault(pt => pt.Language == defaultLanguage);
                if (translation != null)
                {
                    context.Mapper.Map(translation, destination);
                    return;
                }

                translation = source.Translations.FirstOrDefault();
                if (translation != null)
                {
                    context.Mapper.Map(translation, destination);
                }
            });

            return result;
        }

        public static CreateMultiLingualMapResult<TMultiLingualEntity, TTranslation, TDestination> YCreateMultiLingualMap<TMultiLingualEntity, TTranslation, TDestination>(this IMapperConfigurationExpression configuration, MultiLingualMapContext multiLingualMapContext)
            where TTranslation : class, IEntity, IEntityTranslation<TMultiLingualEntity, int>
            where TMultiLingualEntity : IMultiLingualEntity<TTranslation>
        {
            return configuration.YCreateMultiLingualMap<TMultiLingualEntity, int, TTranslation, TDestination>(multiLingualMapContext);
        }

       
    }
}

