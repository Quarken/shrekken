using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShrekController : MonoBehaviour {

    public string left;
    public string right;
    public string up;
    public string down;
    public string punch;
    public string kick;
    private string shrek = "Shrek";
    private int gravity = 100;
    private Rigidbody2D rb;
    private bool isGrounded = false;
    private bool isWalking = false;
    public bool isDead = false;
    private string ground = "Ground";
    public string shrekMode = "Shrump";
    float speed = 600f;
    float maxSpeed = 75f;
    float jumpSpeed = 160f;
    float punchDistance = 20f;
    int punchDamage = 10;
    int kickDamage = 15;
    bool freezeMovement = false;
    int health = 100;
    string direction = "right";
    public int maxHealth = 100;
    private float onionDuration = 0.3f;
    private bool isOnion; // if true = protected
    private float walkAnimationTreshold = 40f;
    private Animator animator;
    public GameObject blood;
    public Slider healthSlider;

    private bool isFlipped; // Whether this shrek is flipped, false is right, true is left
    public sound mainCameraSound;

    // Start is called before the first frame update
    void Start () {
        rb = GetComponent<Rigidbody2D> ();
        rb.drag = 5f;
        animator = GetComponent<Animator> ();
        animator.SetTrigger (shrekMode + "Idle");

        isFlipped = false;
        isOnion = false;

        mainCameraSound = GameObject.FindGameObjectsWithTag("MainCamera")[0].GetComponent<sound>();
    }

    void rotateChar(string direction) {
        if (direction == "right") {
            animator.transform.Rotate(0, 180, 0);
        }
        else {
            animator.transform.Rotate(0, -180, 0);
        }
    }

    void Update () {
        if(isDead) return;

        if (!freezeMovement) {
            // Stop float
            if (!Input.GetKey(left) && !Input.GetKey(right) && isGrounded) {
                rb.velocity = Vector2.zero;
            }

            // Left
            if (Input.GetKey (left)) {
                if (direction != "left") {
                    direction = "left";
                    rotateChar(direction);
                }
                if (rb.velocity.x > -maxSpeed) {
                    rb.AddForce (new Vector2 (-speed, 0));
                }
            }

            // Right
            if (Input.GetKey (right)) {
                if (direction != "right") {
                    direction = "right";
                    rotateChar(direction);
                }
                if (rb.velocity.x < maxSpeed) {
                    rb.AddForce (new Vector2 (speed, 0));
                }
            }
            //Walk animations
            if (rb.velocity.x < -walkAnimationTreshold || rb.velocity.x > walkAnimationTreshold) {
               animator.SetBool ("IsWalking", true);
            } else {
                animator.SetBool ("IsWalking", false);
            }

            // Jump
            if (Input.GetKey (up) && isGrounded) {
                rb.velocity = Vector3.zero;
                rb.AddForce (new Vector2 (0, jumpSpeed), ForceMode2D.Impulse);
                //rigidbody.AddForce(Vector3.up * jumpHeight, ForceMode.VelocityChange);
            }
            // Jump animation
            if (Input.GetKeyDown (up)) {
                animator.SetTrigger (shrekMode + "Jump");
            }
            if (Input.GetKeyDown (punch)) {
                Punch ();
            }

            // Kick animation
            if (Input.GetKeyDown (kick)) {
                Kick ();
            }
        }

        // Onion protection
        if (Input.GetKeyDown (down)) {
            Onion();
        }

        animator.SetBool ("IsGrounded", isGrounded);
    }

    IEnumerator freeze (float time) {
        yield return new WaitForSeconds (time);
        freezeMovement = false;
    }

    void Attack(int damage) {
        GameObject[] shreks = GameObject.FindGameObjectsWithTag(shrek);
        foreach (GameObject shrek in shreks) {
            if (this.gameObject == shrek) continue;
            var punchDirection = transform.position - shrek.transform.position;
            bool rightDirection = (punchDirection.x >= 0f) == (direction.Equals("left"));
            bool closeEnough = punchDirection.magnitude <= punchDistance;
            if (!rightDirection || !closeEnough) continue;
            ShrekController script = shrek.GetComponent<ShrekController>();
            if (!script.isOnion) script.TakeDamage(damage); // Only take damage if not onion (protected)
        }
    }
    void Punch() {
        freezeMovement = true;
        Attack(punchDamage);
        StartCoroutine(freeze(0.3f));
        animator.SetTrigger (shrekMode + "Punch");

    }
    void Kick () {
        freezeMovement = true;
        Attack(kickDamage);
        animator.SetTrigger (shrekMode + "Kick");
        StartCoroutine (freeze (0.45f));
    }

    public void TakeDamage(int damage) {
        health = Mathf.Clamp(health - damage, 0, maxHealth);
        healthSlider.value = health;
        if(health <= 0) OnDeath();
        
        print("takedamage " + this.health + " " + this.healthSlider.value);
    }

    private void OnDeath() {
        rb.rotation = -90;
        isDead = true; // PepeHands

        if(isGrounded) {
            SpawnBloodSprite();
        }

        mainCameraSound.PlayItsOgre();
    }

    private void SpawnBloodSprite() {
        var pos = transform.position;
        pos.z = 2;
        Instantiate(blood, pos, Quaternion.identity);
    }

    void Onion() {
        isOnion = true;
        animator.SetTrigger (shrekMode + "Onion");
        StartCoroutine (hasProtection());
    }

    IEnumerator hasProtection () {
        yield return new WaitForSeconds (onionDuration);
        isOnion = false;
    }

    void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.tag == ground) isGrounded = true;

        if(isDead) {
            SpawnBloodSprite();
        }
    }

    void OnCollisionExit2D (Collision2D col) {
        if (col.gameObject.tag == ground) isGrounded = false;

        // Punch animation
        if (Input.GetKeyDown (KeyCode.Z)) {
            animator.SetTrigger (shrekMode + "Punch");
        }

        // Kick animation
        if (Input.GetKeyDown (KeyCode.X)) {
            animator.SetTrigger (shrekMode + "Kick");
        }

        animator.SetBool ("IsGrounded", isGrounded);

    }

}