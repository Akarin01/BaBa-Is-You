using System;
using System.Diagnostics;

namespace BabaIsYou
{
    public static class GameMode
    {
        public static event Action OnGameWin;

        public static void WinGame()
        {
            OnGameWin?.Invoke();
        }
    }
}
