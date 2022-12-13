using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace JellyButton.Exam.Core.Utils
{
    /// <summary>
    /// For performance purposes.
    /// </summary>
    [RequireComponent(typeof(LayoutGroup))]
    public class DisableLayoutGroup : MonoBehaviour
    {
        private LayoutGroup _layoutGroup;
        
        private void Awake()
        {
            _layoutGroup = GetComponent<LayoutGroup>();
        }

        private void Start()
        {
            StartCoroutine(DelayedDisabling());
        }

        private IEnumerator DelayedDisabling()
        {
            yield return null;
            _layoutGroup.enabled = false;
        }

        public void EnableForOneFrame()
        {
            _layoutGroup.enabled = true;
            StartCoroutine(DelayedDisabling());
        }
    }
}
