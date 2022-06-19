using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update

    public float speed;
    public int jumpForce;
    public int halth;
    public Transform groundCheck;

    private bool invunerable = false;
    private bool grounded = false;
    private bool jumping = false;
    private bool facingRight = true;

    private SpriteRenderer sprite;
    private Rigidbody2D rb2d;
    private Animator anim;
    private Transform trans;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        trans = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {

        float move = Input.GetAxis("Horizontal");
        rb2d.velocity = new Vector2(move * speed, rb2d.velocity.y);

        if (( move < 0f && facingRight ) || ( move > 0f && !facingRight)){
            Flip();
        }

    }

    void Flip()
    {
        facingRight = !facingRight;
        trans.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

}
