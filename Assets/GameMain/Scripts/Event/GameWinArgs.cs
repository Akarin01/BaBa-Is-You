using KitaFramework;

namespace BabaIsYou
{
    public class GameWinArgs : BaseEventArgs
    {
        public override int Id => EventID;

        public static int EventID { get; private set; } = typeof(GameWinArgs).GetHashCode();

        public GameWinArgs() { }
    }
}