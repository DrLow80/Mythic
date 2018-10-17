using Mythic.WPF.Printing;
using Mythic.WPF.Project;
using Spring.Context.Attributes;

namespace Mythic.WPF.Feature.DependencyInjection
{
    [Configuration]
    public class SpringConfiguration
    {
        [Definition]
        public virtual ApplicationViewModel ApplicationViewModel()
        {
            return new ApplicationViewModel();
        }

        [Definition]
        public virtual PrintViewModel PrintViewModel()
        {
            return new PrintViewModel();
        }
    }
}