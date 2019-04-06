using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shrek : MonoBehaviour
{
    private static string ground = "Ground";
    private bool isOnGround = false;
    private readonly int movSpeed = 15;
    private readonly int jumpHeight = 10;
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer sprite;
    private Vector2 prevVel;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        rb.drag = 0;
    }

    // Update is called once per frame
    void Update() {
        print(rb.velocity + " " + (rb.velocity - prevVel));
        prevVel = rb.velocity;
        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            rb.velocity += new Vector2(-movSpeed, 0);
            animator.SetBool("isWalking", true);
            sprite.flipX = true;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow)) {
            rb.velocity += new Vector2(movSpeed, 0);
            animator.SetBool("isWalking", true);
            sprite.flipX = false;
        }
        if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow)) {
            rb.velocity = new Vector2(0, 0);
            animator.Play("ShrekIdle");
            sprite.flipX = false;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            rb.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
            //rb.velocity += Vector2.up*jumpHeight;
            animator.Play("ShrekJump");
        } 
        if (Input.GetKeyDown(KeyCode.Z)) {
            animator.Play("ShrekPunch");
        }
        if (Input.GetKeyDown(KeyCode.X)) {
            animator.Play("ShrekKick");
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        print("CollisionEnter");
        if (collision.gameObject.tag.Equals(ground)) {
            isOnGround = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision) {
        print("CollisionExit");
        if (collision.gameObject.name.Equals(ground)) {
            isOnGround = false;
        }
    }
}