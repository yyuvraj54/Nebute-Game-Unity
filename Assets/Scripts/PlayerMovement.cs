using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    InputSystem controls;
     
    [SerializeField] float speedX = 7f;
    [SerializeField] float Jumpforce = 14f;
    private Animator anim;
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private float dirX = 0f;
    private float direction = 0f;
    private BoxCollider2D coll;

    [SerializeField] private LayerMask jumpableGround; 

    private enum MovementState {idle, running , jumping , falling};

    [SerializeField] private AudioSource jumpSoundEffect;
   
    // Start is called before the first frame update
    void Start()
    { 
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        coll = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        movement();
        playerAnimation();
    }




    void Awake() {

        controls = new InputSystem();
        controls.Enable();
        controls.Land.Movement.performed += ctx => {

            dirX = ctx.ReadValue<float>();
            Debug.Log(dirX);
          
            

        };

        controls.Land.Jump.performed += ctx =>{
            if (isGrounded()) {

                jumpSoundEffect.Play();
                rb.velocity = new Vector2(rb.velocity.x, Jumpforce);

            }


        };


    }




    private void movement() {

        //dirX = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(dirX * 7f, rb.velocity.y);


        if (Input.GetButtonDown("Jump") && isGrounded())
        {
            jumpSoundEffect.Play();
            rb.velocity = new Vector2(rb.velocity.x, Jumpforce);
        }

    }

    private void playerAnimation() {

        MovementState state;
        if (dirX > 0)
        {
            state = MovementState.running;
            sprite.flipX = false;
            
        }
        else if (dirX < 0)
        {
            state = MovementState.running;
            sprite.flipX = true;
            
        }
        else
        {
            state = MovementState.idle;
        }


        if (rb.velocity.y > .1f) {
            state = MovementState.jumping;
        }
        else if(rb.velocity.y < -.1f){
            state = MovementState.falling;
        }


        anim.SetInteger("state",(int)state);


    }


    private bool isGrounded() {
       return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f,jumpableGround);
    }

    private void OnEnable() {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }
}
