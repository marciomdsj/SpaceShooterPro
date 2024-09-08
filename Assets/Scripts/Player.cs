using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    //Datatypes (int, float, bool, string)
    //Public or private reference

    [SerializeField]
    private float _speed = 4.5f;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    [SerializeField]
    private float _fireRate = 0.2f;
    private float _canFire = -1.0f;
    [SerializeField]
    private int _lives = 3;
    [SerializeField]
    private SpawnManager _spawnManager;
    [SerializeField]
    private bool _isTripleShot = false;
    [SerializeField]
    private bool _isSpeedBoost = false;
    
    void Start()
    {
        //take the current position = new position (0,0,0)
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>(); //get access to spawnmanager script
        if(_spawnManager == null)
        {
            Debug.LogError("The SpawnManager is NULL.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
            FireLaser();
        
    }


    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        //new Vector3(-1, 0, 0) is equal to Vector3.left
        //transform.Translate(Vector3.right * horizontalInput * _speed * Time.deltaTime);
        //transform.Translate(Vector3.up * verticalInput * _speed * Time.deltaTime);

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);

        if (_isSpeedBoost == true)
        {
            activeSpeedBoost();
            transform.Translate(direction * (_speed + 8.0f) * Time.deltaTime);
        }
        else
        {
            transform.Translate(direction * _speed * Time.deltaTime);
        }



            if (transform.position.y >= 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
        else if (transform.position.y <= -4.0f)
        {
            transform.position = new Vector3(transform.position.x, -4.0f, 0);
        }

        if (transform.position.x >= 11.3f)
        {
            transform.position = new Vector3(-11.3f, transform.position.y, 0);
        }
        else if (transform.position.x <= -11.3f)
        {
            transform.position = new Vector3(11.3f, transform.position.y, 0);
        }

    }

    void FireLaser()
    {
        Vector3 setPosition = new Vector3(transform.position.x, transform.position.y + 1.06f, 0);
        Vector3 setPosition2 = new Vector3(transform.position.x + 1.06f, transform.position.y, 0);
        Vector3 setPosition3 = new Vector3(transform.position.x - 1.06f, transform.position.y, 0);
        _canFire = Time.time + _fireRate;
        if(_isTripleShot == false)
            Instantiate(_laserPrefab, setPosition, Quaternion.identity);
        else
        {
            Instantiate(_laserPrefab, setPosition, Quaternion.identity);
            Instantiate(_laserPrefab, setPosition2, Quaternion.identity);
            Instantiate(_laserPrefab, setPosition3, Quaternion.identity);
        }
    }

    public void Damage()
    {
        _lives--;
        if (_lives < 1)
        {
            _spawnManager.IsPlayerDeath();
            Destroy(this.gameObject);
        }
            
    }

    public void isTripleShotActive()
    {
        _isTripleShot = true;
        StartCoroutine(TripleShotRoutine());
    }

    IEnumerator TripleShotRoutine()
    {
        while (_isTripleShot)
        {
            yield return new WaitForSeconds(5.0f);
            _isTripleShot= false;
        }
    }

    public void activeSpeedBoost()
    {
        _isSpeedBoost = true;
       // _speed += 8.0f;
        StartCoroutine(SpeedBoostRoutine());
    }

    

    IEnumerator SpeedBoostRoutine()
    {
        yield return new WaitForSeconds(3.0f);
        _isSpeedBoost = false;
    }
}
