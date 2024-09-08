using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.0f;
    [SerializeField]
    private Player _player;
    [SerializeField]
    private int _powerUpID;
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        if (_player == null)
            Debug.LogError("Player is NULL.");
    }

    // Update is called once per frame
    void Update()
    {
        calculateMovement();
    }

    void calculateMovement()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y <= -5.0f)
            Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            
            if(_player != null)
            {
             
                switch (_powerUpID)
                {
                    case 0:
                        _player.isTripleShotActive();
                        break;
                    case 1:
                        _player.activeSpeedBoost();
                        break;
                    case 2:
                        Debug.Log("Shield bust actived");
                        break;
                    default:
                        Debug.Log("Default value");
                        break;
                }
                    
            }
            Destroy(this.gameObject);


        }
    }
}
