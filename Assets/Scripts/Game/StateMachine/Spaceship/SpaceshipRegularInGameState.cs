using UnityEngine;

namespace JellyButton.Exam.Game.StateMachine.Spaceship
{
    public class SpaceshipRegularInGameState : SpaceshipState
    {
        public SpaceshipRegularInGameState(SpaceshipController stateMachine) : base(stateMachine)
        {
        }

        public override void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                CurrentSuperState.SetSubState(new SpaceshipBoostedInGameState(StateMachine));
            }
        }
    }
}
