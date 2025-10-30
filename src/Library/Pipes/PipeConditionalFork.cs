namespace CompAndDel.Pipes;
using CompAndDel.Filters;

public class PipeConditionalFork : IPipe
{
    private FilterConditional filter;
    private IPipe truePipe;
    private IPipe falsePipe;

    public PipeConditionalFork(FilterConditional filter, IPipe truePipe, IPipe falsePipe)
    {
        this.filter = filter;
        this.truePipe = truePipe;
        this.falsePipe = falsePipe;
    }

    public IPicture Send(IPicture picture)
    {
        IPicture result = this.filter.Filter(picture);

        if (filter.HasFace)
        {
            return this.truePipe.Send(result);
        }
        else
        {
            return this.falsePipe.Send(result);
        }
    }
}