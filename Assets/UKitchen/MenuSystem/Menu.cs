using System;
using UnityEngine;
using Zenject;

namespace UKitchen.MenuSystem
{
    public class Menu<TEnum, TArgs, T> : AbsMenu<TEnum, TArgs> where TEnum : struct, Enum
        where TArgs : IMenuArgs
        where T : Menu<TEnum, TArgs, T>
    {
        public static T Instance { get; private set; }

        [Inject]
        public void Init()
        {
            _isInit = false;
            Instance = (T) this;
        }

        protected override void Prepare()
        {
            _isInit = false;
            gameObject.SetActive(false);
            transform.localPosition = Vector3.zero;
            transform.localScale = Vector3.one;
            transform.localRotation = Quaternion.identity;
            
            _isInit = true;
        }

        protected override void Prepare<TArgs1>(TArgs1 args)
        {
            _args = args;
            Prepare();
        }

        protected internal override void Open()
        {
            OnShowBefore();
            transform.SetAsLastSibling();
            gameObject.SetActive(true);
        }
        
        protected internal override void UpdateMenu()
        {
            Prepare();
            Open();
        }

        protected internal override void UpdateMenu<TArgs1>(TArgs1 args)
        {
            Prepare(args);
            Open();
        }

        private void OnEnable()
        {
            if (_isInit)
                OnShowAfter();
        }

        protected override void OnShowBefore()
        {
        }

        protected override void OnShowAfter()
        {
        }

        
        protected internal override void Close()
        {
            OnHideBefore();
            gameObject.SetActive(false);
        }

        private void OnDisable()
        {
            if (_isInit)
            {
                OnHideAfter();
                if (destroyWhenClosed)
                    Destroy(gameObject);
            }
        }

        protected override void OnHideBefore()
        {
        }

        protected override void OnHideAfter()
        {
        }

        public class Factory : PlaceholderFactory<Transform, IMenuArgs, T>
        {
            private DiContainer _container;
            private GameObject _prefab;

            public Factory(DiContainer container, GameObject prefab)
            {
                _container = container;
                _prefab = prefab;
            }

            public override T Create(Transform parent, IMenuArgs args)
            {
                if (Instance == null)
                {
                    var item = _container.InstantiatePrefab(_prefab, parent).GetComponent<T>();
                    item.Prepare(args);
                    return item;
                }
                else
                {
                    Instance.Prepare(args);
                    return Instance;
                }
            }

            public override void Validate()
            {
                _container.InstantiatePrefab(_prefab);
            }
        }
    }
}