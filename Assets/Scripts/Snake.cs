using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    private int health = 3;
    private float speed = 3;
    public Transform wallCheck;

    private bool facingRight = true;
    private bool touchedWall = false;

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rigidbody2D;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        touchedWall = Physics2D.Linecast(transform.position, wallCheck.position, 1 << LayerMask.NameToLayer("Ground"));

        if (touchedWall)
        {
            ExecuteFlip();
        }
    }

    void FixedUpdate()
    {
        rigidbody2D.velocity = new Vector2(speed, rigidbody2D.velocity.y);
    }

    void ExecuteFlip()
    {
        facingRight = !facingRight;
        transform.localScale = new Vector3(
            -transform.localScale.x, transform.localScale.y, transform.localScale.z);
        speed *= -1;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Player"))
        {
            Debug.Log("Colider on Player");
        }

        if (col.CompareTag("Attack"))
        {
            Debug.Log("Colider on Attack");
            DamageEnemy();
        }
    }

    IEnumerator DamageEffect()
    {
        float actualSpeed = speed;
        speed = speed * -1;

        spriteRenderer.color = Color.red;
        rigidbody2D.AddForce(new Vector2(0f, 200f));
        yield return new WaitForSeconds(0.1f);
        speed = speed * 1;
        spriteRenderer.color = Color.white;
    }

    void DamageEnemy()
    {
        health = health - 1;
        StartCoroutine(DamageEffect());

        if(health < 1)
        {
            Destroy(gameObject);
        }
    }
}
