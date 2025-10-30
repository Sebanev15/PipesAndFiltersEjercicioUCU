using Ucu.Poo.Cognitive;

namespace CompAndDel.Filters;



public class FilterConditional : IFilter
{
    private CognitiveFace cognitiveFace = new CognitiveFace(true, System.Drawing.Color.Red);

    public bool HasFace { get; private set; }

    public IPicture Filter(IPicture image)
    {
        string tempPath = "temp.jpg";

        PictureProvider provider = new PictureProvider();
        provider.SavePicture(image, tempPath);

        cognitiveFace.Recognize(tempPath);
        HasFace = cognitiveFace.FaceFound;

        return image;
    }
}