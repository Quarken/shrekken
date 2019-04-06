using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shrek : MonoBehaviour
{
    private static string ground = "Ground";
    private bool isOnGround = true;
    private bool hasHitGround = true;
    private readonly int movSpeed = 30;
    private readonly int jumpHeight = 40;

    private readonly int gravity = 60;
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer sprite;
    private Collider2D collider;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        collider = GetComponent<Collider2D>();
    }

    void FixedUpdate() {
        RaycastHit2D hit = Physics2D.Raycast(rb.position, Vector2.down, 4);
        isOnGround = hit.collider != null;

        if(!isOnGround) {
            rb.velocity += Vector2.down * gravity * Time.fixedDeltaTime;
            hasHitGround = false;
        }
        else if(!hasHitGround) {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            hasHitGround = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
            if (Input.GetKeyDown(KeyCode.LeftArrow)) {
                rb.velocity = new Vector2(-movSpeed, rb.velocity.y);
                animator.SetBool("isWalking", true);
                sprite.flipX = true;
            }
            if (Input.GetKeyDown(KeyCode.RightArrow)) {
                rb.velocity = new Vector2(movSpeed, rb.velocity.y);
                animator.SetBool("isWalking", true);
                sprite.flipX = false;
            }
            if (Input.GetKeyUp(KeyCode.LeftArrow)) {
                rb.velocity = new Vector2(0, rb.velocity.y);
                animator.Play("ShrekIdle");
                sprite.flipX = false;
            }
            if (Input.GetKeyUp(KeyCode.RightArrow)) {
                rb.velocity = new Vector2(0, rb.velocity.y);
                animator.Play("ShrekIdle");
                sprite.flipX = false;
            }
            if (Input.GetKeyDown(KeyCode.UpArrow) && isOnGround) {
                rb.velocity += Vector2.up * jumpHeight;
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
        if (collision.gameObject.tag.Equals(ground)) {
            isOnGround = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision) {
        if (collision.gameObject.name.Equals(ground)) {
            isOnGround = false;
        }
    }
}