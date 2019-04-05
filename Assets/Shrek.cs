using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shrek : MonoBehaviour {

    private static string ground = "Ground";
    private bool isOnGround = false;
    private Sprite[] sprites;

    public SpriteRenderer spriteRenderer;

    private Sprite still;
    private Sprite walk;

    public Rigidbody2D rb;

    void Start() {
       
    }

    void Update() {

        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            rb.velocity += new Vector2(-15, 0);
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow)) {
            rb.velocity = new Vector2(0, 0);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow)) {
            rb.velocity += new Vector2(15, 0);
        }
        if (Input.GetKeyUp(KeyCode.RightArrow)) {
            rb.velocity = new Vector2(0, 0);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && isOnGround) {
                rb.velocity += new Vector2(0, 20);
        }

    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.name == ground) {
            isOnGround = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision) {
        if (collision.gameObject.name == ground) {
            isOnGround = false;
        }
    }

    void movement(KeyCode down, KeyCode up) {

    }

}
