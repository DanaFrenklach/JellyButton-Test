using UnityEngine;

namespace JellyButton.Exam.Core.StateMachineBase
{
    /// <summary>
    /// Every state machine will be inheriting from this,
    /// which encapsulates some basic functionality needed for a state machine.
    /// </summary>
    /// <typeparam name="T">The type of the class inheriting from the state machine.</typeparam>
    public abstract class StateMachine<T> : MonoBehaviour where T : class
    {
        protected State<T> CurrentState;

        protected void SetState(State<T> state)
        {
            CurrentState?.End();
            CurrentState = state;
            CurrentState.Start();
        }
    }
}
