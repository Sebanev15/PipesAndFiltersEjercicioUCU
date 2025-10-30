using CompAndDel;
using System;
public class FilterSave : IFilter
{
    private string path;
    private PictureProvider provider;

    public FilterSave(string path)
    {
        this.path = path;
        this.provider = new PictureProvider();
    }

    public IPicture Filter(IPicture image)
    {
        provider.SavePicture(image, path);
        Console.WriteLine($"La imagen se guardo correctamente en: {path}");
        return image;
    }
}