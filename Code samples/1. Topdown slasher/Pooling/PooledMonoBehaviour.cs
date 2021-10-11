using System;
using System.Collections;
using UnityEngine;

public class PooledMonoBehaviour : MonoBehaviour
{
    [SerializeField] int _initialPoolSize = 50;

    public event Action<PooledMonoBehaviour> onReturnToPool;

    public int InitialPoolSize => _initialPoolSize;

    public T Get<T>(bool enable = true) where T: PooledMonoBehaviour
    {
        Pool pool = Pool.GetPool(this);
        var pooledObject = pool.Get<T>();

        if (enable)
            pooledObject.gameObject.SetActive(true);

        return pooledObject;
    }

    public T Get<T>(Vector3 position, Quaternion rotation) where T : PooledMonoBehaviour
    {
        var pooledObject = Get<T>();
        pooledObject.transform.position = position;
        pooledObject.transform.rotation = rotation;

        return pooledObject;
    }

    protected virtual void OnDisable()
    {
        onReturnToPool?.Invoke(this);
    }

    protected void ReturnToPool(float delay = 0)
    {
        StartCoroutine(ReturnToPoolAfterSeconds(delay));
    }

    IEnumerator ReturnToPoolAfterSeconds(float delay)
    {
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
    }
}
