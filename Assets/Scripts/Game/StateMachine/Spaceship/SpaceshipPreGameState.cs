namespace JellyButton.Exam.Game.StateMachine.Spaceship
{
    public class SpaceshipPreGameState : SpaceshipState
    {
        public SpaceshipPreGameState(SpaceshipController stateMachine) : base(stateMachine)
        {
        }

        public override void Start()
        {
            base.Start();
            StateMachine.SetUpSpaceship();
        }
    }
}
