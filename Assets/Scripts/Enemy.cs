using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;


public class Enemy : MonoBehaviour
{

    [SerializeField]
    private float _speedDown = 2f;
    private static System.Random random = new System.Random();
    private float min = -11.3f;
    private float max = 11.3f;
    // Start is called before the first frame update
    void Start()
    {
        //transform.position = new Vector3(0, 11f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
    }

    void CalculateMovement()
    {
        //or just Random.Range(min, max);
        float randX = Random.Range(min, max);
        transform.Translate(Vector3.down * _speedDown * Time.deltaTime);
        if (transform.position.y <= -6f)
            transform.position = new Vector3(randX, 11f, 0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //if the laser collides
        //destroy laser
        //destroy us
        if(other.tag == "Laser")
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
        else if(other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            if (player != null)
            {
                player.Damage();
            }
            Destroy(this.gameObject);
        }
    }
}
