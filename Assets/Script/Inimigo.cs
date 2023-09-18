using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inimigo : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float attackRange = 2f;
    public int damage = 5;
    public int maxHealth = 3;
    public int currentHealth = 0;
    public float detectionRange = 10f;

    private Transform player;
    private Animator animator;
    private bool isAttacking = false;

    

    public delegate void PlayerAttackEvent(int damage);
    public static event PlayerAttackEvent OnPlayerAttack;

    

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
        PlayerMovement.OnPlayerAttack += TakeDamage;
    }

    void Update()
    {

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange)
        {
            if (distanceToPlayer <= attackRange)
            {
                if (!isAttacking)
                {
                    StartCoroutine(Attack());
                }
            }
            else
            {
                MoveTowardsPlayer();
                animator.SetBool("ataque", true);
            }
        }
        else
        {
            StopMoving();
        }
    }

    void MoveTowardsPlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        transform.Translate(direction * moveSpeed * Time.deltaTime);
        animator.SetBool("andando", true);
    }

    void StopMoving()
    {
        animator.SetBool("andando", false);
        animator.SetBool("ataque", false);
    }

    IEnumerator Attack()
    {
        isAttacking = true;
        animator.SetBool("ataque", true);

        yield return new WaitForSeconds(0.5f); // Tempo de duração da animação de ataque

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackRange)
        {
            // Emita o evento de ataque ao jogador.
            if (OnPlayerAttack != null)
            {
                OnPlayerAttack(damage);
            }
        }

        animator.SetBool("ataque", false);
        isAttacking = false;
    }

    void TakeDamage(GameObject player)
    {
        // Verifique se o jogador que atacou é o mesmo objeto de jogador.
        if (player == gameObject)
        {
            currentHealth -= player.GetComponent<PlayerMovement>().playerDamage;

            // Verifique se o inimigo deve ser destruído.
            if (currentHealth <= 0)
            {
                Morrer();
            }
        }
    }

    void Morrer(){
        Destroy(gameObject, 0.1f); // Destrua o objeto do inimigo após um tempo
    }
}
