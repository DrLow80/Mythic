using CSharpFunctionalExtensions;

namespace Mythic.WPF.Printing
{
    public class MythicEventScene
    {
        private MythicEventScene(string eventFocus, string eventAction, string eventSubject)
        {
            EventFocus = eventFocus;
            EventAction = eventAction;
            EventSubject = eventSubject;
        }

        public string EventAction { get; }
        public string EventFocus { get; }
        public string EventSubject { get; }

        public static Result<MythicEventScene> Build(string eventFocus, string eventAction, string eventSubject)
        {
            var mythicEventScene = new MythicEventScene(eventFocus, eventAction, eventSubject);

            return Result.Ok(mythicEventScene);
        }
    }
}