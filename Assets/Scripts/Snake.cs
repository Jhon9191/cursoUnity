using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    public Transform wallCheck;
    public float speed;
    public int health;

    private bool facingRight = true;
    private bool touchWall = false;

    private SpriteRenderer sprite;
    private Rigidbody2D rb2d;

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        touchWall = Physics2D.Linecast(transform.position, wallCheck.position, 1 << LayerMask.NameToLayer("Ground"));

        if (touchWall)
        {
            Flip();
        }

    }

    void Flip()
    {
        facingRight = !facingRight;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        speed *= -1;
    }


    private void FixedUpdate()
    {
        rb2d.velocity = new Vector2(speed, rb2d.velocity.y);
    }


}
