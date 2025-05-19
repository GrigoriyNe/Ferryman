using System.Collections.Generic;
using UnityEngine;

namespace Pool
{
    public class Pool<T>
        where T : MonoBehaviour
    {
        [SerializeField] private T _prefab;

        private Queue<T> _pool;

        public Pool(T prefab)
        {
            _prefab = prefab;
            _pool = new Queue<T>();
        }

        public MonoBehaviour GetItem()
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

        private MonoBehaviour CreateObject()
        {
            MonoBehaviour item;

            if (_pool.Count == 0)
            {
                item = Object.Instantiate(_prefab);
                Activate(item);

                return item;
            }

            item = _pool.Dequeue();
            Activate(item);

            return item;
        }

        private void Activate(MonoBehaviour item)
        {
            item.gameObject.SetActive(true);
        }
    }
}