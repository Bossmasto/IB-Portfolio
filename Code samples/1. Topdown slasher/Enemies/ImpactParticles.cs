using UnityEngine;

public class ImpactParticles : MonoBehaviour
{
    [SerializeField] PooledMonoBehaviour _impactParticle;

    ITakeHit _entity;

    void Awake()
    {
        _entity = GetComponent<ITakeHit>();
        _entity.OnHit += HandleHit;
    }

    void OnDestroy()
    {
        _entity.OnHit -= HandleHit;
    }

    void HandleHit()
    {
        _impactParticle.Get<PooledMonoBehaviour>(transform.position + new Vector3(0, 2, 0), Quaternion.identity);
    }
}
