using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;
using System.IO;
using System.Threading.Tasks;

namespace HortifrutiWebApp
{
    public static class AppUtils
    {
        public static async Task ProcessImageFile(int productId, IFormFile ImageProduct, IWebHostEnvironment webHostEnvironment)
        {
            // Faz uma cópia da imagem para um stream em memória.
            var ms = new MemoryStream();
            await ImageProduct.CopyToAsync(ms);

            // Carrega o stream em memória para o objeto de processamento de imagens.
            ms.Position = 0;
            var img = await Image.LoadAsync(ms);
            JpegEncoder jpegEncoder = new JpegEncoder();
            //PngEncoder pngEncoder = new PngEncoder();
            jpegEncoder.Quality = 100;
            img.SaveAsJpeg(ms, jpegEncoder);
            ms.Position = 0;
            img = await Image.LoadAsync(ms);
            ms.Close();
            ms.Dispose();

            // Cria um retângulo de recorte para deixar imagem quadrada
            var size = img.Size();
            Rectangle rectangle;

            if (size.Width > size.Height)
            {
                float x = (size.Width - size.Height) / 2.0F;
                rectangle = new Rectangle((int)x, 0, size.Height, size.Height);
            }
            else
            {
                float y = (size.Height - size.Width) / 2.0F;
                rectangle = new Rectangle((int)y, 0, size.Width, size.Width);
            }

            // Recorta a imagem com base no rectangle computado
            img.Mutate(i => i.Crop(rectangle));

            // Monta o caminho da da imagem (~/img/product/000000.jpeg)
            var pathImage = Path.Combine(webHostEnvironment.WebRootPath, "img\\product", productId.ToString("D6") + ".jpg");

            // Cria um arquivo de imagem sobreescrevendo o existente, caso já exista um
            await img.SaveAsync(pathImage);
        }
    }
}
