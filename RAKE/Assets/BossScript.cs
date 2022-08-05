using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossScript : MonoBehaviour
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

    [SerializeField]
    private float borderNegative, borderPositive;

    private float timer = 0, currentHealth, startSpeed;
    private Transform player;

    private Rigidbody2D rb;

    private bool flipped, particleFlip;

    private Animator animator;


    Vector2 playerPos, enemyPos;
    Vector3 scale;

    private float dist;
    void Awake()
    {
        startSpeed = speed;
        currentHealth = maxHealth;
        timer = attackCoolDown + 1;
        scale = transform.localScale;
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
    }


    void FixedUpdate()
    {
        if (!animator.GetBool("IsAttacking") && !animator.GetBool("GetHit"))
            SetSpeedBack();
        healthBar.fillAmount = currentHealth / maxHealth;
        if (healthBar.fillAmount <= 0)
        {
            animator.SetBool("IsDead", true);
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        }


        if (!animator.GetBool("GetHit") && !animator.GetBool("IsDead"))
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
            animator.SetFloat("Speed",Mathf.Abs(rb.velocity.x));
            if (dist < range)
            {
                if (timer > attackCoolDown)
                {
                    if (!animator.GetBool("IsAttacking"))
                    {
                        int rand = Random.Range(1, 4);
                        animator.SetBool("IsAttacking", true);
                        if (rand == 1)
                        {
                            animator.SetBool("FirstAttack", true);
                        }
                        else 
                        {
                            animator.SetBool("SecondAttack", true);
                        }
                    }
                    timer = 0;
                }
            }
            timer += Time.deltaTime;

        }
        Debug.Log(speed);
        Debug.Log(rb.velocity.x);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (currentHealth > 0)
        {
            if (collision.gameObject.CompareTag("HitBox") && !animator.GetBool("IsAttacking"))
            {
                int rand = Random.Range(1, 5);
                if (rand != 4)
                {
                    animator.SetBool("GetHit", true);
                    currentHealth -= player.GetComponent<PlayerMovement>().damage;
                    particle.GetComponent<ParticleSystem>().Play();
                    audioSource.pitch = Random.Range(1.7f, 2f);
                    audioSource.Play();
                }
                else 
                {
                    animator.SetBool("Teleport", true);
                    StartCoroutine(FallBack());
                }
            }
        }

        IEnumerator FallBack() 
        {
            yield return new WaitForSeconds(0.2f);
            if (player.position.x > transform.position.x)
            {
                transform.position = new Vector2(transform.position.x + 8, transform.position.y);
                if(borderNegative > transform.position.x)
                    transform.position = new Vector2(transform.position.x - 16, transform.position.y);
            }
            else if (transform.position.x > player.position.x)
            {
                transform.position = new Vector2(transform.position.x - 8, transform.position.y);
                if (borderPositive < transform.position.x)
                    transform.position = new Vector2(transform.position.x + 16, transform.position.y);
            }
        }
    }


    public void SetSpeedZero() 
    {
        speed = 0;
    }

    public void Dash() 
    {
        speed = 1;
    }

    public void SetSpeedBack() 
    {
        speed = Random.Range(startSpeed , startSpeed + 0.2f);
    }
}
