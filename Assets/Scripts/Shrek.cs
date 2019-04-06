﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shrek : MonoBehaviour
{
    private static string ground = "Ground";
    private bool isOnGround = false;
    private readonly int movSpeed = 25;
    private readonly int jumpHeight = 250;
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer sprite;
    private Vector2 movementVelocity;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        rb.drag = 0;
        movementVelocity = new Vector2(0, 0);
    }

    // Update is called once per frame
    void Update() {
        rb.velocity -= movementVelocity;
        movementVelocity = new Vector2(0, 0);
        if (Input.GetKey(KeyCode.LeftArrow)) {
            movementVelocity += new Vector2(-movSpeed, 0);
            animator.SetBool("isWalking", true);
            sprite.flipX = true;
        }
        if (Input.GetKey(KeyCode.RightArrow)) {
            movementVelocity += new Vector2(movSpeed, 0);
            animator.SetBool("isWalking", true);
            sprite.flipX = false;
        }
        if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow)) {
            rb.velocity = new Vector2(0, 0);
            animator.SetBool("isWalking", false);

            sprite.flipX = false;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) && isOnGround) {
            rb.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
        } 
        if (Input.GetKeyDown(KeyCode.Z)) {
            animator.SetTrigger("Punch");
        }
        if (Input.GetKeyDown(KeyCode.X)) {
            animator.SetTrigger("Kick");
        }

        animator.SetBool("isInAir", !isOnGround);

        rb.velocity += movementVelocity;
    }

    void OnCollisionEnter2D(Collision2D collision) {
        print("CollisionEnter " + collision.gameObject.tag + " " + ground + " " + collision.gameObject.tag.Equals(ground));
        if (collision.gameObject.tag.Equals(ground)) {
            isOnGround = true;
        }
        print(isOnGround);
    }

    void OnCollisionExit2D(Collision2D collision) {
        print("CollisionExit " + collision.gameObject.tag + " " + ground + " " + collision.gameObject.tag.Equals(ground));
        if (collision.gameObject.tag.Equals(ground)) {
            print("setting false");
            isOnGround = false;
        }
        print(isOnGround);
    }
}