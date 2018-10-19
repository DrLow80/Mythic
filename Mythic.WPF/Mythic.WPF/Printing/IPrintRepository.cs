using CSharpFunctionalExtensions;

namespace Mythic.WPF.Printing
{
    public interface IPrintRepository
    {
        Result<GenerateResponse> Generate(GenerateRequest generateRequest);
    }
}