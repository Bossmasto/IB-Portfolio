using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Behavior of the ship in the game
/// </summary>
public class Ship : MonoBehaviour
{

    Rigidbody2D ship;

    //Thrusting related
    Vector2 thrustDirection = new Vector2(1, 0);
    const float ThrustForce = 6;

    //Rotating

    const float rotatePerSecond = 180f;

    //bullets

    [SerializeField] GameObject prefabBullet;

    [SerializeField] GameObject HUD;


    // Start is called before the first frame update
    void Start()
    {
        ship = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        float rotationInput = Input.GetAxis("Rotate");

        // calculate rotation amount and apply rotation

        if (rotationInput != 0)
        {
            float rotationAmount = rotatePerSecond * Time.deltaTime;
            if (rotationInput < 0)
            {
                rotationAmount *= -1;
            }
            transform.Rotate(Vector3.forward, rotationAmount);

            float newZposition = Mathf.Deg2Rad * transform.rotation.eulerAngles.z;

            thrustDirection = new Vector2(Mathf.Cos(newZposition), Mathf.Sin(newZposition));
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
           GameObject bullet = Instantiate (prefabBullet, transform.position, transform.rotation);
            bullet.GetComponent<Bullet>().ApplyForce(thrustDirection);

            AudioManager.Play(AudioClipName.PlayerShot);
        }

    }
    private void FixedUpdate()
    {

        //checking for input to apply thrust
        if (Input.GetAxis("thrust") != 0)
        {
            ship.AddForce(thrustDirection * ThrustForce, 0);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if( collision.gameObject.tag == "Asteroid")
        {
            Destroy(gameObject);
            HUD.GetComponent<HUD>().StopGameTimer();
            AudioManager.Play(AudioClipName.PlayerDeath);
        }
    }
}

