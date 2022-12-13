using JellyButton.Exam.Core.ObjectPooling;
using UnityEngine;

namespace JellyButton.Exam.Game.Road
{
    /// <summary>
    /// Responsible for endlessly pooling the road tiles.
    /// </summary>
    public class RoadManager : ObjectPoolManager, IPooler
    {
        private Transform _transform;
        private const int RoadTileSize = 10;

        protected override void Awake()
        {
            base.Awake();

            IPooler = this;
            _transform = transform;
            GenerateRoad();
        }

        private void GenerateRoad()
        {
            for (int i = 0; i < _defaultPoolCapacity; i++)
            {
                Pool.Get();
            }
        }

        #region Pooling Methods

        protected override void OnGetObject(Pooled obj)
        {
            obj.gameObject.SetActive(true);
            
            // Either calculate the position the first one, or get the position of the last one
            var recentPos = RecentObject == null ? _transform.position - Vector3.forward * _defaultPoolCapacity : RecentObject.Transform.position;
            var newPos = recentPos + Vector3.forward * RoadTileSize;
            obj.Pooler = this;
            obj.Transform.position = newPos;
            RecentObject = obj;
        }

        public void Release(Pooled go)
        {
            Pool.Release(go);
            Pool.Get();
        }

        #endregion
    }
}
