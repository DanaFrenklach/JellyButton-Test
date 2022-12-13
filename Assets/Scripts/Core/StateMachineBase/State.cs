using System;
using UnityEngine;

namespace JellyButton.Exam.Core.StateMachineBase
{
    /// <summary>
    /// Encapsulate the basic functionality needed for the states of the state machine.
    /// Provides basic fields and methods for the various stages of a state's life cycle.
    /// - A substate is used in case any state needs to handle states within itself.
    /// - A superstate is used as the "parent" of the substate.
    /// </summary>
    /// <typeparam name="T">The state machine type, to which the state belongs</typeparam>
    [Serializable]
    public abstract class State<T> where T : class
    {
        protected readonly T StateMachine;
        protected State<T> CurrentSuperState;
        protected State<T> CurrentSubState;

        protected State(T stateMachine)
        {
            StateMachine = stateMachine;
        }
        
        public virtual void Start()
        {
            #if UNITY_EDITOR
            Debug.Log($"<color=green>{GetType().Name} On</color>");
            #endif
        }

        public virtual void Update()
        {
        }

        public virtual void End()
        {
            #if UNITY_EDITOR
            Debug.Log($"<color=red>{GetType().Name} Off</color>");
            #endif
        }

        protected void SetSuperState(State<T> state)
        {
            CurrentSuperState = state;
        }
        
        public void SetSubState(State<T> state)
        {
            CurrentSubState?.End();
            CurrentSubState = state;
            state.SetSuperState(this);
            CurrentSubState.Start();
        }
    }
}
