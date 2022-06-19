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
    }

    // Update is called once per frame
    void Update()
    {

        grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

        if ( Input.GetButtonDown("Jump") && grounded)
        {
            jumping = true;
        }

    }

    private void FixedUpdate()
    {
        //SCRIPT PARA VIRAR O JOGADOR
        float move = Input.GetAxis("Horizontal");
        rb2d.velocity = new Vector2(move * speed, rb2d.velocity.y);

        if (( move < 0f && facingRight ) || ( move > 0f && !facingRight)){
            Flip();
        }

        //SCRIPT PARA PULAR O JOGADOR
        if (jumping)
        {
            rb2d.AddForce(new Vector2(0, jumpForce));
            jumping = false;
        }

    }

    void Flip()
    {
        facingRight = !facingRight;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

}
