using UnityEngine;

public class Fireball : MonoBehaviour
{
    [SerializeField] float _launchForce = 5;
    [SerializeField] float _bounceForce = 5f;

    Rigidbody2D rb2d;

    int _bouncesRemaining = 3;

    public int Direction { get; set; }

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = new Vector2(Direction * _launchForce, _bounceForce) ;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
         ITakeDamage damageable = collision.collider.GetComponent<ITakeDamage>();
        if (damageable != null)
        {
            damageable.TakeDamage();
            Destroy(gameObject);
            return;
        }

        _bouncesRemaining--;
        if (_bouncesRemaining < 0)
            Destroy(gameObject);
        else 
            rb2d.velocity = new Vector2(Direction * _launchForce, _bounceForce);
    }
}
