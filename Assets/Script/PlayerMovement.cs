using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Animator animator;

    private float horizontal;
    public int speed = 8;
    public int Vida = 8;
    public int jumpingPower = 16;
    
    private bool isFacingRight = true;
    private bool isAttacking = false; // Variável booleana para controlar o estado de ataque.

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    void Start()
    {
        animator = GetComponent<Animator>();
        Inimigo.OnPlayerAttack += TakeDamage;
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        // Detecta se o jogador está se movendo horizontalmente.
        bool isMoving = Mathf.Abs(horizontal) > 0;

        // Define o parâmetro "andando" no Animator com base no movimento.
        animator.SetBool("andando", isMoving);

        // Verifica se o jogador está no chão.
        bool isGrounded = IsGrounded();

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }

        // Adicione a chamada do método de ataque aqui (por exemplo, quando a tecla de ataque for pressionada).
        if (Input.GetButtonDown("Fire1")) // Supondo que "Fire1" seja a tecla de ataque.
        {
            if (!isAttacking)
            { //Inicie o ataque.
                GetComponent<Animator>().SetBool("ataque", true); // Ative a animação de ataque.
            }
        }
        else
        {
            GetComponent<Animator>().SetBool("ataque", false);
        }

        if (Input.GetButtonDown("Fire2")) // Supondo que "Fire1" seja a tecla de ataque.
        {
            if (!isAttacking)
            { //Inicie o ataque.
                GetComponent<Animator>().SetBool("magia", true); // Ative a animação de ataque.
            }
        }
        else
        {
            GetComponent<Animator>().SetBool("magia", false);
        }
        

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        Flip();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

     void TakeDamage(int damage)
    {
        Vida -= damage;

        // Verifique se o jogador morreu.
        if (Vida <= 0)
        {
         
        }
    }


}
