using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrekController : MonoBehaviour
{
    private int gravity = 100;
    private Rigidbody2D rb;
    private bool isGrounded = false;
    private string ground = "Ground";
    private string shrek = "Shrek";
    float speed = 600f;
    float maxSpeed = 75f;
    float jumpSpeed = 150f;
    float punchDistance = 50f;
    int punchDamage = 10;
    int health = 100;

    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        rb.drag = 5f;
    }


    void FixedUpdate() {
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
        if (Input.GetKeyDown(KeyCode.Z)) {
            Punch();
        }
    }
    void Punch() {
        print("Punch");
        GameObject[] shreks = GameObject.FindGameObjectsWithTag(shrek);
        foreach (GameObject shrek in shreks) {
            if (this.gameObject == shrek) continue;
            if (Vector2.Distance(transform.position, shrek.transform.position) > punchDistance) continue;
            print("found shrek");
            ShrekController script = gameObject.GetComponent<ShrekController>(); 
            script.takeDamage(punchDamage);
        }
    }

    public void takeDamage(int damage) {
        print("take damage");
        this.health -= damage;
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
