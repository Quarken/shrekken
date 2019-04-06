using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ShrekController : NetworkBehaviour {

    private string shrek = "Shrek";
    private int gravity = 100;
    private Rigidbody2D rb;
    private bool isGrounded = false;
    private bool isWalking = false;
    private string ground = "Ground";
    private string shrekMode = "Shrek";
    float speed = 600f;
    float maxSpeed = 75f;
    float jumpSpeed = 100f;
    float punchDistance = 20f;
    int punchDamage = 10;
    [SyncVar]
    int health = 100;

    private float walkAnimationTreshold = 20f;

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
        if (rb.velocity.x < -walkAnimationTreshold || rb.velocity.x > walkAnimationTreshold) {
            animator.SetBool ("IsWalking", true);
        }
        else {
            animator.SetBool ("IsWalking", false);
        }

        // Jump
        if (Input.GetKey (KeyCode.UpArrow) && isGrounded) {
            rb.AddForce (new Vector2 (0, jumpSpeed), ForceMode2D.Impulse);
        }
        // Jump animation
        if (Input.GetKeyDown (KeyCode.UpArrow)) {
            animator.SetTrigger (shrekMode + "Jump");
        }
        if (Input.GetKeyDown(KeyCode.Z)) {
            Punch();
        }

        // Kick animation
        if (Input.GetKeyDown(KeyCode.X)) {
            Kick();
        }

        animator.SetBool ("IsGrounded", isGrounded);

    }
    void Punch() {
        print("I, " + this.GetComponent<NetworkIdentity>().netId.ToString() + " , am punching.");
        CmdPunch();
        animator.SetTrigger (shrekMode + "Punch");
    }
    void Kick() {
        animator.SetTrigger (shrekMode + "Kick");
    }

    [Command]
    public void CmdPunch() {
        GameObject[] shreks = GameObject.FindGameObjectsWithTag(shrek);
        foreach (GameObject shrek in shreks) {
            if (this.gameObject == shrek) continue;
            if (Vector2.Distance(transform.position, shrek.transform.position) > punchDistance) continue;
            shrek.GetComponent<ShrekController>().TakeDamage(10);
        }
    }

    public void TakeDamage(int damage) {
        print("I, " + this.GetComponent<NetworkIdentity>().netId.ToString() + " , took damage.");
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