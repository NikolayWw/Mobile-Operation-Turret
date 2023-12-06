using System;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Logic.Pool
{
    public abstract class BaseObjectPoolHandler<TKey, TObjectPool> where TObjectPool : class, IObjectPool
    {
        public class PoolObjectData
        {
            public TObjectPool ObjectPool { get; }
            private Dictionary<Type, MonoBehaviour> _components;

            public PoolObjectData(TObjectPool objectPool, params MonoBehaviour[] components)
            {
                ObjectPool = objectPool;
                InitComponentsDictionary(components);
            }

            public MonoBehaviour ReciveComponent(Type key) =>
                _components.TryGetValue(key, out MonoBehaviour component) ? component : null;

            private void InitComponentsDictionary(MonoBehaviour[] components)
            {
                _components = new Dictionary<Type, MonoBehaviour>(components.Length);
                foreach (MonoBehaviour component in components)
                    _components.Add(component.GetType(), component);
            }
        }

        private readonly Dictionary<TKey, Queue<PoolObjectData>> _objectsDictionary = new();

        public void InitStartObjects(int count, params TKey[] keys)
        {
            foreach (TKey key in keys)
            {
                GetQueue(key, out Queue<PoolObjectData> queue);
                for (int i = 0; i < count; i++)
                {
                    PoolObjectData poolData = InitNewObjectPool(queue, key);
                    poolData.ObjectPool.Disable();
                }
            }
        }

        protected abstract PoolObjectData NewObjectPoolData(TKey key);

        protected PoolObjectData GetData(TKey key)
        {
            GetQueue(key, out Queue<PoolObjectData> queue);
            if (TryGetObjectPool(queue, out PoolObjectData poolObjectData))
                return poolObjectData;

            Debug.LogError("Pool size is too small");
            return InitNewObjectPool(queue, key);
        }

        private PoolObjectData InitNewObjectPool(Queue<PoolObjectData> queue, TKey key)
        {
            PoolObjectData newObjectPool = NewObjectPoolData(key);
            queue.Enqueue(newObjectPool);
            return newObjectPool;
        }

        private static bool TryGetObjectPool(Queue<PoolObjectData> queue, out PoolObjectData poolData)
        {
            for (int i = 0; i < queue.Count; i++)
            {
                poolData = queue.Dequeue();
                queue.Enqueue(poolData);

                if (poolData.ObjectPool.IsReady())
                    return true;
            }

            poolData = null;
            return false;
        }

        private void GetQueue(TKey key, out Queue<PoolObjectData> queue)
        {
            if (_objectsDictionary.TryGetValue(key, out queue) == false)
            {
                queue = new Queue<PoolObjectData>();
                _objectsDictionary.Add(key, queue);
            }
        }
    }
}