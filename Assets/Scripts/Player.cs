using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update

    public float speed;
    public int jumpForce;
    public int health;
    public Transform groundCheck;

    private bool invunerable = false;
    private bool grounded = false;
    private bool jumping = false;
    private bool facingRight = true;

    private SpriteRenderer sprite;
    private Rigidbody2D rb2d;
    private Animator anim;

    public float atackRate;
    public Transform SpawnAtack;
    public GameObject AtackPrefab;
    private float nextAtack = 0;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        SpawnAtack = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {

        //VERIFICA SE O PLAYER ESTA COLIDANDO COM O CHÃO
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
            rb2d.AddForce(new Vector2(0f, jumpForce));
            jumping = false;
        }

        SetAnimations();


        if( Input.GetButton("Fire1") && grounded && Time.time > nextAtack)
        {
            Atack();
        }

    }

    void Flip()
    {
        facingRight = !facingRight;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    void SetAnimations()
    {
        anim.SetFloat("ValY", rb2d.velocity.y);
        anim.SetBool("JumpFall", !grounded);
        anim.SetBool("Walk", rb2d.velocity.x != 0f && grounded);
    }

    void Atack()
    {
        anim.SetTrigger("Punch");

        nextAtack = Time.time + atackRate;
        GameObject cloneAtack = Instantiate( AtackPrefab, SpawnAtack.position, SpawnAtack.rotation );

        if( !facingRight)
        {
            cloneAtack.transform.eulerAngles = new Vector3(180, 0, 180);
        }

    }

}
