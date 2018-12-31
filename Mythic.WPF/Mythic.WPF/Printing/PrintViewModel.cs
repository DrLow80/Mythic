using System;
using System.Collections.ObjectModel;
using Mythic.Feature.Engine;
using Mythic.WPF.Project;

namespace Mythic.WPF.Printing
{
    public class PrintViewModel : BaseViewModel
    {
        private readonly IPrintRepository _printRepository;

        public PrintViewModel(IPrintRepository printRepository)
        {
            _printRepository = printRepository;
        }

        public ObservableCollection<MythicEvent> MythicEvents { get; } = new ObservableCollection<MythicEvent>();

        public ObservableCollection<MythicFate> MythicFates { get; } = new ObservableCollection<MythicFate>();

        public void Load()
        {
            var generateRequest = GenerateRequest.Factory();

            var generateResponse = _printRepository.Generate(generateRequest);

            if (generateResponse.IsFailure) throw new InvalidOperationException(generateResponse.Error);

            MythicFates.Clear();

            foreach (var mythicFate in generateResponse.Value.MythicFates) MythicFates.Add(mythicFate);

            MythicEvents.Clear();

            foreach (var mythicEvent in generateResponse.Value.MythicEvents) MythicEvents.Add(mythicEvent);
        }
    }
}