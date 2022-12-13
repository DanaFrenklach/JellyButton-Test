using JellyButton.Exam.Core.Utils;

namespace JellyButton.Exam.Game.StateMachine.UI
{
    public class UIGameOverState : UIState
    {
        public UIGameOverState(UIManager stateMachine) : base(stateMachine)
        {
        }
        
        public override void Start()
        {
            base.Start();
            
            // In case the "Congratulations" turns on
            StateMachine.GameOverDisableLayoutGroup.EnableForOneFrame();
            StateMachine.TurnOnRelevantCanvas(Utility.GameCanvases.GameOver);
        }
    }
}
