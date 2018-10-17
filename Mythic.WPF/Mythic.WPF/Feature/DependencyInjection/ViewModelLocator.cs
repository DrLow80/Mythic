using Mythic.WPF.Printing;
using Mythic.WPF.Project;

namespace Mythic.WPF.Feature.DependencyInjection
{
    public class ViewModelLocator
    {
        public static ApplicationViewModel ApplicationViewModel =>
            SpringContext.GetObject<ApplicationViewModel>(nameof(Project.ApplicationViewModel));

        public static PrintViewModel PrintViewModel =>
            SpringContext.GetObject<PrintViewModel>(nameof(Printing.PrintViewModel));
    }
}