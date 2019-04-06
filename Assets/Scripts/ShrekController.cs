using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrekController : MonoBehaviour {

    private string shrekMode = "Shreikh"; // Should be defined during gameplay
    private int speed = 80;
    private int gravity = 100;
    private Rigidbody2D rb;
    private bool isGrounded = false;
    private bool isWalking = false;
    private string ground = "Ground";

    private Animator animator;

    // Start is called before the first frame update
    void Start () {
        rb = GetComponent<Rigidbody2D> ();
        rb.drag = 5f;
        animator = GetComponent<Animator> ();

        animator.SetTrigger (shrekMode + "Idle");
    }

    void FixedUpdate () {

        float speed = 600f;
        float maxSpeed = 75f;
        float jumpSpeed = 150f;

        // Left
        if (Input.GetKey (KeyCode.LeftArrow)) {
            if (rb.velocity.x > -maxSpeed) {
                rb.AddForce (new Vector2 (-speed, 0));
            }
        }

        // Right
        if (Input.GetKey (KeyCode.RightArrow)) {
            if (rb.velocity.x < maxSpeed) {
                rb.AddForce (new Vector2 (speed, 0));
            }
        }

        // Walk animations
        if (rb.velocity.x < -40 || rb.velocity.x > 40) {
            animator.SetBool ("IsWalking", true);
        }
        else {
            animator.SetBool ("IsWalking", false);
        }

        // Jump
        if (Input.GetKey (KeyCode.UpArrow) && isGrounded) {
            rb.AddForce (new Vector2 (0, jumpSpeed), ForceMode2D.Impulse);
        }
        // Jump aniation
        if (Input.GetKeyDown (KeyCode.UpArrow)) {
            animator.SetTrigger (shrekMode + "Jump");
        }

        // Punch animation
        if (Input.GetKeyDown(KeyCode.Z)) {
            animator.SetTrigger (shrekMode + "Punch");
        }

        // Kick animation
        if (Input.GetKeyDown(KeyCode.X)) {
            animator.SetTrigger (shrekMode + "Kick");
        }

        animator.SetBool ("IsGrounded", isGrounded);

    }

    void OnCollisionEnter2D (Collision2D col) {
        print ("enter " + col.gameObject.tag);
        if (col.gameObject.tag == ground) {
            isGrounded = true;
        }
    }

    void OnCollisionExit2D (Collision2D col) {
        print ("exit " + col.gameObject.tag);
        if (col.gameObject.tag == ground) {
            isGrounded = false;
        }
    }

    void Update () {

    }
}