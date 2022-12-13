namespace JellyButton.Exam.Game.StateMachine.Spaceship
{
    public class SpaceshipGameOverState : SpaceshipState
    {
        public SpaceshipGameOverState(SpaceshipController stateMachine) : base(stateMachine)
        {
        }

        public override void Start()
        {
            base.Start();
            StateMachine.ExplodeSpaceshipShip();
        }
    }
}
