using UnityEngine;

namespace JellyButton.Exam.Core.Data
{
    /// <summary>
    /// Basically gets used for the UI.
    /// </summary>
    public class GameData : ScriptableObject
    {
        public int CurrentScore { get; set; }
        public int HighScore { get; set; }
        public bool DidBeatHighScore { get; set; }
        public int AsteroidsPassed { get; set; }

        // x - minutes, y - seconds
        public Vector2Int CurrentTime { get; set; } 

        private void OnEnable()
        {
            CurrentScore = 0;
            AsteroidsPassed = 0;
        }
    }
}
