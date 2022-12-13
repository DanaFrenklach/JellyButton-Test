using JellyButton.Exam.Core.Data;
using UnityEngine;

namespace JellyButton.Exam.Game.StateMachine.Spaceship
{
    public class SpaceshipBoostedInGameState : SpaceshipState
    {
        public SpaceshipBoostedInGameState(SpaceshipController stateMachine) : base(stateMachine)
        {
        }
        
        public override void Start()
        {
            base.Start();
            GlobalData.IsSpeeding = true;
        }

        public override void Update()
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                CurrentSuperState.SetSubState(new SpaceshipRegularInGameState(StateMachine));
            }
        }

        public override void End()
        {
            base.End();
            GlobalData.IsSpeeding = false;
        }
    }
}