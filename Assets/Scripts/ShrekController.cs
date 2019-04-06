using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrekController : MonoBehaviour
{
    private int speed = 80;
    private int gravity = 100;
    private Rigidbody2D rb;
    private bool isGrounded = false;
    private string ground = "Ground";

    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        rb.drag = 5f;
    }


    void FixedUpdate() {
        float speed = 600f;
        float maxSpeed = 75f;
        float jumpSpeed = 150f;
        if (Input.GetKey(KeyCode.RightArrow)) {
            if (rb.velocity.x < maxSpeed)
                rb.AddForce(new Vector2(speed, 0));
        }
        if (Input.GetKey(KeyCode.LeftArrow)) {
            if (rb.velocity.x > -maxSpeed)
                rb.AddForce(new Vector2(-speed, 0));
        }
        if (Input.GetKey(KeyCode.UpArrow) && isGrounded) {
            rb.AddForce(new Vector2(0, jumpSpeed), ForceMode2D.Impulse);
        }
    }

    void OnCollisionEnter2D(Collision2D col) {
        print("enter " + col.gameObject.tag);
        if (col.gameObject.tag == ground) isGrounded = true;
    }

    void OnCollisionExit2D(Collision2D col) {
        print("exit " + col.gameObject.tag);
        if (col.gameObject.tag == ground) isGrounded = false;
    }

    // Update is called once per frame
    void Update() {
        
    }
}
