using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrekController : MonoBehaviour
{
    private bool is_grounded = false;
    private BoxCollider2D collision;
    private Vector2 velocity = new Vector2(0,0);
    private int speed = 80;
    private int gravity = 100;
    private int jump_height = 60;
    private int counter = 0;
    private float epsilon = 0.01f;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        rb.drag = 5f;
    }


    void FixedUpdate() {
        float speed = 600;
        float maxSpeed = 75;
        if (Input.GetKey(KeyCode.RightArrow)) {
            if (rb.velocity.x < maxSpeed)
                rb.AddForce(new Vector2(speed, 0));
        }
        if (Input.GetKey(KeyCode.LeftArrow)) {
            if (rb.velocity.x > -maxSpeed)
                rb.AddForce(new Vector2(-speed, 0));
        }


        /* transform.Translate(velocity * Time.fixedDeltaTime);

        if (Input.GetButtonDown("Jump") && is_grounded) {
            velocity.y = jump_height;
        }

        var horizontal_input = Input.GetAxis("Horizontal");
        velocity.x = horizontal_input * speed;

        velocity.y -= gravity * Time.fixedDeltaTime;

        Collider2D[] hits = Physics2D.OverlapBoxAll(transform.position, collision.size, 0);
        is_grounded = false;
        foreach (Collider2D hit in hits) {
            if (hit == collision)
                continue;

            ColliderDistance2D collider_distance = hit.Distance(collision);
            counter++;
            print(counter + " " + transform.position + " " + hit + " " + collider_distance.normal);
            
            if (Vector2.Angle(collider_distance.normal, Vector2.up) < 90 && velocity.y < 0) {
                velocity.y = 0;
                is_grounded = true;
            }

            if (collider_distance.isOverlapped) {
                print("overlapped");
                transform.Translate(collider_distance.pointA - collider_distance.pointB);
            }
        }*/
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
