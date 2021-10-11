using UnityEngine;

public class DeathParticles : MonoBehaviour
{
    [SerializeField] PooledMonoBehaviour deathParticlePrefab;

    IDie _entity;

    private void Awake()
    {
         _entity = GetComponent<IDie>();    
    }

    void OnEnable()
    {
        _entity.OnDied += Character_OnDied;
    }

    private void Character_OnDied(IDie entity)
    {
        entity.OnDied -= Character_OnDied;
        deathParticlePrefab.Get<PooledMonoBehaviour>(transform.position, Quaternion.identity);
    }

    void OnDisable()
    {
        _entity.OnDied -= Character_OnDied;
    }
}
