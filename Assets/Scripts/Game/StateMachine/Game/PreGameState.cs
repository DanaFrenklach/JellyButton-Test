using JellyButton.Exam.Core.Data;
using UnityEngine;

namespace JellyButton.Exam.Game.StateMachine.Game
{
    public class PreGameState : GameState
    {
        public PreGameState(GameManager stateMachine) : base(stateMachine)
        {
        }

        public override void Start()
        {
            base.Start();
            GameStateEvents.OnPreGame();
        }

        public override void Update()
        {
            if (Input.anyKeyDown)
            {
                StateMachine.OnInGame();
            }
        }
    }
}