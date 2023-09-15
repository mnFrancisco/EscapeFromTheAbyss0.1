using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Animator animator;

    private float horizontal;
    public int speed = 8;
    public int jumpingPower = 16;
    private bool isFacingRight = true;
 
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    void Start(){

        animator = GetComponent<Animator>() ;

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
}