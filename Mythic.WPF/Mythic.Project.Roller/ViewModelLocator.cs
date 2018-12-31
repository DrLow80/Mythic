using Mythic.Foundation.DependencyInjection;

namespace Mythic.Project.Roller
{
    public class ViewModelLocator
    {
        public static ApplicationViewModel ApplicationViewModel =>
            SpringContext.GetObject<ApplicationViewModel>(nameof(ApplicationViewModel));
    }
}