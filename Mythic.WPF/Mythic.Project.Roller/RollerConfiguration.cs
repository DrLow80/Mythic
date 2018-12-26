using Spring.Context.Attributes;

namespace Mythic.Project.Roller
{
    [Configuration]
    public class RollerConfiguration
    {
        [Definition]
        public virtual ApplicationViewModel ApplicationViewModel()
        {
            return new ApplicationViewModel();
        }
    }
}