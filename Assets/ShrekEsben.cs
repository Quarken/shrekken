using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shrekkers : MonoBehaviour {

    // This defines which Shrek figure we are using
    private static string mode = "Shreikh";

    private static string ground = "Ground";
    private bool isOnGround = false;

    public SpriteRenderer spriteRenderer;

    public Rigidbody2D rb;

    private Sprite idle0;
    private Sprite walk0;
    private Sprite walk1;
    private Sprite jump0;
    private Sprite punch0;
    private Sprite punch1;
    private Sprite kick0;
    private Sprite kick1;

    private bool isWalking;

    void Start() {
       idle0 = Resources.Load<Sprite>("Sprites/Idle/" + mode + "Idle0");
       walk0 = Resources.Load<Sprite>("Sprites/Walk/" + mode + "Walk0");
       walk1 = Resources.Load<Sprite>("Sprites/Walk/" + mode + "Walk1");
       jump0 = Resources.Load<Sprite>("Sprites/Jump/" + mode + "Jump0");
       punch0 = Resources.Load<Sprite>("Sprites/Punch/" + mode + "Punch0");
       punch1 = Resources.Load<Sprite>("Sprites/Punch/" + mode + "Punch1");
       kick0 = Resources.Load<Sprite>("Sprites/Kick/" + mode + "Kick0");
       kick1 = Resources.Load<Sprite>("Sprites/Kick/" + mode + "Kick1");
    }

    float timer;

    void Walk0() {
        spriteRenderer.sprite = walk0;
    }
    void Walk1() {
        spriteRenderer.sprite = walk1;
    }

    void Update() {

        // Left
        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            isWalking = true;
            rb.velocity += new Vector2(-15, 0);
            InvokeRepeating("Walk0", 0f, 0.3f);
            InvokeRepeating("Walk1", 0.15f, 0.3f);
            spriteRenderer.flipX = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow)) {
            spriteRenderer.sprite = idle0;
            isWalking = false;
            rb.velocity = new Vector2(0, 0);
            CancelInvoke();
        }

        print("timer = " + timer + ", delta = " + Time.deltaTime); // Debug
        // Right
        if (Input.GetKeyDown(KeyCode.RightArrow)) {
            isWalking = true;
            rb.velocity += new Vector2(15, 0);
            InvokeRepeating("Walk0", 0f, 0.3f);
            InvokeRepeating("Walk1", 0.15f, 0.3f);
            spriteRenderer.flipX = false;
        }

        if (Input.GetKeyUp(KeyCode.RightArrow)) {
            spriteRenderer.sprite = idle0;
            isWalking = false;
            CancelInvoke();
            rb.velocity = new Vector2(0, 0);
        }

        // Jump
        if (Input.GetKeyDown(KeyCode.UpArrow) && isOnGround) {
                rb.velocity += new Vector2(0, 10);
        }

        // Punch
        if (Input.GetKeyDown(KeyCode.P)) {
                StartCoroutine(PunchAnimation());
        }

        // Kick
        if (Input.GetKeyDown(KeyCode.K)) {
                StartCoroutine(KickAnimation());
        }

    }
    // This is probably the best approach for the animations and should be used for walk instead of the InvokeRepeating i use now...
    IEnumerator PunchAnimation() {
        spriteRenderer.sprite = punch0;
        yield return new WaitForSeconds(0.05f);
        spriteRenderer.sprite = punch1;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.sprite = idle0;
    }
    IEnumerator KickAnimation() {
        spriteRenderer.sprite = kick0;
        yield return new WaitForSeconds(0.10f);
        spriteRenderer.sprite = kick1;
        yield return new WaitForSeconds(0.15f);
        spriteRenderer.sprite = idle0;
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.name == ground) {
            spriteRenderer.sprite = idle0;
            isOnGround = true;

        }
    }

    void OnCollisionExit2D(Collision2D collision) {
        if (collision.gameObject.name == ground) {
            spriteRenderer.sprite = jump0;
            isOnGround = false;
        }
    }

}
