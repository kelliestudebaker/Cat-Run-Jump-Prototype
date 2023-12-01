using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEnemy : MonoBehaviour
{
    public GameObject gameObjectToDestroy;

    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "CatStomp")
        {
            Destroy(gameObjectToDestroy); //Destroy the rat when it is jumped on.
            Debug.Log("Destroyed rat");
        }
    }
}
