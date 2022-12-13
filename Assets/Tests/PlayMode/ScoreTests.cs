using System.Collections;
using JellyButton.Exam.Core.Data;
using JellyButton.Exam.Game.UI;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.PlayMode
{
    public class ScoreTests
    {
        private ScoreManager _scoreManager;
        private int _initialScore;

        [SetUp]
        public void SetUp()
        {
            InitializeScoreManager();
            _initialScore = PlayTestsReferences.GameData.CurrentScore;
        }

        [TearDown]
        public void TearDown()
        {
            Object.Destroy(_scoreManager);
        }
    
        private void InitializeScoreManager()
        {
            _scoreManager = Object.Instantiate(PlayTestsReferences.ScoreManager).GetComponent<ScoreManager>();
        }
    
        [UnityTest]
        public IEnumerator Seconds_Passed_Add_Points_According_To_Rules_Regular()
        {
            yield return null;
            GlobalData.IsSpeeding = false;
            PlayTestsReferences.OnOneSecondPassed.Raise();

            var expectedResult = _initialScore + PlayTestsReferences.ScoreRules.FlyingPerSecond;
            Assert.AreEqual(expectedResult, PlayTestsReferences.GameData.CurrentScore);
        }
        
        [UnityTest]
        public IEnumerator Seconds_Passed_Add_Points_According_To_Rules_Boost()
        {
            yield return null;     
            GlobalData.IsSpeeding = true;
            PlayTestsReferences.OnOneSecondPassed.Raise();

            var expectedResult = _initialScore + PlayTestsReferences.ScoreRules.BoostFlyingPerSecond;
            Assert.AreEqual(expectedResult, PlayTestsReferences.GameData.CurrentScore);
        }
        
        [UnityTest]
        public IEnumerator Passing_Asteroid_Adds_Points_According_To_Rules()
        {
            yield return null;
            PlayTestsReferences.OnPassedAsteroid.Raise();

            var expectedResult = _initialScore + PlayTestsReferences.ScoreRules.PassingAnAsteroid;
            Assert.AreEqual(expectedResult, PlayTestsReferences.GameData.CurrentScore);
        }
    }
}
