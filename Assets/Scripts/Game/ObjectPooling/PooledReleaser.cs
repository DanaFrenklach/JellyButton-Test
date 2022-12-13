using JellyButton.Exam.Core.ObjectPooling;
using JellyButton.Exam.Core.Utils;
using UnityEngine;

namespace JellyButton.Exam.Game.ObjectPooling
{
    /// <summary>
    /// Responsible for releasing pooled objects onTriggerEnter back to their pools.
    /// </summary>
    public class PooledReleaser : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            other.GetComponent<Pooled>()?.Release();
            this.Log($"{other.name} hit trigger wall.");
        }
    }
}
