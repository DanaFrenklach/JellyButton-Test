using JellyButton.Exam.Core.Utils;

namespace JellyButton.Exam.Game.StateMachine.UI
{
    public class UIPreGameState : UIState
    {
        public UIPreGameState(UIManager stateMachine) : base(stateMachine)
        {
        }

        public override void Start()
        {
            base.Start();
            StateMachine.TurnOnRelevantCanvas(Utility.GameCanvases.PreGame);
        }
    }
}
