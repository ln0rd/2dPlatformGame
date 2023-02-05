using System.Transactions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float speed;
    public int jumpForce;
    public int health;
    public Transform groundCheck;

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
        // e o objeto de fisica e quem sofre as ações da gravidade
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    // se houver 30 fps ele executara 30 vezes por segundo
    void Update() {
        // ele cria uma linha imaginaria para que vc saiba se ele está encostando no chao, ultimo parametro é a camada que está tocado
        grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

        if (Input.GetButtonDown("Jump") && grounded)
        {
            jumping = true;
        }

    }

    // faz o mesmo que a Update mas indicado para RigidBodys
    void FixedUpdate() {
        // quando vai para a direita o valor e positivo, esquerda e negativo
        float move = Input.GetAxis("Horizontal");

        ExecuteMove(move);
        TurnLeftOrRight(move);
        ExecuteJumping();
    }

    float ExecuteMove(float move) {
        rigidbody2D.velocity = new Vector2(move * speed, rigidbody2D.velocity.y);
        return move;
    }

    void TurnLeftOrRight(float move) {
        if(move < 0f && facingRight || move > 0f && !facingRight) {
            ExecuteFlip();
        }
    }

    void ExecuteFlip() {
        facingRight = !facingRight;
        transform.localScale = new Vector3(
            -transform.localScale.x, transform.localScale.y, transform.localScale.z);
        
    }

    void ExecuteJumping() {
        if (jumping)
        {
            rigidbody2D.AddForce(new Vector2(0f, jumpForce));
            // para nao pular novamente e o personagem nao sair voando
            jumping = false;
        }
    }
}
