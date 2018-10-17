using Mythic.WPF.View.ViewModel;
using System.Windows.Input;

namespace Mythic.WPF.Project
{
    public class ApplicationViewModel : BaseViewModel
    {
        public ICommand NavigateCommand => new RelayCommand<BaseViewModel>(OnNavigate);

        public BaseViewModel CurrentViewModel { get; set; }

        private void OnNavigate(BaseViewModel obj)
        {
            CurrentViewModel = obj;
        }
    }
}