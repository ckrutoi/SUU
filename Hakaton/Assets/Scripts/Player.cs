using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private float MoveX = 0f;
    public float speed = 1f;
    public float MaxSpeed = 1f;
    public float jumpForce = 5f;
    private bool isGrounded = false;
    public string collisionTag;
    public Transform groundCheck;
    private Animator anim;
    private void OnCollisionEnter2D(Collision2D coll)
    {
        collisionTag = coll.gameObject.tag;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    private void FixedUpdate()
    {
        Vector2 targetVelocity = new Vector2(MoveX * MaxSpeed, rb.velocity.y);
        rb.velocity = targetVelocity;
    }
    void Update()
    {
        MoveX = Input.GetAxisRaw("Horizontal") * speed;
        Flip();
        Jump();
        GroundCheck();
        Animation();
    }
    private void Animation()
    {
        if (!isGrounded)
        {
            anim.SetInteger("AnimComp", 4);
        }
        if (Input.GetAxis("Horizontal") > 0 || Input.GetAxis("Horizontal") < 0)
        {
            anim.SetInteger("AnimComp", 2);
        }
        if (Input.GetAxis("Horizontal") == 0)
        {
            anim.SetInteger("AnimComp", 1);
        }
        
    }
    
    private void Flip()
    {
        if (Input.GetAxis("Horizontal") > 0)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        if (Input.GetAxis("Horizontal") < 0)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
    }
    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        }
    }
    private void GroundCheck()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, 0.2f);
        if (colliders.Length > 1 && collisionTag == "Solid")
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }
}
