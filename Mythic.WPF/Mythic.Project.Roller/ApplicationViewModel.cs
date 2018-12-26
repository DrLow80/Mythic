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
        public int Chaos { get; set; } = 5;

        public ICommand DecreaseChaosCommand => new RelayCommand(DecreaseChaos, CanDecreaseChaos);
        public ICommand IncreaseChaosCommand => new RelayCommand(IncreaseChaos, CanIncreaseChaos);

        public const int MinChaos = 1;
        public const int MaxChaos = 9;

        private bool CanIncreaseChaos(object obj)
        {
            return Chaos < MaxChaos;
        }

        private void IncreaseChaos(object obj)
        {
            Chaos++;
        }

        private bool CanDecreaseChaos(object obj)
        {
            return Chaos > MinChaos;
        }

        private void DecreaseChaos(object obj)
        {
            Chaos--;
        }

        public ICommand AddRandomEventCommand => new RelayCommand(AddRandomEvent);

        private void AddRandomEvent(object obj)
        {
            var mythicEventResult = MythicEvent.Build(Chaos);

            if (mythicEventResult.IsFailure)
            {
                throw new ApplicationException(mythicEventResult.Error);
            }

            MythicEvent = mythicEventResult.Value;
        }

        public MythicEvent MythicEvent { get; set; }

        public ICommand RollFateCommand => new RelayCommand<string>(RollFate);

        private void RollFate(string obj)
        {
            DiceRoll diceRoll = DiceRoll.Rolld100().Value;

            var mythicFateResult = MythicFate.Build(Chaos, diceRoll);

            if (mythicFateResult.IsFailure)
            {
                throw new ApplicationException(mythicFateResult.Error);
            }

            FateResult = $"{diceRoll.Value}: {mythicFateResult.Value.FateResults.First(x => x.Name == obj).Value}";

            if (mythicFateResult.Value.HasRandomMythicEvent)
            {
                AddRandomEvent(this);
            }
        }

        public ObservableCollection<string> FateOdds { get; set; } = new ObservableCollection<string>(FateTable.FateOdds);

        public string FateResult { get; set; }
    }
}