using System.Collections.Generic;
using UnityEngine;

namespace Pool
{
    public class Pool<T> : MonoBehaviour
        where T : SpawnableObject
    {
        [SerializeField] private T _prefab;

        private Queue<T> _pool;

        private void Awake()
        {
            _pool = new Queue<T>();
        }

        public void ChangePrefab(T newPrefab)
        {
            _prefab = newPrefab;
        }

        public SpawnableObject GetItem()
        {
            return CreateObject();
        }

        public void Clean()
        {
            _pool = new Queue<T>();
        }

        public void ReturnItem(T item)
        {
            item.gameObject.SetActive(false);
            _pool.Enqueue(item);
        }

        private SpawnableObject CreateObject()
        {
            SpawnableObject item;

            if (_pool.Count == 0)
            {
                item = Instantiate(_prefab);
                Activate(item);

                return item;
            }

            item = _pool.Dequeue();
            Activate(item);

            return item;
        }

        private void Activate(SpawnableObject item)
        {
            item.gameObject.SetActive(true);
        }
    }
}