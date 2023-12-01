using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Sign : MonoBehaviour
{
    public GameObject sign;
    Rigidbody2D m_Rigidbody;

    public GameObject ZText;

    public bool isInRange;
    public KeyCode interactKey;
    public KeyCode continueKey;
    public UnityEvent interactAction;
    public UnityEvent continueText;

    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (isInRange)
        {
            if (Input.GetKeyDown(interactKey))
            {
                interactAction.Invoke();
                Debug.Log("Doing something");
            }
            if (Input.GetKeyDown(continueKey))
            {
                continueText.Invoke();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            isInRange = true; //If the player is near the sign,
            ZText.SetActive(true); //show the letter "Z" to indicate that the player should press the Z key.
            Debug.Log("Near a sign");
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        isInRange = false; //Once the player moves away from the sign,
        ZText.SetActive(false); //Hide the letter "Z".
        Debug.Log("Away from sign");
    }
}
