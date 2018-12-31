using System.Collections.Generic;
using CSharpFunctionalExtensions;
using Mythic.Feature.Engine;

namespace Mythic.WPF.Printing
{
    public class GenerateResponse
    {
        private GenerateResponse(MythicEvent[] mythicEvents, MythicFate[] mythicFates)
        {
            MythicEvents = mythicEvents;
            MythicFates = mythicFates;
        }

        public MythicEvent[] MythicEvents { get; }
        public MythicFate[] MythicFates { get; }

        public static Result<GenerateResponse> Build(GenerateRequest generateRequest)
        {
            var mythicEvents = GetMythicEvents(generateRequest.Chaos, generateRequest.NumberOfMythicEvents);

            if (mythicEvents.IsFailure) return Result.Fail<GenerateResponse>(mythicEvents.Error);

            var mythicFates = GetMythicFates(generateRequest.Chaos, generateRequest.NumberOfMythicFates);

            if (mythicFates.IsFailure) return Result.Fail<GenerateResponse>(mythicFates.Error);

            var generateResponse = new GenerateResponse(mythicEvents.Value, mythicFates.Value);

            return Result.Ok(generateResponse);
        }

        public static Result<MythicEvent[]> GetMythicEvents(int chaos, int amount)
        {
            var mythicEvents = new List<MythicEvent>();

            for (var i = 0; i < amount; i++)
            {
                var result = MythicEvent.Build(chaos);

                if (result.IsFailure) return Result.Fail<MythicEvent[]>(result.Error);

                mythicEvents.Add(result.Value);
            }

            return Result.Ok(mythicEvents.ToArray());
        }

        private static Result<MythicFate[]> GetMythicFates(int chaos, int amount)
        {
            var mythicFates = new List<MythicFate>();

            for (var i = 0; i < amount; i++)
            {
                var result = MythicFate.Build(chaos, DiceRoll.Rolld100().Value);

                if (result.IsFailure) return Result.Fail<MythicFate[]>(result.Error);

                mythicFates.Add(result.Value);
            }

            return Result.Ok(mythicFates.ToArray());
        }
    }
}