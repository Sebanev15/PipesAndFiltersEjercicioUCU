using System;
using System.IO;
using Ucu.Poo.Twitter;

namespace CompAndDel.Filters
{
    public class FilterTwitter : IFilter
    {
        private readonly string message;
        private readonly string tempPath;

        public FilterTwitter(string message, string tempPath = null)
        {
            this.message = message ?? throw new ArgumentNullException(nameof(message));
            // Asignar ruta temporal por defecto si no se provee
            this.tempPath = string.IsNullOrWhiteSpace(tempPath)
                ? Path.Combine(Path.GetTempPath(), $"temp_tweet_{Guid.NewGuid():N}.jpg")
                : tempPath;
        }

        public IPicture Filter(IPicture image)
        {
            PictureProvider provider = new PictureProvider();

            try
            {
                // Validación extra antes de llamar a SavePicture
                if (string.IsNullOrWhiteSpace(tempPath))
                    throw new InvalidOperationException("La ruta temporal para guardar la imagen es nula o vacía.");

                provider.SavePicture(image, tempPath);

                try
                {
                    TwitterImage twitter = new TwitterImage();
                    string tweetUrl = twitter.PublishToTwitter(message, tempPath);
                    Console.WriteLine($"La imagen se ha publicado correctamente: {tweetUrl}");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error publicando en Twitter: {e.Message}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error guardando imagen temporal o publicando: {ex.Message}");
            }
            finally
            {
                try
                {
                    if (!string.IsNullOrWhiteSpace(tempPath) && File.Exists(tempPath))
                        File.Delete(tempPath);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"No se pudo borrar archivo temporal '{tempPath}': {ex.Message}");
                }
            }

            return image;
        }
    }
}
