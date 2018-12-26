using System.Linq;
using CSharpFunctionalExtensions;

namespace Mythic.Feature.Engine
{
    public class MythicEvent
    {
        public const string Altered = "Altered";

        public const string Interrupted = "Interrupted";

        public const string Normal = "Normal";

        private static readonly string[] Actions =
        {
            "Attainment",
            "Starting",
            "Neglect",
            "Fight",
            "Recruit",
            "Triumph",
            "Violate",
            "Oppose",
            "Malice",
            "Communicate",
            "Persecute",
            "Increase",
            "Decrease",
            "Abandon",
            "Gratify",
            "Inquire",
            "Antagonise",
            "Move",
            "Waste",
            "Truce",
            "Release",
            "Befriend",
            "Judge",
            "Desert",
            "Dominate",
            "Procrastinate",
            "Praise",
            "Separate",
            "Take",
            "Break",
            "Heal",
            "Delay",
            "Stop",
            "Lie",
            "Return",
            "Immitate",
            "Struggle",
            "Inform",
            "Bestow",
            "Postpone",
            "Expose",
            "Haggle",
            "Imprison",
            "Release",
            "Celebrate",
            "Develop",
            "Travel",
            "Block",
            "Harm",
            "Debase",
            "Overindulge",
            "Adjourn",
            "Adversity",
            "Kill",
            "Disrupt",
            "Usurp",
            "Create",
            "Betray",
            "Agree",
            "Abuse",
            "Oppress",
            "Inspect",
            "Ambush",
            "Spy",
            "Attach",
            "Carry",
            "Open",
            "Carelessness",
            "Ruin",
            "Extravagance",
            "Trick",
            "Arrive",
            "Propose",
            "Divide",
            "Refuse",
            "Mistrust",
            "Deceive",
            "Cruelty",
            "Intolerance",
            "Trust",
            "Excitement",
            "Activity",
            "Assist",
            "Care",
            "Negligence",
            "Passion",
            "Workhard",
            "Control",
            "Attract",
            "Failure",
            "Pursue",
            "Vengeance",
            "Proceedings",
            "Dispute",
            "Punish",
            "Guide",
            "Transform",
            "Overthrow",
            "Oppress",
            "Change"
        };

        private static readonly string[] Subjects =
        {
            "Goals",
            "Dreams",
            "Environment",
            "Outside",
            "Inside",
            "Reality",
            "Allies",
            "Enemies",
            "Evil",
            "Good",
            "Emotions",
            "Opposition",
            "War",
            "Peace",
            "Theinnocent",
            "Love",
            "Thespiritual",
            "Theintellectual",
            "Newideas",
            "Joy",
            "Messages",
            "Energy",
            "Balance",
            "Tension",
            "Friendship",
            "Thephysical",
            "Aproject",
            "Pleasures",
            "Pain",
            "Possessions",
            "Benefits",
            "Plans",
            "Lies",
            "Expectations",
            "Legalmatters",
            "Bureaucracy",
            "Business",
            "Apath",
            "News",
            "Exteriorfactors",
            "Advice",
            "Aplot",
            "Competition",
            "Prison",
            "Illness",
            "Food",
            "Attention",
            "Success",
            "Failure",
            "Travel",
            "Jealousy",
            "Dispute",
            "Home",
            "Investment",
            "Suffering",
            "Wishes",
            "Tactics",
            "Stalemate",
            "Randomness",
            "Misfortune",
            "Death",
            "Disruption",
            "Power",
            "Aburden",
            "Intrigues",
            "Fears",
            "Ambush",
            "Rumor",
            "Wounds",
            "Extravagance",
            "Arepresentative",
            "Adversities",
            "Opulence",
            "Liberty",
            "Military",
            "Themundane",
            "Trials",
            "Masses",
            "Vehicle",
            "Art",
            "Victory",
            "Dispute",
            "Riches",
            "Statusquo",
            "Technology",
            "Hope",
            "Magic",
            "Illusions",
            "Portals",
            "Danger",
            "Weapons",
            "Animals",
            "Weather",
            "Elements",
            "Nature",
            "Thepublic",
            "Leadership",
            "Fame",
            "Anger",
            "Information"
        };

        private MythicEvent(MythicEventScene mythicEventScene, string status,
            MythicEventScene interruptMythicEventScene = null)
        {
            MythicEventScene = mythicEventScene;
            InterruptMythicEventScene = interruptMythicEventScene;
            Status = status;
        }

        public MythicEventScene InterruptMythicEventScene { get; }

        public MythicEventScene MythicEventScene { get; }

        public string Status { get; }

        public static Result<MythicEvent> Build(int chaos)
        {
            var mythicEventScene = GetMythicEventScene();

            if (mythicEventScene.IsFailure) return Result.Fail<MythicEvent>(mythicEventScene.Error);

            var status = GetStatus(chaos);

            if (status == Interrupted)
            {
                var interruptedScene = GetMythicEventScene();

                if (interruptedScene.IsFailure) return Result.Fail<MythicEvent>(interruptedScene.Error);

                var mythicEvent = new MythicEvent(mythicEventScene.Value, status, interruptedScene.Value);

                return Result.Ok(mythicEvent);
            }
            else
            {
                var mythicEvent = new MythicEvent(mythicEventScene.Value, status);

                return Result.Ok(mythicEvent);
            }
        }

        public static Result<MythicEventScene> GetMythicEventScene()
        {
            var eventFocus = GetEventFocus();

            var eventAction = GetEventAction();

            var eventSubject = GetEventSubject();

            return MythicEventScene.Build(eventFocus, eventAction, eventSubject);
        }

        private static string GetEventAction(DiceRoll diceRoll = null)
        {
            diceRoll = diceRoll ?? DiceRoll.Rolld100();

            return Actions.ElementAt(diceRoll.Value);
        }

        private static string GetEventFocus(DiceRoll diceRoll = null)
        {
            diceRoll = diceRoll ?? DiceRoll.Rolld100();

            if (diceRoll.Value < 7) return "Remote event";

            if (diceRoll.Value < 28) return "NPC action";

            if (diceRoll.Value < 35) return "Introduce a new NPC";

            if (diceRoll.Value < 45) return "Move toward a thread";

            if (diceRoll.Value < 52) return "Move away from a thread";

            if (diceRoll.Value < 55) return "Close a thread";

            if (diceRoll.Value < 67) return "PC negative";

            if (diceRoll.Value < 75) return "PC positive";

            if (diceRoll.Value < 83) return "Ambiguous event";

            if (diceRoll.Value < 92) return "NPC negative";

            return "NPC positive";
        }

        private static string GetEventSubject(DiceRoll diceRoll = null)
        {
            diceRoll = diceRoll ?? DiceRoll.Rolld100();

            return Subjects.ElementAt(diceRoll.Value);
        }

        private static string GetStatus(int chaos, DiceRoll diceRoll = null)
        {
            diceRoll = diceRoll ?? DiceRoll.Rolld10();

            if (diceRoll.Value > chaos) return Normal;

            if (diceRoll.Value % 2 == 0) return Interrupted;

            return Altered;
        }
    }
}