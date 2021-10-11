using UnityEngine;

public class Fly : MonoBehaviour, ITakeDamage
{
    [SerializeField] Vector2 _direction = Vector2.up;
    [SerializeField] float _maxDistance = 2;
    [SerializeField] float _speed = 2;

    Vector2 _startPos;

    void Start()
    {
        _startPos = transform.position;
    }

    void Update()
    {
        MoveFly();
    }


    /// <summary>
    /// Moving the fly on the screen to generate obstacles
    /// </summary>
    private void MoveFly()
    {
        transform.Translate(_direction.normalized * Time.deltaTime * _speed);
        float distance = Vector2.Distance(_startPos, transform.position);
        if (distance >= _maxDistance)
        {
            transform.position = _startPos + (_direction.normalized * _maxDistance);
            _direction *= -1;
        }
    }

    public void TakeDamage()
    {
        gameObject.SetActive(false);
    }
}

