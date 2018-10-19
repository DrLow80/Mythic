using CSharpFunctionalExtensions;

namespace Mythic.WPF.Printing
{
    public class PrintRepository : IPrintRepository
    {
        public Result<GenerateResponse> Generate(GenerateRequest generateRequest)
        {
            return GenerateResponse.Build(generateRequest);
        }
    }
}