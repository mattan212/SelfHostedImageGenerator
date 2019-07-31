using System;
using System.IO;

namespace SelfHostedImageGenerator
{
    public class ImageBankDataService
    {
        private const string OutputFolder = "Output";

        public ImageBankDataService()
        {
            var outputPath = Path.Combine(Directory.GetCurrentDirectory(), OutputFolder);
            if (!Directory.Exists(outputPath))
            {
                Directory.CreateDirectory(outputPath);
            }
        }

        public bool SaveImage(ImageModel imageModel)
        {
            var outputPath = Path.Combine(Directory.GetCurrentDirectory(), OutputFolder, imageModel.Name);

            var stream = ConvertBase64ToImage(imageModel.Data.Replace("data:image/png;base64,", ""));
            
            return SaveFile(outputPath, stream);
        }

        private bool SaveFile(string fileName, Stream input)
        {
            using (Stream file = File.Create(fileName))
            {
                CopyStream(input, file);
                input.Close();
                file.Close();
            }

            return true;
        }

        private Stream ConvertBase64ToImage(string base64String)
        {
            byte[] data = Convert.FromBase64String(base64String);
            return new MemoryStream(data, 0, data.Length);
        }

        private void CopyStream(Stream input, Stream output)
        {
            byte[] buffer = new byte[8 * 1024];
            int len;
            while ((len = input.Read(buffer, 0, buffer.Length)) > 0)
            {
                output.Write(buffer, 0, len);
            }
        }
    }
}