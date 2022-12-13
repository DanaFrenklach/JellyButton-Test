using JellyButton.Exam.Core.Data;
using JellyButton.Exam.Core.GameEvents;
using UnityEditor;
using UnityEngine;

namespace Tests.PlayMode
{
    public static class PlayTestsReferences
    {
        #region Data Objects

        internal static GameData GameData => AssetDatabase.LoadAssetAtPath<GameData>("Assets/Objects/Data/Game Data.asset");
        internal static ScoreRules ScoreRules => AssetDatabase.LoadAssetAtPath<ScoreRules>("Assets/Objects/Data/Score Rules.asset");

        #endregion
        
        #region Prefabs
        internal static GameObject TestListener => AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Tests/TestListener.prefab");
        
        internal static GameObject SpaceshipPrefab => AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Entities/Spaceship.prefab");
        internal static GameObject AsteroidPrefab => AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Entities/Asteroid.prefab");
        internal static GameObject RoadTile => AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Entities/RoadTile.prefab");
        
        internal static GameObject ScoreManager => AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/UI/UI Elements/Score.prefab");

        #endregion

        #region Game Events

        public static GameEvent OnHitAsteroid => AssetDatabase.LoadAssetAtPath<GameEvent>("Assets/Objects/Game Events/OnHitAsteroid.asset");
        public static GameEvent OnOneSecondPassed => AssetDatabase.LoadAssetAtPath<GameEvent>("Assets/Objects/Game Events/OnOneSecondPassed.asset");
        public static GameEvent OnPassedAsteroid => AssetDatabase.LoadAssetAtPath<GameEvent>("Assets/Objects/Game Events/OnPassedAsteroid.asset");

        #endregion

    }
}
