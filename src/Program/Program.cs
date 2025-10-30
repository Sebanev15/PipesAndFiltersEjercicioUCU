using System;
using System.IO;
using CompAndDel.Pipes;
using CompAndDel.Filters;

namespace CompAndDel
{
    class Program
    {
        static void Main(string[] args)
        {
            PipeNull pipeNull = new PipeNull();

            PipeSerial pipeTwitter = new PipeSerial(new FilterTwitter("Hay una cara en esta foto"), pipeNull);
            
            PipeSerial pipeNegative = new PipeSerial(new FilterNegative(), pipeNull);

            FilterConditional conditionalFilter = new FilterConditional();
            PipeConditionalFork pipeConditional = new PipeConditionalFork(conditionalFilter, pipeTwitter, pipeNegative);
            
            PipeSerial pipeGreyscale = new PipeSerial(new FilterGreyscale(), pipeConditional);

            PictureProvider provider = new PictureProvider();
            IPicture picture = provider.GetPicture(@"luke.jpg");

            IPicture result = pipeGreyscale.Send(picture);
            provider.SavePicture(result, @"luke-final.jpg");
            Console.WriteLine(conditionalFilter.HasFace);
        }
    }
}
