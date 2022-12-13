using UnityEngine;

namespace JellyButton.Exam.Core.Data
{
    public class ScoreRules : ScriptableObject
    {
        [SerializeField] private int _flyingPerSecond = 1;
        [SerializeField] private int _boostFlyingPerSecond = 2;
        [SerializeField] private int _passingAnAsteroid = 5;

        public int FlyingPerSecond => _flyingPerSecond;
        public int BoostFlyingPerSecond => _boostFlyingPerSecond;
        public int PassingAnAsteroid => _passingAnAsteroid;
    }
}
