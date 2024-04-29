using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{

    int coinScore = 10;
    public float speed = 500.0f;  
    public Vector3 rotationAxis = Vector3.up; 

    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (gameObject.tag != "Player") {
            if (collision.gameObject.tag == "Player")
            {
                collision.gameObject.GetComponent<PlayerScript>().IncreaseScore(coinScore);
                Destroy(gameObject);
            }
        }
    }

    void Update()
    {
        if (gameObject.tag != "Player")
        {
            float angle = speed * Time.deltaTime;
            transform.Rotate(rotationAxis, angle);
        }
    }
}
