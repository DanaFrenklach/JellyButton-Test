using System.Collections;
using JellyButton.Exam.Game;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.PlayMode
{
    public class ScrollerTests
    {
        private Scroller _scroller;
        
        [SetUp]
        public void SetUp()
        {
            _scroller = new GameObject().AddComponent<Scroller>();
        }

        [TearDown]
        public void TearDown()
        {
            Object.Destroy(_scroller.gameObject);
        }

        [UnityTest]
        public IEnumerator Scroller_Moves()
        {
            var scrollerTransform = _scroller.transform;
            var initialPos = scrollerTransform.position;
            yield return new WaitForSeconds(2);
            
            Assert.AreNotEqual(initialPos, scrollerTransform.position);
        }
    }
}
