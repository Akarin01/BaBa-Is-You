using KitaFramework;

namespace BabaIsYou
{
    public class GameWinArgs : BaseEventArgs
    {
        public override int ID => EventID;

        public static int EventID = typeof(GameWinArgs).GetHashCode();

        public GameWinArgs() { }
    }
}