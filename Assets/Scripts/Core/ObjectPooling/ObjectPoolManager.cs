using System.Collections.Generic;
using JellyButton.Exam.Core.Utils;
using UnityEngine;
using UnityEngine.Pool;

namespace JellyButton.Exam.Core.ObjectPooling
{
    /// <summary>
    /// Responsible for managing the basic functionality of object pooling.
    /// It initializes the pool on awake, and forces some abstract methods needed for its initialization.
    /// </summary>
    public abstract class ObjectPoolManager : MonoBehaviour
    {
        [Header("Pool Settings")]
        [SerializeField] protected int _defaultPoolCapacity;
        [SerializeField] protected int _maxPoolSize;
        [SerializeField] protected GameObject _objectPrefab;
        // Doesn't actually get used, but rather copied to another private list
        [SerializeField] protected List<Pooled> _preexistingObjects;

        private List<Pooled> _initialPool;
        protected ObjectPool<Pooled> Pool;
        protected Pooled RecentObject;
        protected IPooler IPooler;

        protected virtual void Awake()
        {
            SetUpPool();
        }

        /// <summary>
        /// Initializes the object pool, copies the preexisting objects to a private list and turns them off.
        /// </summary>
        protected virtual void SetUpPool()
        {
            Pool = new ObjectPool<Pooled>(OnCreateObject, OnGetObject, OnReleaseObject, OnDestroyObject, false, _defaultPoolCapacity, _maxPoolSize);
            _initialPool = new List<Pooled>(_preexistingObjects);
            TurnOffPreexistingObjects();
            
            this.Log($"<color=orange>Initialized pool of type {GetType().Name}.</color>");
        }

        private void TurnOffPreexistingObjects()
        {
            if (_initialPool.Count <= 0) return;
            foreach (var preexistingUnit in _initialPool)
            {
                preexistingUnit.gameObject.SetActive(false);
            }
        }

        #region Object Pooling Methods

        /// <summary>
        /// Used by Unity's object pooling system.
        /// Responsible for creating a new object for the pool.
        /// If available, prefers using preexisting objects.
        /// If not, it instantiates a new one.
        /// </summary>
        /// <returns>Return the new object.</returns>
        private Pooled OnCreateObject()
        {
            Pooled obj;
            if (_initialPool.Count > 0)
            {
                obj = _initialPool[_initialPool.Count - 1];
                _initialPool.Remove(obj);
            }
            else
            {
                obj = Instantiate(_objectPrefab).GetComponent<Pooled>();
            }
            
            obj.Pooler = IPooler;
            obj.gameObject.SetActive(false);
            return obj;
        }
        
        private void OnReleaseObject(Pooled obj)
        {
            obj.gameObject.SetActive(false);
        }

        private void OnDestroyObject(Pooled obj)
        {
            Destroy(obj);
        }
        
        protected abstract void OnGetObject(Pooled obj);

        #endregion
    }
}