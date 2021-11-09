using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UKitchen.Pooling
{
    public class AbsPrefabPooling<T, TFactory> : IDisposable where T : AbsPrefabItem<T> where TFactory : AbsPrefabItem<T>.Factory
    {
        private List<T> _pooledObjects;

        //sample of the actual object to store
        //used if we need to grow the list
        private TFactory _factory;

        //maximum number of objects to have in the list.
        private int _maxPoolSize;

        //initial and default number of objects to have in the list
        private int _initialPoolSize;

        private bool _willGrow;

        private Transform _poolTarget;

        private bool _alreadyDisposed = false;

        public AbsPrefabPooling(TFactory factory, Transform poolTarget, int maxPoolSize, int initialPoolSize, bool willGrow = false )
        {
            _factory = factory;
            _maxPoolSize = maxPoolSize;
            _initialPoolSize = initialPoolSize;
            _willGrow = willGrow;
            _poolTarget = poolTarget;

            _pooledObjects ??= new List<T>();

            for (int i = 0; i < _initialPoolSize; i++)
            {
                var o = factory.Create(poolTarget);
                _pooledObjects.Add(o);
            }
        }

        public IPrefabItem GetItem()
        {
            var item = _pooledObjects.FirstOrDefault(s => s.gameObject.activeSelf == false);

            if (item != null)
            {
                return item;
            }

            if (_willGrow || _maxPoolSize > _pooledObjects.Count)
            {
                var nObj = _factory.Create(_poolTarget);
                _pooledObjects.Add(nObj);
                return nObj;
            }

            return null;
        }
        
        public void HideAllObjects()
        {
            foreach (var o in _pooledObjects)
            {
                o.SetActive(false);
            }
        }

        public void RemoveAllObjects()
        {
            for (int i = _pooledObjects.Count - 1; i >= 0; i--)
            {
                _pooledObjects[i].Dispose();
            }

            _pooledObjects.Clear();

            Debug.Log("Clear Objects");
        }

        #region Dispose

        public void Dispose()
        {
            Dispose(true);
        }

        public void Dispose(bool explicitCall)
        {
            if (!this._alreadyDisposed)
            {
                if (explicitCall)
                {
                    Debug.Log("Not in the destructor");
                    RemoveAllObjects();
                }
                _alreadyDisposed = true;
            }
        }

        ~AbsPrefabPooling()
        {
            Debug.Log("in the destructor");
            Dispose(false);
        }

        #endregion
        
    }
    
    public class AbsPrefabPooling<T, TArgs, TFactory> : IDisposable where T : AbsPrefabItem<T, TArgs> where TFactory : AbsPrefabItem<T, TArgs>.ItemFactory
    {
        private List<T> _pooledObjects;

        //sample of the actual object to store
        //used if we need to grow the list
        private TFactory _factory;

        //maximum number of objects to have in the list.
        private int _maxPoolSize;

        //initial and default number of objects to have in the list
        private int _initialPoolSize;

        private bool _willGrow;

        private Transform _poolTarget;

        private bool _alreadyDisposed = false;

        public AbsPrefabPooling(TFactory factory, Transform poolTarget, int maxPoolSize, int initialPoolSize, bool willGrow = false )
        {
            _factory = factory;
            _maxPoolSize = maxPoolSize;
            _initialPoolSize = initialPoolSize;
            _willGrow = willGrow;
            _poolTarget = poolTarget;

            _pooledObjects ??= new List<T>();

            for (int i = 0; i < _initialPoolSize; i++)
            {
                var o = factory.Create(poolTarget);
                _pooledObjects.Add(o);
            }
        }

        // ReSharper disable Unity.PerformanceAnalysis
        public TItem GetItem<TItem>() where TItem : class, IPrefabItem<TArgs>
        {
            return GetItem() as TItem;
        }

        public IPrefabItem<TArgs> GetItem()
        {
            var item = _pooledObjects.FirstOrDefault(s => s.gameObject.activeSelf == false);

            if (item != null)
            {
                item.ReInitialize();
                item.SetActive(true);
                return item;
            }

            if (_willGrow || _maxPoolSize > _pooledObjects.Count)
            {
                var nObj = _factory.Create(_poolTarget);
                _pooledObjects.Add(nObj);
                nObj.SetActive(true);
                return nObj;
            }

            return null;
        }
        
        public void HideAllObjects()
        {
            foreach (var o in _pooledObjects)
            {
                o.SetActive(false);
            }
        }
        
        public void Shrink()
        {
            //how many objects are we trying to remove here?
            int objectsToRemoveCount = _pooledObjects.Count - _initialPoolSize;
            //Are there any objects we need to remove?
            if (objectsToRemoveCount <= 0)
            {
                //cool lets get out of here.
                return;
            }

            //iterate through our list and remove some objects
            //we do reverse iteration so as we remove objects from
            //the list the shifting of objects does not affect our index
            //Also notice the offset of 1 to account for zero indexing
            //and i >= 0 to ensure we reach the first object in the list.
            for (int i = _pooledObjects.Count - 1; i >= _initialPoolSize; i--)
            {
                var go = _pooledObjects[i].gameObject;
                //Is this object active?
                if (!go.activeSelf)
                {
                    //Guess not, lets grab it.
                    var obj = _pooledObjects[i];
                    //and kill it from the list.
                    _pooledObjects.Remove(obj);

                    GameObject.Destroy(go);
                }
            }
        }

        public void RemoveAllObjects()
        {
            for (int i = _pooledObjects.Count - 1; i >= 0; i--)
            {
                _pooledObjects[i].Dispose();
            }

            _pooledObjects.Clear();

            Debug.Log("Clear Objects");
        }

        #region Dispose

        public void Dispose()
        {
            Dispose(true);
        }

        public void Dispose(bool explicitCall)
        {
            if (!this._alreadyDisposed)
            {
                if (explicitCall)
                {
                    Debug.Log("Not in the destructor");
                    RemoveAllObjects();
                }
                _alreadyDisposed = true;
            }
        }

        ~AbsPrefabPooling()
        {
            Debug.Log("in the destructor");
            Dispose(false);
        }

        #endregion
        
    }
}