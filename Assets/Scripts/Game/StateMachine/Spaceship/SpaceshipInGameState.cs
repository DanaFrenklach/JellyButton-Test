namespace JellyButton.Exam.Game.StateMachine.Spaceship
{
    public class SpaceshipInGameState : SpaceshipState
    {
        public SpaceshipInGameState(SpaceshipController stateMachine) : base(stateMachine)
        {
        }

        public override void Start()
        {
            base.Start();
            SetSubState(new SpaceshipRegularInGameState(StateMachine));
        }

        public override void End()
        {
            base.End();
            CurrentSubState.End();
        }

        public override void Update()
        {
            CurrentSubState.Update();
            StateMachine.ReadInput();
        }
    }
}
