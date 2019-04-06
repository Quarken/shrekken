using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrekController : MonoBehaviour {

    public string left;
    public string right;
    public string up;
    public string punch;
    public string kick;
    private string shrek = "Shrek";
    private int gravity = 100;
    private Rigidbody2D rb;
    private bool isGrounded = false;
    private bool isWalking = false;
    private string ground = "Ground";
    private string shrekMode = "Shrump";
    float speed = 600f;
    float maxSpeed = 75f;
    float jumpSpeed = 100f;
    float punchDistance = 20f;
    int punchDamage = 10;
    int health = 100;

    private float walkAnimationTreshold = 40f;

    private Animator animator;

    // Start is called before the first frame update
    void Start () {
        rb = GetComponent<Rigidbody2D> ();
        rb.drag = 5f;
        animator = GetComponent<Animator> ();

        animator.SetTrigger (shrekMode + "Idle");
    }

    void FixedUpdate () {
        // Left
        if (Input.GetKey(left)) {
            if (rb.velocity.x > -maxSpeed) {
                rb.AddForce (new Vector2 (-speed, 0));
            }
        }

        // Right
        if (Input.GetKey(right)) {
            if (rb.velocity.x < maxSpeed) {
                rb.AddForce (new Vector2 (speed, 0));
            }
        }

        // Walk animations
        if (rb.velocity.x < -walkAnimationTreshold || rb.velocity.x > walkAnimationTreshold) {
            animator.SetBool ("IsWalking", true);
        }
        else {
            animator.SetBool ("IsWalking", false);
        }

        // Jump
        if (Input.GetKey(up) && isGrounded) {
            rb.AddForce (new Vector2 (0, jumpSpeed), ForceMode2D.Impulse);
        }
        // Jump animation
        if (Input.GetKeyDown(up)) {
            animator.SetTrigger (shrekMode + "Jump");
        }
        if (Input.GetKeyDown(punch)) {
            Punch();
        }

        // Kick animation
        if (Input.GetKeyDown(kick)) {
            Kick();
        }

        animator.SetBool ("IsGrounded", isGrounded);

    }
    void Punch() {
        print("punch");
        GameObject[] shreks = GameObject.FindGameObjectsWithTag(shrek);
        foreach (var shrek in shreks) {
            if (this.gameObject == shrek) continue;
            shrek.GetComponent<ShrekController>().TakeDamage(10);
        }
        animator.SetTrigger (shrekMode + "Punch");
    }
    void Kick() {
        print("kick");
        animator.SetTrigger (shrekMode + "Kick");
    }

    public void TakeDamage(int damage) {
        this.health -= damage;
    }

    void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.tag == ground) isGrounded = true;
    }

    void OnCollisionExit2D(Collision2D col) {
        if (col.gameObject.tag == ground) isGrounded = false;

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

    void Update () {

    }

}