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

    // Start is called before the first frame update
    void Start()
    {
        collision = GetComponent<BoxCollider2D>();
    }


    void FixedUpdate()
    {
        transform.Translate(velocity * Time.fixedDeltaTime);

        if (Input.GetButtonDown("Jump") && is_grounded)
        {
            velocity.y = jump_height;
        }

        var horizontal_input = Input.GetAxis("Horizontal");
        velocity.x = horizontal_input * speed;

        velocity.y -= gravity * Time.fixedDeltaTime;

        Collider2D[] hits = Physics2D.OverlapBoxAll(transform.position, collision.size, 0);

        foreach (Collider2D hit in hits)
        {
            if (hit == collision)
                continue;

            ColliderDistance2D collider_distance = hit.Distance(collision);

            is_grounded = false;
            if (Vector2.Angle(collider_distance.normal, Vector2.up) < 90 && velocity.y < 0)
            {
                velocity.y = 0;
                is_grounded = true;
            }

            if (collider_distance.isOverlapped)
            {
                transform.Translate(collider_distance.pointA - collider_distance.pointB);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
