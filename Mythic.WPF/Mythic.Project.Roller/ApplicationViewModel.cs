using Mythic.Feature.Engine;
using PropertyChanged;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace Mythic.Project.Roller
{
    [AddINotifyPropertyChangedInterface]
    public class ApplicationViewModel
    {
        public Chaos Chaos { get; set; } = Feature.Engine.Chaos.Build(5).Value;

        public ICommand DecreaseChaosCommand => new RelayCommand(DecreaseChaos, CanDecreaseChaos);
        public ICommand IncreaseChaosCommand => new RelayCommand(IncreaseChaos, CanIncreaseChaos);

        private bool CanIncreaseChaos(object obj)
        {
            return Chaos < Chaos.Max;
        }

        private void IncreaseChaos(object obj)
        {
            Chaos = Chaos.Increment();
        }

        private bool CanDecreaseChaos(object obj)
        {
            return Chaos > Chaos.Min;
        }

        private void DecreaseChaos(object obj)
        {
            Chaos = Chaos.Decrement();
        }

        public ICommand AddRandomEventCommand => new RelayCommand(AddRandomEvent);

        private void AddRandomEvent(object obj)
        {
            var mythicEventResult = MythicEvent.Build(Chaos);

            if (mythicEventResult.IsFailure)
            {
                throw new ApplicationException(mythicEventResult.Error);
            }

            MythicEventScene = mythicEventResult.Value.MythicEventScene.ToString();

            Status = mythicEventResult.Value.Status;

            InterruptMythicEventScene = mythicEventResult.Value.InterruptMythicEventScene?.ToString();
        }

        //public MythicEvent MythicEvent { get; set; }

        public string MythicEventScene { get; set; }
        public string Status { get; set; }
        public string InterruptMythicEventScene { get; set; }

        public ICommand RollFateCommand => new RelayCommand<string>(RollFate);

        private void RollFate(string obj)
        {
            DiceRoll diceRoll = DiceRoll.Rolld100().Value;

            var mythicFateResult = MythicFate.Build(Chaos, diceRoll, obj);

            if (mythicFateResult.IsFailure)
            {
                throw new ApplicationException(mythicFateResult.Error);
            }

            FateResult = $"{diceRoll}: {mythicFateResult.Value.FateResults.First(x => x.Name == obj).Value}";

            if (mythicFateResult.Value.HasRandomMythicEvent)
            {
                AddRandomEvent(this);
            }
        }

        public ObservableCollection<string> FateOdds { get; set; } = new ObservableCollection<string>(FateTable.FateOdds);

        public string FateResult { get; set; }
    }
}