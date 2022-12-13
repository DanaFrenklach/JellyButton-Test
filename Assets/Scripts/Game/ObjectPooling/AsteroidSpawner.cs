using System.Collections;
using System.Collections.Generic;
using JellyButton.Exam.Core.Data;
using JellyButton.Exam.Core.ObjectPooling;
using UnityEngine;
using Random = UnityEngine.Random;

namespace JellyButton.Exam.Game.ObjectPooling
{
    /// <summary>
    /// Responsible for everything relating to asteroid spawning.
    /// </summary>
    public class AsteroidSpawner : ObjectPoolManager, IPooler
    {
        [SerializeField] private Transform[] _spawnTransforms;
        
        [Space][Header("Spawning Settings")]
        [SerializeField] private float _spawningFrequency = 0.2f;
        [Space] // odds for spawning asteroids
        [Tooltip("The higher the multiplier, the faster the odds for spawning grow")]
        [SerializeField] private float _spawningOddsMultiplier = 8f;
        [SerializeField] private float _initialOddsToSpawn = 20f;
        [SerializeField] private float _maxOddsToSpawn = 100f;
        [Space] // min distance between asteroids
        [SerializeField] private float _minDistance = 6f;
        [SerializeField] private float _initialMinDistance = 30f;

        private List<int> _availableSpawnTransformIndexes;
        private Transform _transform;
        private Coroutine _activeLoopRoutine;
        
        private float _oddsToSpawn;
        private float _currMinDistance;
        private float _spawningDelay;
        
        private bool _shouldSpawn;

        protected override void Awake()
        {
            base.Awake();
            
            IPooler = this;
        }

        private void OnEnable()
        {
            GameStateEvents.PreGameState += OnPreGameState;
            GameStateEvents.InGameState += OnInGameState;
            GameStateEvents.GameOverState += OnGameOverState;
        }

        private void OnDisable()
        {
            GameStateEvents.PreGameState -= OnPreGameState;
            GameStateEvents.InGameState -= OnInGameState;
            GameStateEvents.GameOverState -= OnGameOverState;
        }

        #region Event Methods

        private void OnPreGameState()
        {
            SetUpPool();
            RecentObject = null;
            _currMinDistance = _initialMinDistance;
            _oddsToSpawn = _initialOddsToSpawn;
            _spawningDelay = _spawningFrequency;
        }
        
        private void OnInGameState()
        {
            _shouldSpawn = true;
            _activeLoopRoutine = StartCoroutine(SpawnRoutine());
        }
        
        private void OnGameOverState()
        {
            _shouldSpawn = false;
            StopCoroutine(_activeLoopRoutine);
        }
        
        #endregion

        #region Initialization

        protected override void SetUpPool()
        {
            base.SetUpPool();
            InitializeAvailableSpawnTransforms();
        }

        /// <summary>
        /// Initializes the list of available spawn transform indexes.
        /// </summary>
        private void InitializeAvailableSpawnTransforms()
        {
            _availableSpawnTransformIndexes = new List<int>();
            for (var index = 0; index < _spawnTransforms.Length; index++)
            {
                _availableSpawnTransformIndexes.Add(index);
            }
        }

        #endregion

        #region Spawning

        /// <summary>
        /// The coroutine responsible for repeatedly calling the spawn methods,
        /// restricted by the spawning delay variable.
        /// </summary>
        /// <returns></returns>
        private IEnumerator SpawnRoutine()
        {
            while (_shouldSpawn)
            {
                yield return new WaitForSeconds(_spawningDelay);
                TryToSpawnAsteroid();
                IncreaseSpawnFrequency();
            }
        }

        private void TryToSpawnAsteroid()
        {
            var rndOdds = Random.Range(0, 101);
            var shouldSpawnAsteroid = rndOdds < _oddsToSpawn;
            if (!shouldSpawnAsteroid) return;
            
            UpdateAllowedSpawnPositions();
            if (CheckForAvailableSpawnTransforms())
            {
                Pool.Get();
            }
        }
        
        private bool CheckForAvailableSpawnTransforms()
        {
            return _availableSpawnTransformIndexes.Count > 0;
        }

        /// <summary>
        /// Responsible for clearing the existing list of available spawn transforms,
        /// and repopulating it according to the current min distance restrictions.
        /// </summary>
        private void UpdateAllowedSpawnPositions()
        {
            _availableSpawnTransformIndexes.Clear();

            if (_spawnTransforms.Length == 0)
            {
                Debug.LogWarning("Please reference Transforms using which new asteroids can get spawned.");
                var tempTransform = new GameObject().transform;
                tempTransform.position = _transform.position;
                _spawnTransforms = new[] {tempTransform};
            }

            var recentPos = RecentObject == null ? Vector3.zero : RecentObject.transform.position;
            for (var index = 0; index < _spawnTransforms.Length; index++)
            {
                var spawnTransform = _spawnTransforms[index];
                var isDistanceMoreThanMin =
                    Vector3.Distance(spawnTransform.position, recentPos) >=
                    _currMinDistance;
                if (!isDistanceMoreThanMin) continue;

                _availableSpawnTransformIndexes.Add(index);
            }
        }
        
        /// <summary>
        /// The spawn frequency is increased by:
        /// - increasing the odds for spawning a new asteroid.
        /// - reducing the min distance (up to a predefined threshold).
        /// - reducing the delay between spawns.
        /// </summary>
        private void IncreaseSpawnFrequency()
        {
            if (_oddsToSpawn < _maxOddsToSpawn)
            {
                _oddsToSpawn += Time.deltaTime * _spawningOddsMultiplier;
            }

            if (_currMinDistance > _minDistance)
            {
                _currMinDistance -= Time.deltaTime * 20;
            }
        }

        #endregion

        #region Pooling Methods

        protected override void OnGetObject(Pooled obj)
        {
            var rndSpawnTransformIndex = _availableSpawnTransformIndexes[Random.Range(0, _availableSpawnTransformIndexes.Count)];
            obj.transform.position = _spawnTransforms[rndSpawnTransformIndex].position;
            obj.Pooler = this;
            obj.gameObject.SetActive(true);
            RecentObject = obj;
        }
        
        /// <summary>
        /// Used by "outsiders" when there's a need to release an object.
        /// </summary>
        /// <param name="obj">The object to release.</param>
        public void Release(Pooled obj)
        {
            Pool.Release(obj);
        }

        #endregion
    }
}
