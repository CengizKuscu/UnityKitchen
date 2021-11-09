using UnityEngine;
using Zenject;

namespace UKitchen.Pooling
{
    public abstract class AbsPrefabItem<T> : MonoBehaviour, IPrefabItem where T : IPrefabItem
    {
        public virtual void ReInitialize()
        {
            var o = gameObject;
            o.transform.localPosition = Vector3.zero;
            o.transform.localScale = Vector3.one;
            o.transform.rotation = Quaternion.identity;
            o.SetActive(false);
        }

        public void SetActive(bool val)
        {
            gameObject.SetActive(val);
        }

        public void Dispose()
        {
            Destroy(gameObject);
        }

        public class Factory : PlaceholderFactory<Transform, T>
        {
            private DiContainer _container;
            private GameObject _prefab;

            public Factory(DiContainer container, GameObject prefab)
            {
                _container = container;
                _prefab = prefab;
            }

            public override T Create(Transform t)
            {
                var item = _container.InstantiatePrefab(_prefab, t);
                var comp = item.GetComponent<T>();
                comp.ReInitialize();
                return comp;
            }

            public override void Validate()
            {
                var o = _container.InstantiatePrefab(_prefab);
            }
        }
    }

    public abstract class AbsPrefabItem<T, TArgs> : AbsPrefabItem<T>, IPrefabItem<TArgs> where T : IPrefabItem<TArgs>
    {
        public virtual TArgs Args { get; set; }
        

        public class ItemFactory : PlaceholderFactory<Transform, T>
        {
            private DiContainer _container;
            private GameObject _prefab;

            public ItemFactory(DiContainer container, GameObject prefab)
            {
                _container = container;
                _prefab = prefab;
            }

            public override T Create(Transform t)
            {
                var item = _container.InstantiatePrefab(_prefab, t);
                var comp = item.GetComponent<T>();
                comp.ReInitialize();
                return comp;
            }
            
            public override void Validate()
            {
                var o = _container.InstantiatePrefab(_prefab);
            }
        }
    }
}