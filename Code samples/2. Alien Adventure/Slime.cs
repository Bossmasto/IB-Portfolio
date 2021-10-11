using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour, ITakeDamage
{
    [SerializeField] Transform _sensorLeft;
    [SerializeField] Transform _sensorRight;
    [SerializeField] Sprite _deadSprite;

    Rigidbody2D _rb2d;

    float _direction = -1;

    void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        _rb2d.velocity = new Vector2(_direction, _rb2d.velocity.y);

        if (_direction < 0)
            ScanSensor(_sensorLeft);
        else
            ScanSensor(_sensorRight);
    }

    public void TakeDamage()
    {
        StartCoroutine(Die());
    }

    void ScanSensor(Transform sensor)
    {
        //check down
        RaycastHit2D result = Physics2D.Raycast(sensor.position, Vector2.down, 0.1f);
        if (result == false)
            TurnAround();

        //check to sides
        Debug.DrawRay(sensor.position, new Vector2(_direction, 0) * 0.1f, Color.red);
        RaycastHit2D sideResult = Physics2D.Raycast(sensor.position, new Vector2(_direction, 0), 0.1f);
        if (sideResult != false)
            TurnAround();

    }

    void TurnAround()
    {
        _direction *= -1;
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.flipX = _direction > 0;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Player player = collision.collider.GetComponent<Player>();

        Vector2 normal = collision.contacts[0].normal; //getting normal out of point of contact
        Debug.Log($"Normal = {normal}");

        if (normal.y <= -0.5)
            TakeDamage();
        else
            player?.ResetToStart();
    }

    IEnumerator Die()
    {
        //stopping animations and everything else before changing the sprite
        GetComponent<Animator>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
        _rb2d.simulated = false;

        AudioSource audioSource = GetComponent<AudioSource>();
        if (audioSource != null)
            audioSource.Play();

        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = _deadSprite;
        
        //fading
        float alpha = 1;
        while(alpha > 0)
        {
            yield return null;
            alpha -= Time.deltaTime;
            spriteRenderer.color = new Color(1, 1, 1, alpha);
        }
    }
}
