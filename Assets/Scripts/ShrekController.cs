using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
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
    private BoxCollider2D collider;
    private bool isGrounded = false;
    private bool isWalking = false;
    public bool isDead = false;
    private string ground = "Ground";
    private string shrekMode = "Shrek";
    float speed = 600f;
    float maxSpeed = 75f;
    float jumpSpeed = 160f;
    float punchDistance = 13f;
    float punchDamage = 2;
    int kickDamage = 3;
    bool freezeMovement = false;
    float health = 100;
    string direction = "right";
    public int maxHealth = 100;
    private float onionDuration = 0.3f;
    private float minChargeTime = 0.3f;
    private float maxChargeTime = 3f;
    private float maxChargeFactor = 2.5f;
    private DateTime lastChargeStart;
    private bool isOnion; // if true = protected
    private float walkAnimationTreshold = 40f;
    private Animator animator;
    public GameObject blood;
    public Slider healthSlider;
    private bool isFlipped; // Whether this shrek is flipped, false is right, true is left
    public sound mainCameraSound;
    public GameManager gameManager;
    private SpriteRenderer spriteRenderer;
    private UltStateMachine ultStateMachine = new UltStateMachine();
    public GameObject UltPrefab;

    private float ultSpeed = 50;
    private float ultDamage = 50;
    public SpriteRenderer backgroundRenderer;

    // Start is called before the first frame update
    void Start () {
        rb = GetComponent<Rigidbody2D> ();
        rb.drag = 5f;
        animator = GetComponent<Animator>();
        collider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        lastChargeStart = DateTime.Now;
        isFlipped = false;
        isOnion = false;

        mainCameraSound = GameObject.FindGameObjectsWithTag("MainCamera")[0].GetComponent<sound>();

        if (name.Equals("PlayerOne") && !PlayerInfo.player1.Equals("")) {
            shrekMode = PlayerInfo.player1;
        }
        if (name.Equals("PlayerTwo") && !PlayerInfo.player2.Equals("")) {
            shrekMode = PlayerInfo.player2;
        }
        animator.SetTrigger (shrekMode + "Idle");
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
                PunchStart();
            }

            // Kick animation
            if (Input.GetKeyDown (kick)) {
                Kick ();
            }
        }

        if (Input.GetKeyUp(punch)) {
            PunchEnd();
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

    void Attack(float damage) {
        if (gameManager.match_time <= 0) return;
        GameObject[] shreks = GameObject.FindGameObjectsWithTag(shrek);
        foreach (GameObject shrek in shreks) {
            if (this.gameObject == shrek) continue;
            var punchDirection = transform.position - shrek.transform.position;
            bool rightDirection = (punchDirection.x >= 0f) == (direction.Equals("left"));
            bool closeEnough = punchDirection.magnitude <= punchDistance;
            bool rightHeight = Math.Abs(punchDirection.y) <= collider.bounds.size.y/2;
            if (!rightDirection || !closeEnough || !rightHeight) continue;
            ShrekController script = shrek.GetComponent<ShrekController>();
            if (!script.isOnion) script.TakeDamage(damage); // Only take damage if not onion (protected)
        }
    }
    void PunchStart() {
        freezeMovement = true;
        ultStateMachine.PunchDown();
        lastChargeStart = DateTime.Now;
        StartCoroutine(freeze(0.3f));
    }

    void PunchEnd() {
        print("punchedn");
        float chargeTime = Math.Min((float)(DateTime.Now - lastChargeStart).TotalSeconds, maxChargeTime);
        float chargeFactor = Math.Max(0, (chargeTime - minChargeTime)/(maxChargeTime - minChargeTime));
        Attack(punchDamage*(1.0f + chargeFactor*maxChargeFactor));
        animator.SetTrigger (shrekMode + "Punch");
        bool doUlt = ultStateMachine.PunchUp();
        if (doUlt) performUlt();
    }
    void Kick () {
        freezeMovement = true;
        ultStateMachine.Kick();
        Attack(kickDamage);
        animator.SetTrigger (shrekMode + "Kick");
        StartCoroutine (freeze (0.45f));
    }
    void performUlt() {
        
        float xDirection = direction.Equals("right") ? 1 : -1;
        var x = rb.position.x + xDirection * (collider.bounds.size.x);
        var y = rb.position.y + 3;
        var pos = new Vector3(x, y, 0);
        var ult = UnityEngine.Object.Instantiate(UltPrefab, pos, new Quaternion());
        var ultRb = ult.GetComponent<Rigidbody2D>();
        ultRb.velocity = new Vector2(xDirection*ultSpeed, 0f);
        print("performUlt");
        mainCameraSound.PlaySwampSound();
    }

    public void TakeDamage(float damage) {
        health = Mathf.Clamp(health - damage, 0, maxHealth);
        healthSlider.value = health;
        if(health <= 0) OnDeath();
        StartCoroutine (tookHitAnimation());
        mainCameraSound.PlayHitSound();
        print("takedamage " + this.health + " " + this.healthSlider.value);
    }

    IEnumerator tookHitAnimation() {
        spriteRenderer.color = new Color(255, 0, 0);
        yield return new WaitForSeconds (0.1f);
        spriteRenderer.color = new Color(255, 255, 255);
    }

    private void OnDeath() {
        rb.rotation = -90;
        isDead = true; // PepeHands

        if(isGrounded) {
            SpawnBloodSprite();
        }

        StartCoroutine(turnDownBackgroundMusicAndPlayAllGore());
        print("dead " + name.Equals("PlayerOne") + " " + name.Equals("Player1") + " " + PlayerInfo.player1 + " " + PlayerInfo.player2);
        bool trump1 = name.Equals("PlayerOne") && PlayerInfo.player2.Equals("Shrump");
        bool trump2 = name.Equals("PlayerTwo") && PlayerInfo.player1.Equals("Shrump");
        if (trump1 || trump2) {
            var sexy = Resources.Load<Sprite>("Sprites/Background/sexy");
            backgroundRenderer.sprite = sexy;
        }
    }

    public void Revive() {
        isDead = false;
        health = maxHealth;
        rb.rotation = 0;
        healthSlider.value = health;
    }

    IEnumerator turnDownBackgroundMusicAndPlayAllGore () {
        if (mainCameraSound.backgroundSound.volume != 0) {
        yield return new WaitForSeconds (0.1f);
        mainCameraSound.backgroundSound.volume -= 0.05f;
        StartCoroutine(turnDownBackgroundMusicAndPlayAllGore());
        }
        else {
            mainCameraSound.PlayItsOgre();
        }
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
        print("collusion " + col.gameObject.tag);
        if (col.gameObject.tag == ground) isGrounded = true;
        if (col.gameObject.tag == "Ult") {
            TakeDamage(ultDamage);
            UnityEngine.Object.Destroy(col.gameObject);
        }
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