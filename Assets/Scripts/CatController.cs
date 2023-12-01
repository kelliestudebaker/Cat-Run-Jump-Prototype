using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CatController : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 11f; //How fast the cat moves.
    private float movementX;

    private Vector3 tempScale;

    private Rigidbody2D rb;
    public static int health = 3; //Cat's health.

    [SerializeField]
    private float jumpForce = 3f;

    private bool isGrounded;

    public GameObject gameObjectToMove;

    public Animator animator;

    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;
    private SpriteRenderer spriteRend;

    public GameObject heart3;
    public GameObject heart2;
    public GameObject heart1;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    void CatMovement()
    {
        movementX = Input.GetAxisRaw("Horizontal");
        transform.position += new Vector3(movementX, 0f, 0f) * moveSpeed * Time.deltaTime;
    }

    void Update()
    {
        if (!Pause.isPaused) // Everything only happens if the game is NOT paused.
        {
            CatMovement();
            FacingDirection();
            Jumping();
            animator.SetFloat("Speed", Mathf.Abs(movementX));
            //healthText.text = health.ToString();

            if (rb.velocity.y == 0) //Do not play the jumping or falling animation if the cat is not moving on the y axis.
            {
                animator.SetBool("IsJumping", false);
                animator.SetBool("IsFalling", false);
            }

            if (rb.velocity.y > 0) //If the cat is moving on the y axis, then it is jumping.
            {
                animator.SetBool("IsJumping", true);
            }

            if (rb.velocity.y < 0)
            {
                animator.SetBool("IsJumping", false);
                animator.SetBool("IsFalling", true);
            }

            if (health == 3) //Display the number of hearts to indicate the player's health.
            {
                heart3.SetActive(true);
                heart2.SetActive(false);
                heart1.SetActive(false);
            }
            if (health == 2)
            {
                heart3.SetActive(false);
                heart1.SetActive(false);
                heart2.SetActive(true);
            }
            if (health == 1)
            {
                heart3.SetActive(false);
                heart2.SetActive(false);
                heart1.SetActive(true);
            }

            if (health == 0)
            {
                Physics2D.IgnoreLayerCollision(8, 9, false); //Reset layers so they're not ignorning each other.
                SceneManager.LoadScene("02GameOver"); //Go to the game over screen once health reaches 0.
                health = 3; //Reset health after death.
            }
            if (health > 3) //Keep the health from going over 3.
            {
                health = 3;
            }
        }
    }

    void FacingDirection() //Change the direction the cat is facing.
    {
        tempScale = transform.localScale;

        if (movementX > 0)
            tempScale.x = Mathf.Abs(tempScale.x);
        else if (movementX < 0)
            tempScale.x = -Mathf.Abs(tempScale.x);

        transform.localScale = tempScale;
    }

    void Jumping()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded) //If the up arrow is pressed and cat is on the ground,
        {                                                       //then jump.
            isGrounded = false;
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        }
    }

    public static class TagManager
    {
        public static string GROUND_TAG = "Ground";
        public static string PLAYER_TAG = "Player";
        public static string RAT_TAG = "Rat";
        public static string SIGN_TAG = "Sign";
        public static string HURT_TAG = "Ouch";
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(TagManager.GROUND_TAG)) //Detect if the cat is on the ground.
        {
            isGrounded = true;
        }
    }

    public float damageTimeout = 1f;
    private bool canTakeDamage = true;
    void OnTriggerEnter2D(Collider2D col)
    {
        if (canTakeDamage && col.tag == "Ouch")
        {
            health -= 1; //When colliding with an enemy, take damage.
            StartCoroutine(Invincible()); //Invincibility frames.
            StartCoroutine(damageTimer());
            Debug.Log("Ouch! Health is now " + health);
        }
        if (col.tag == "Death") //If cat falls off of world, reset its position.
        {
            gameObjectToMove.transform.position = new Vector3(-8f, -.46f, 0f);
        }
        if (col.tag == "Goal")
        {
            Debug.Log("GOAL!!!");
            SceneManager.LoadScene("03LevelComplete"); //Load the "Level Complete" screen when the goal is reached.
            health = 3; //Reset the health to 3 at the end of the level.
        }
    }
    private IEnumerator damageTimer() //This allows cat to run into an enemy without taking too much damage.
    {
        canTakeDamage = false;
        yield return new WaitForSeconds(damageTimeout);
        canTakeDamage = true;
    }
        
    private IEnumerator Invincible()
    {
        Physics2D.IgnoreLayerCollision(8, 9, true); //Cat on layer 8, rat on layer 9
        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRend.color = new Color(1, 0, 0, 0.5f); //Make the cat flash red when it takes damage.
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(8, 9, false);
    }
}
