using System.Transactions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    private float speed = 4;
    private int jumpForce = 310;
    private int health = 2;
    public Transform groundCheck;

    private bool invulnarable = false;
    private bool grounded = false;
    private bool jumping = false;
    private bool facingRight = true;

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rigidbody2D;
    private Animator animator;

    public float attackRate;
    public Transform spawnAttack;
    public GameObject attackPrefeb;
    public GameObject skull;
    private float nextAttack = 0f;

    private CamScript camScript;

    // Y - verticial
    // X - horizontal

    // Start is called before the first frame update
    // uma unica vez ao iniciar
    void Start() 
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        // e o objeto de fisica e quem sofre as ações da gravidade
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        camScript = GameObject.Find("Main Camera").GetComponent<CamScript>();
    }

    // Update is called once per frame
    // se houver 30 fps ele executara 30 vezes por segundo
    void Update() 
    {
        // ele cria uma linha imaginaria para que vc saiba se ele está encostando no chao, ultimo parametro é a camada que está tocado
        grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

        if (Input.GetButtonDown("Jump") && grounded)
        {
            jumping = true;
        }

        ExecuteAnimations();

    }

    // faz o mesmo que a Update mas indicado para RigidBodys
    // ele roda a cada 0.2s
    void FixedUpdate() 
    {
        // quando vai para a direita o valor e positivo, esquerda e negativo
        float move = Input.GetAxis("Horizontal");

        ExecuteMove(move);
        TurnLeftOrRight(move);
        ExecuteJumping();
        ExecuteAttack();
    }

    float ExecuteMove(float move) 
    {
        rigidbody2D.velocity = new Vector2(move * speed, rigidbody2D.velocity.y);
        return move;
    }

    void TurnLeftOrRight(float move) 
    {
        if(move < 0f && facingRight || move > 0f && !facingRight) {
            ExecuteFlip();
        }
    }

    void ExecuteFlip() 
    {
        facingRight = !facingRight;
        transform.localScale = new Vector3(
            -transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    void ExecuteJumping() 
    {
        if (jumping) {
            rigidbody2D.AddForce(new Vector2(0f, jumpForce));
            // para nao pular novamente e o personagem nao sair voando
            jumping = false;
        }
    }

    void ExecuteAnimations()
    {
        float playerVelocity = rigidbody2D.velocity.y;
        animator.SetFloat("VelY", playerVelocity);
        animator.SetBool("JumpFall", !grounded);
        animator.SetBool("Walk", rigidbody2D.velocity.x != 0f && grounded);
    }

    void ExecuteAttack()
    {
        if(Input.GetButton("Fire1") && grounded && Time.time > nextAttack) {
            Attack();
        }
    }

    void Attack()
    {
        animator.SetTrigger("Punch");
        nextAttack = Time.time + attackRate;

        GameObject cloneAttack = Instantiate(attackPrefeb, spawnAttack.position, spawnAttack.rotation);

        if (!facingRight) {
            cloneAttack.transform.eulerAngles = new Vector3(180, 0, 180);
        }
    }


    IEnumerator DamageEffect()
    {

        camScript.ShakeCamera(0.5f, 0.08f);

        rigidbody2D.AddForce(new Vector2(0f, 50f));

        for (float i = 0; i < 1f; i += 0.1f)
        {
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(0.1f);
        }

        invulnarable = false;
    }

    public void DamagePlayer()
    {
        if(invulnarable == false)
        {
            invulnarable = true;
            health = health - 1;
            StartCoroutine(DamageEffect());

            if (health < 1)
            {
                Debug.Log("Morreu!!!");
                KingDeath();
                Destroy(gameObject);

                // Reiniciar Scene, build index pega a atual que o jogador está;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Snake"))
        {
            Debug.Log("Colider on Player");
            DamagePlayer();
        }

        if (col.CompareTag("Coin"))
        {
            Debug.Log("Colider on Coin");
            Destroy(col.gameObject);
        }
    }

    void KingDeath()
    {
        GameObject cloneSkull = Instantiate(skull, transform.position, Quaternion.identity);
        Rigidbody2D rigidbody2DSkull = cloneSkull.GetComponent<Rigidbody2D>();
        rigidbody2DSkull.AddForce(Vector3.up * 200);
    }
}
