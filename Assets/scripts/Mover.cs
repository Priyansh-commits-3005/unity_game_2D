using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    private Rigidbody2D rb;
    private float horizontal;

    public float movespeed = 0f;
    public float jumpForce = 0f;
    public float rayCastLength = 0f;

    public bool isGrounded;
    public LayerMask groundLayerMask;
    private Animator anim;
    private SpriteRenderer spriteRenderer;
    public Transform respawnPoint;
    

// Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        respawnPoint.position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        rb.velocity = new Vector2(x: horizontal * movespeed, rb.velocity.y);
        horizontal = Input.GetAxis("Horizontal");
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, y: jumpForce);

        }
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, rayCastLength, groundLayerMask);
        Debug.DrawRay(transform.position, Vector3.down * rayCastLength, Color.green);

        //animation

        if (rb.velocity.x != 0)
        {
            anim.SetBool("isMoving", true);

        }
        else
        {
            anim.SetBool("isMoving", false);
        }

        if (isGrounded == false)
        {
            anim.SetBool("isJumping", true);
        }
        else
        {
            anim.SetBool("isJumping", false);
        }


        // flipping player when going left side
        if (horizontal < 0)
        {
            spriteRenderer.flipX = true;

        }
        else
        {
            spriteRenderer.flipX = false;

        }
        
    
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Crystal")
        {
            Destroy(other.gameObject);
        }
        if (other.tag == "Respawn")
        {
            Respawn();
        }

    }
    void Respawn()
    {
        transform.position = respawnPoint.position;
    }
}








 