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
    float punchDistance = 200f;
    int punchDamage = 10;
    bool freezeMovement = false;
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
        if (!freezeMovement) {
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
            if (Input.GetKeyDown(KeyCode.A)) {
                Kick();
            }
        }
        animator.SetBool ("IsGrounded", isGrounded);
    }

    IEnumerator freeze(float time) {
        yield return new WaitForSeconds(time);
        freezeMovement = false;
    }

    void Punch() {
        freezeMovement = true;
        print("Punch");
        GameObject[] shreks = GameObject.FindGameObjectsWithTag(shrek);
        foreach (GameObject shrek in shreks) {
            if (this.gameObject == shrek) continue;
            if (Vector2.Distance(transform.position, shrek.transform.position) > punchDistance) continue;
            print("found shrek");
            ShrekController script = gameObject.GetComponent<ShrekController>(); 
            script.CmdTakeDamage(punchDamage);
        }
        StartCoroutine(freeze(0.5f));
        animator.SetTrigger (shrekMode + "Punch");
        
    }

    void Kick() {
        freezeMovement = true;
        animator.SetTrigger (shrekMode + "Kick");
        StartCoroutine(freeze(0.5f));
    }

    [Command]
    public void CmdTakeDamage(int damage) {
        this.health -= damage;
        print("take damage " + this.health);
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