using System;
using SixLabors.ImageSharp;
using McMaster.Extensions.CommandLineUtils;

namespace ResizeImages
{
    internal class Program
    {
        /// <summary>
        /// Resize the image to the specified width and height.
        /// </summary>
        /// <param name="imageURL">The image to resize.</param>
        /// <param name="width">The width to resize to.</param>
        /// <param name="height">The height to resize to.</param>
        /// <returns>The resized image.</returns>
        public static void ResizeImages(string imageURL, int width, int height)
        {
            var resultImage = "output.png";
            var image = Image.Load(imageURL);
            image.Mutate(ctx => ctx.Resize(width, height));
            image.Save(resultImage);
        }

        static int Main(string[] args)
        {
            var app = new CommandLineApplication(throwOnUnexpectedArg: false);
            app.Name = "ResizeImages";
            app.HelpOption("-?|-h|--help");
            var manifestOption = app.Option("-m|--manifest <MANIFEST>", "The path to the glTF manifest file.", CommandOptionType.SingleValue);
            var widthOption = app.Option("-w|--width <WIDTH>", "The width of the resized images.", CommandOptionType.SingleValue);
            var heightOption = app.Option("-h|--height <HEIGHT>", "The height of the resized images.", CommandOptionType.SingleValue);

            app.OnExecute(() =>
            {
                if (manifestOption.HasValue())
                {
                    Console.WriteLine(manifestOption.Value());
                }
                
                Console.WriteLine("Hello World");
                return 0;
            });

          
            return app.Execute(args);
        }
    }
}
