using UnityEngine;

public class Jump : AbilityBase
{
    [SerializeField] float _jumpForce = 100f;

    Rigidbody _rb;

    void Awake()
    {
        _rb = GetComponent<Rigidbody>();    
    }
    protected override void OnUse()
    {
        _rb.AddForce(Vector3.up * _jumpForce);
    }
}
