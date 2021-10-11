using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballLauncher : MonoBehaviour
{

    [SerializeField] Fireball _fireballPrefab;
    [SerializeField] float _fireRate = 0.5f;

    Player _player;
    string _fireButton;
    string _horizontalAxis;
    float _nextFireTime;


    private void Awake()
    {
        _player = GetComponent<Player>();
        _fireButton = $"P{_player.PlayerNumber}Fire";
        _horizontalAxis = $"P{_player.PlayerNumber}Horizontal";
    }

    void Update()
    {
        //shooting fireballs with a delay
        if (Input.GetButtonDown(_fireButton) && Time.time >= _nextFireTime)
        {
            float horizontal = Input.GetAxis(_horizontalAxis);

            Fireball fireball = Instantiate(_fireballPrefab, transform.position, Quaternion.identity);
            fireball.Direction = horizontal >= 0 ? 1 : -1;

            _nextFireTime = Time.time + _fireRate;
        }    
    }
}
