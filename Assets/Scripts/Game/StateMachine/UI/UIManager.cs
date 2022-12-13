using JellyButton.Exam.Core.Data;
using JellyButton.Exam.Core.StateMachineBase;
using JellyButton.Exam.Core.Utils;
using UnityEngine;

namespace JellyButton.Exam.Game.StateMachine.UI
{
    public class UIManager : StateMachine<UIManager>
    {
        [SerializeField] private Canvas _preGameCanvas;
        [SerializeField] private Canvas _inGameCanvas;
        [SerializeField] private Canvas _gameOverCanvas;
        [SerializeField] private DisableLayoutGroup _gameOverDisableLayoutGroup;
        
        private Utility.GameCanvases _currentCanvas;
        public DisableLayoutGroup GameOverDisableLayoutGroup => _gameOverDisableLayoutGroup;

        private void Awake()
        {
            _preGameCanvas.enabled = true;
            _gameOverCanvas.enabled = false;
            _inGameCanvas.enabled = false;
        }

        private void OnEnable()
        {
            GameStateEvents.PreGameState += PreGameState;
            GameStateEvents.InGameState += InGameState;
            GameStateEvents.GameOverState += GameOverStateScreen;
        }

        private void OnDisable()
        {
            GameStateEvents.PreGameState -= PreGameState;
            GameStateEvents.InGameState -= InGameState;
            GameStateEvents.GameOverState -= GameOverStateScreen;
        }

        #region Switch States Methods

        private void PreGameState()
        {
            SetState(new UIPreGameState(this));
        }
        
        private void InGameState()
        {
            SetState(new UIInGameState(this));
        }

        private void GameOverStateScreen()
        {
            SetState(new UIGameOverState(this));
        }

        #endregion
        
        internal void TurnOnRelevantCanvas(Utility.GameCanvases gameCanvases)
        {
            _currentCanvas = gameCanvases;
            _preGameCanvas.enabled = _currentCanvas == Utility.GameCanvases.PreGame;
            _inGameCanvas.enabled = _currentCanvas == Utility.GameCanvases.InGame;
            _gameOverCanvas.enabled = _currentCanvas == Utility.GameCanvases.GameOver;
        }
        
        public void OnClickStartOver()
        {
            GameStateEvents.OnRestartGame();
        }
    }
}