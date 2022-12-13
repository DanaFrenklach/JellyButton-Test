using System.Collections;
using JellyButton.Exam.Core.GameEvents;
using JellyButton.Exam.Game.StateMachine.Spaceship;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.PlayMode
{
    public class SpaceshipTests
    {
        private SpaceshipController _spaceship;
        private GameObject _roadTile;
        private TestsListener _testListener;
        
        [SetUp]
        public void SetUp()
        {
            InitializeSpaceship();
            InitializeRoadTile();
        }

        [TearDown]
        public void TearDown()
        {
            Object.Destroy(_spaceship);
            Object.Destroy(_roadTile);
        }

        private void InitializeSpaceship()
        {
            _spaceship = Object.Instantiate(PlayTestsReferences.SpaceshipPrefab).AddComponent<SpaceshipController>();
            _spaceship.transform.position = Vector3.zero;
        }

        private void InitializeRoadTile()
        {
            _roadTile = Object.Instantiate(PlayTestsReferences.RoadTile);
            _roadTile.transform.position = new Vector3(0, -2, 0);
        }

        private void InitializeTestListener(GameEvent gameEvent)
        {
            _testListener = Object.Instantiate(PlayTestsReferences.TestListener).GetComponent<TestsListener>();
            _testListener.RegisterEvent(gameEvent);
        }

        [UnityTest]
        public IEnumerator Spaceship_Moves_Left()
        {
            
            var initialPos = _spaceship.transform.position;
            var direction = new Vector3(1, 0, 0);

            var currentTime = 0f;
            var testDuration = 1f;

            while (currentTime < testDuration)
            {
                currentTime += Time.deltaTime;
                _spaceship.Move(direction);
                yield return null;
            }

            Assert.Greater(_spaceship.transform.position.x, initialPos.x);
        }
        
        [UnityTest]
        public IEnumerator Spaceship_Moves_Right()
        {
            Object.Instantiate(PlayTestsReferences.RoadTile).transform.position = new Vector3(0, -2, 0);
            var initialPos = _spaceship.transform.position;
            var direction = new Vector3(-1, 0, 0);

            var currentTime = 0f;
            var testDuration = 1f;

            while (currentTime < testDuration)
            {
                currentTime += Time.deltaTime;
                _spaceship.Move(direction);
                yield return null;
            }

            Assert.Less(_spaceship.transform.position.x, initialPos.x);
        }
        
        [UnityTest]
        public IEnumerator Spaceship_Hits_Asteroid()
        {
            InitializeTestListener(PlayTestsReferences.OnHitAsteroid);
            Object.Instantiate(PlayTestsReferences.AsteroidPrefab);
            yield return new WaitForSeconds(0.1f);
            
            Assert.IsTrue(_testListener.WasRaised);
            Object.Destroy(_testListener);
        }
    }
}
