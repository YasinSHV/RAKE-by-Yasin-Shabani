using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float speed = 3f;

    [SerializeField]
    private float range;

    [SerializeField]
    private float maxHealth = 100f, attackCoolDown = 1f;

    [SerializeField]
    private Image healthBar;

    [SerializeField]
    private GameObject particle;

    [SerializeField]
    private AudioSource audioSource;

    private float timer = 0, currentHealth;
    private Transform player;

    private Rigidbody2D rb;

    private bool flipped, particleFlip;

    private Animator animator;


    Vector2 playerPos, enemyPos;
    Vector3 scale;

    private float dist;
    void Awake()
    {
        currentHealth = maxHealth;
        timer = attackCoolDown + 1;
        scale = transform.localScale;
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
    }


    void Update()
    {
        healthBar.fillAmount = currentHealth / maxHealth;
        if (healthBar.fillAmount <= 0)
        {
            animator.SetBool("IsDead", true);
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        }


        if (!animator.GetBool("GetHit") && !animator.GetBool("IsDead") && !animator.GetBool("Attack"))
        {
            scale = transform.localScale;
            if (player.position.x < rb.position.x)
            {
                if (!flipped)
                {
                    transform.localScale = new Vector3(scale.x * -1, scale.y);
                    if (!particleFlip)
                    {
                        particle.transform.localScale = new Vector3(Mathf.Abs(particle.transform.localScale.x), particle.transform.localScale.y);
                        particleFlip = true;
                    }
                    flipped = true;
                }
            }
            else
            {
                if (particleFlip)
                {
                    particle.transform.localScale = new Vector3(particle.transform.localScale.x * -1, particle.transform.localScale.y);
                    particleFlip = false;
                }
                transform.localScale = new Vector3(Mathf.Abs(scale.x), scale.y);
                flipped = false;
            }


            enemyPos = rb.position;
            playerPos = player.position;
            dist = (playerPos - enemyPos).magnitude;
            Vector2 aim_Vector = playerPos - enemyPos;
            rb.velocity = new Vector2(aim_Vector.x * speed, 0);
            if (dist < range)
            {
                if (timer > attackCoolDown)
                {
                    animator.SetBool("Attack", true);
                    timer = 0;
                }
                timer += Time.deltaTime;
            }
              
            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (currentHealth > 0)
        {
            if (collision.gameObject.CompareTag("HitBox") && !animator.GetBool("Attack"))
            {
                animator.SetBool("GetHit", true);
                currentHealth -= PlayerPrefs.GetInt("Str");
                particle.GetComponent<ParticleSystem>().Play();
                audioSource.pitch = Random.Range(1.7f, 2f);
                audioSource.Play();
            }
        }
    }
}
