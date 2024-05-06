using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public float coinRotateSpeed = 250.0f;
    public float keyRotateSpeed = 100.0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (gameObject.tag == "Coin")
        {
            float Rotate = coinRotateSpeed * Time.deltaTime;
            transform.Rotate(Rotate, 0, 0);
        }
        else if (gameObject.tag == "Key")
        {
            float Rotate = keyRotateSpeed * Time.deltaTime;
            transform.Rotate(Rotate, Rotate, 0);
        }
    }
}
