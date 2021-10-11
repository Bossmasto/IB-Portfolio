using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    static Dictionary<PooledMonoBehaviour, Pool> _pools = new Dictionary<PooledMonoBehaviour, Pool>();

    Queue<PooledMonoBehaviour> _objects = new Queue<PooledMonoBehaviour>();

    PooledMonoBehaviour _prefab;

    public static Pool GetPool(PooledMonoBehaviour prefab)
    {
        if (_pools.ContainsKey(prefab))
            return _pools[prefab];

        Pool pool = new GameObject("Pool-" + prefab.name).AddComponent<Pool>();   //if pool does not exist, create new pool and add to the dictionary
        pool._prefab = prefab;

        _pools.Add(prefab, pool);
        return pool;
    }

    public T Get<T>() where T :PooledMonoBehaviour
    {
        if(_objects.Count == 0)
        {
            GrowPool();
        }

        PooledMonoBehaviour pooledObject = _objects.Dequeue();

        return pooledObject as T;
    }

    private void GrowPool()
    {
        for (int i = 0; i < _prefab.InitialPoolSize; i++)
        {
            PooledMonoBehaviour pooledObject = Instantiate(_prefab) as PooledMonoBehaviour;
            pooledObject.gameObject.name += " " + i;

            pooledObject.onReturnToPool += AddObjectToAvailableQueue;

            pooledObject.transform.SetParent(this.transform);  //make objects children of the pool in the hierarchy 
            pooledObject.gameObject.SetActive(false);
        }
    }

    void AddObjectToAvailableQueue(PooledMonoBehaviour pooledObject)
    {
        pooledObject.transform.SetParent(this.transform);
        _objects.Enqueue(pooledObject);
    }
}
