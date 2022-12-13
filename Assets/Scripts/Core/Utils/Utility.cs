using UnityEngine;

namespace JellyButton.Exam.Core.Utils
{
    public static class Utility
    {
        public static void Log(this MonoBehaviour monoBehaviour, string message)
        {
            #if UNITY_EDITOR
            Debug.Log(message, monoBehaviour.gameObject);
            #endif
        }

        // In the format of 00
        public static string NumberToTimeString(int number)
        {
            return $"{(number < 10 ? "0" : "")}{number}";
        }
        
        public enum GameCanvases
        {
            None,
            PreGame,
            InGame,
            GameOver
        }

        #region Tags

        public const string ObstacleTag = "Obstacle";

        #endregion
    }
}
