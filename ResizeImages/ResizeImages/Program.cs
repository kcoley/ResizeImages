using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
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
        private static void ResizeImage(string imageURL, int width, int height)
        {
            // var resultImage = "output.png";
            // var image = Image.Load(imageURL);
            // image.Mutate(ctx => ctx.Resize(width, height));
            // image.Save(resultImage);
        }

        public static void ResizeImages(string manifestFile, int width, int height)
        {
            using (StreamReader stream = new StreamReader(manifestFile))
            {
                var jsonData = stream.ReadToEnd();
                dynamic items = JsonConvert.DeserializeObject(jsonData);

                foreach(var item in items)
                {
                    Console.WriteLine(item);
                    Console.WriteLine("///");
                }
            }

        }

        private static int FailWithMessage(string message)
        {
            Console.WriteLine(message);
            return -1;
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
                    var manifestFile = manifestOption.Value();
                    if (File.Exists(manifestFile))
                    {
                        if (widthOption.HasValue())
                        {
                            var width = 0;
                            if (Int32.TryParse(widthOption.Value(), out width))
                            {
                                if (heightOption.HasValue())
                                {
                                    var height = 0;
                                    if (Int32.TryParse(heightOption.Value(), out height))
                                    {
                                        Console.WriteLine(manifestOption.Value());
                                        ResizeImages(manifestOption.Value(), Int32.Parse(widthOption.Value()), Int32.Parse(heightOption.Value()));
                                    }
                                    else
                                    {
                                        return FailWithMessage("Height value is not an integer!");
                                    }
                                }
                                else
                                {
                                    return FailWithMessage("Height is missing!");
                                }
                            }
                            else
                            {
                                return FailWithMessage("Width value is not an integer!");
                            }
                        }
                        else
                        {
                            return FailWithMessage("Width is missing!");
                        }
                    }
                    else
                    {
                        return FailWithMessage("Manifest file not found!");
                    }
                }
                else
                {
                    return FailWithMessage("Missing glTF manifest file!");
                }

                Console.WriteLine("Hello World");
                return 0;
            });


            return app.Execute(args);
        }
    }
}
