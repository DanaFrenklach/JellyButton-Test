using JellyButton.Exam.Core.Data;
using JellyButton.Exam.Core.StateMachineBase;
using UnityEngine;

namespace JellyButton.Exam.Game.StateMachine.Game
{
    /// <summary>
    /// Responsible for the high-level state management of the game, as well as for the speed.
    /// </summary>
    public class GameManager : StateMachine<GameManager>
    {
        [Header("Speed Settings")]
        [SerializeField] private float _initialScrollSpeed = 30f;
        [SerializeField] private float _speedBoostMultiplier = 2f;
        [SerializeField] private float _speedDamp = 8f;
        [SerializeField] private float _speedBoostedDamp = 3f;
        
        private void Start()
        {
            OnStartGame();
        }

        private void OnEnable()
        {
            GameStateEvents.RestartGame += OnStartGame;
        }

        private void OnDisable()
        {
            GameStateEvents.RestartGame -= OnStartGame;
        }
        
        #region Game Event Listeners

        public void OnHitAsteroidListener()
        {
            OnGameOver();
        }

        #endregion

        #region Switch States Methods
        
        private void OnStartGame()
        {
            SetState(new PreGameState(this));
        }
        
        public void OnInGame()
        {
            SetState(new InGameState(this));
        }
        
        private void OnGameOver()
        {
            SetState(new GameOverState(this));
        }

        #endregion

        private void Update()
        {
            CurrentState.Update();
            CheckForQuitting();
            UpdateSpeed();
        }

        private void CheckForQuitting()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }
        }

        private void UpdateSpeed()
        {
            var desiredSpeed = (GlobalData.IsSpeeding ? _speedBoostMultiplier : 1) * _initialScrollSpeed;
            var desiredDamp = GlobalData.IsSpeeding ? _speedBoostedDamp : _speedDamp;
            GlobalData.CurrentScrollSpeed = Mathf.Lerp(GlobalData.CurrentScrollSpeed, desiredSpeed, desiredDamp * Time.deltaTime);
        }
    }
}
