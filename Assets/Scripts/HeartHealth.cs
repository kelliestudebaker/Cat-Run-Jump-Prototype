using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartHealth : MonoBehaviour
{
    public GameObject hearthealth;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player") //If the player touches a heart object, add one to the player's health.
        {
            CatController.health += 1;
            Destroy(hearthealth); //Destroy the heart object once it has been collected.
        }
    }
}
