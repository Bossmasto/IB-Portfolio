using UnityEngine;

public class SpringBoard : MonoBehaviour
{
    [SerializeField] float _bounceVelocity = 10f;
    [SerializeField] Sprite _downSprite;
    
    Sprite _upSprite;
    SpriteRenderer _spriteRenderer;


    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _upSprite = _spriteRenderer.sprite;  // default sprite
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Player player = collision.collider.GetComponent<Player>();
        if (player != null)
        {
            Rigidbody2D rb2d = player.GetComponent<Rigidbody2D>();
            if (rb2d != null)
            {
                rb2d.velocity = new Vector2(rb2d.velocity.x, _bounceVelocity);
                _spriteRenderer.sprite = _downSprite;
            }
        }
    }

    /// <summary>
    /// changing sprite back to default one
    /// </summary>
    /// <param name="collision"></param>
    void OnCollisionExit2D(Collision2D collision)
    {
        Player player = collision.collider.GetComponent<Player>();
        if (player != null)
        {
            _spriteRenderer.sprite = _upSprite;
        }
    }
}
