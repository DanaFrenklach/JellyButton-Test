using UnityEngine;

namespace JellyButton.Exam.Core.ObjectPooling
{
    /// <summary>
    /// An abstract class that any pooled object inherits from.
    /// </summary>
    public abstract class Pooled : MonoBehaviour
    {
        public IPooler Pooler { get; set; }
        public Transform Transform { get; private set; }

        private void Awake()
        {
            Transform = transform;
        }

        public void Release()
        {
            Pooler?.Release(this);
        }
    }
}