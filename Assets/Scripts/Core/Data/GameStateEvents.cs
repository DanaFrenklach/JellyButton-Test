namespace JellyButton.Exam.Core.Data
{
    /// <summary>
    /// Manages the global game state events.
    /// </summary>
    public static class GameStateEvents
    {
        public delegate void GlobalStateEvents();
        public static event GlobalStateEvents PreGameState;
        public static event GlobalStateEvents InGameState;
        public static event GlobalStateEvents GameOverState;
        public static event GlobalStateEvents RestartGame;
        
        public static void OnPreGame()
        {
            PreGameState?.Invoke();
        }
        
        public static void OnInGame()
        {
            InGameState?.Invoke();
        }
        
        public static void OnGameOver()
        {
            GameOverState?.Invoke();
        }

        public static void OnRestartGame()
        {
            RestartGame?.Invoke();
        }
    }
}
