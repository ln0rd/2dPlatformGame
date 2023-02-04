//using System.Threading.Tasks.Dataflow;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float speed;
    //public int jumpForce;
    //public int health;
    //public Transform groundCheck;

    private bool invulnarable = false;
    private bool grounded = false;
    private bool jumping = false;
    private bool facingRight = true;

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rigidbody2D;
    private Animator animator;

    // Start is called before the first frame update
    // uma unica vez ao iniciar
    void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    // se houver 30 fps ele executara 30 vezes por segundo
    void Update() {
        
    }

    // faz o mesmo que a Update mas indicado para RigidBodys
    void FixedUpdate() {
        // quando vai para a direita o valor e positivo, esquerda e negativo
        float move = Input.GetAxis("Horizontal");

        rigidbody2D.velocity = new Vector2(move * speed, rigidbody2D.velocity.y);
        if(move < 0f && facingRight || move > 0f && !facingRight) {
            Flip();
        };
    }

    void Flip() {
        facingRight = !facingRight;
        transform.localScale = new Vector3(
            -transform.localScale.x, transform.localScale.y, transform.localScale.z);
        
    }
}
