using System.IO;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

public static void Run(Stream myBlob, ILogger log, Stream outputBlob, ExecutionContext context)
{
    log.LogInformation($"C# Blob trigger function Processed blob Size: {myBlob.Length} Bytes");

    var logoPath = Path.Combine(context.FunctionDirectory, "azure_logo.jpg");
    var logoBytes = File.ReadAllBytes(logoPath);
    var logo = Image.Load(logoBytes);

    using (Image image = Image.Load(myBlob))
    {
        image.Mutate(x => x
            .Resize(new ResizeOptions
            {
                Mode = ResizeMode.BoxPad,
                Size = new Size(1000, 650)
            })
            .BackgroundColor(new Rgba32(0, 0, 0))
            .DrawImage(logo, new Point(1, 1), opacity: 0.5f)
        );

        image.Save(outputBlob, new JpegEncoder());
    }
}