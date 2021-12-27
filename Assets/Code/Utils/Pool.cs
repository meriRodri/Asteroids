using System.Collections.Generic;
using UnityEngine;

namespace Code.Utils
{
    public class Pool<T> where T : Component
    {
        private List<T> _pool = new List<T>();
        private T _prefab;
        private Transform _parent;

        public Pool(int size, T prefab, Transform parent = null)
        {
            _prefab = prefab;
            _parent = parent;

            CreatePool(size); 
        }

        public void AddObjectsToPool(int size)
        {
            CreatePool(size, _pool.Count);
        }

        public void CreatePool(int size, int initNumber = 0)
        {
            for (int i = initNumber; i < size; i++)
            {
                _pool.Add(CreateNewInstance());
            }
        }

        private T CreateNewInstance()
        {
            T go = Object.Instantiate(_prefab, _parent);
            go.gameObject.SetActive(false);

            return go;
        }

        public List<T> GetPooledObjects()
        {
            return _pool;
        }

        public T GetPooledObject()
        {
            for (int i = 0; i < _pool.Count; i++)
            {
                if (!_pool[i].gameObject.activeSelf)
                {
                    return _pool[i];
                }

            }
            return null;
        }

        public int Count => _pool.Count;
    }
}
