using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller2D;

    private Image healthBar;
    [SerializeField]
    private Text blood;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private float speed = 20f, maxHealth = 100f;

    public float damage = 20;

    [SerializeField]
    private GameObject dialoguePanel, hpBar, panel;

    [SerializeField] AudioSource woosh, sword, hurt, music;

    float horizontalDirection = 0f, lastDirection = 0f, timer = 0, maxTime = 1.5f, currentHealth, startSpeed;

    bool jump = false, crouch = false, turn = false;
    public bool fallen = false;

    private void Start()
    { 
        startSpeed = speed;
        hpBar.transform.localScale = new Vector3(PlayerPrefs.GetFloat("Bar"), 3.3f);
        damage = PlayerPrefs.GetInt("Str");
        maxHealth = PlayerPrefs.GetInt("Hp");
        currentHealth = maxHealth;
        healthBar = GameObject.FindGameObjectWithTag("HealthBar").GetComponent<Image>();
    }

    private void Update()
    {
        blood.text = PlayerPrefs.GetInt("Blood").ToString();
        healthBar.fillAmount = currentHealth / maxHealth;
        if (!fallen && !animator.GetBool("GetHit") && dialoguePanel.activeInHierarchy == false)
        {
            if (Input.GetMouseButtonDown(0) && !animator.GetBool("IsCrouching") && !animator.GetBool("IsTurnning")
                && !animator.GetBool("IsAttacking") && dialoguePanel.activeInHierarchy == false)
            {
                horizontalDirection = 0;
                int rand = Random.Range(0, 2);
                animator.SetBool("IsAttacking", true);
               // sword.pitch;
                sword.Play();
                switch (rand)
                {
                    case 0:
                        animator.SetBool("FirstAttack", true);
                        break;
                    case 1:
                        animator.SetBool("SecondAttack", true);
                        break;
                }

            }

            if (!animator.GetBool("IsAttacking"))
            {
                horizontalDirection = Input.GetAxisRaw("Horizontal") * speed;
                animator.SetFloat("Speed", Mathf.Abs(horizontalDirection));
                if (horizontalDirection < 0 && !turn)
                {
                    animator.SetBool("IsTurnning", true);
                    turn = true;
                }
                else if (turn && horizontalDirection > 0)
                {
                    animator.SetBool("IsTurnning", true);
                    turn = false;
                }
            }




            if (Input.GetButtonDown("Jump") && !animator.GetBool("IsCrouching") && !animator.GetBool("IsRolling"))
            {
                speed += 20;
                woosh.Play();
                jump = true;
                animator.SetBool("IsRolling", true);
            }
            if(!animator.GetBool("IsRolling"))
            {
                speed = startSpeed;
            }

            if (Input.GetButtonDown("Crouch"))
                crouch = true;
            else if (Input.GetButtonUp("Crouch"))
                crouch = false;

            lastDirection = horizontalDirection;
        }
        else 
        {
            animator.SetBool("Fall", fallen);
            timer += Time.deltaTime;
            if (timer > maxTime && maxHealth > 0)
            {
                fallen = false;
                animator.SetBool("Fall", fallen);
                timer = 0;
            }
        }
           
    }
    private void FixedUpdate()
    {
        if (currentHealth > 0 )
        {
            if (dialoguePanel.activeInHierarchy == true) { horizontalDirection = 0; animator.SetFloat("Speed", 0); }
            controller2D.Move(horizontalDirection * Time.fixedDeltaTime, crouch, jump);
            jump = false;
        }
        else 
        {
            animator.SetBool("Fall", true);
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            StartCoroutine(GameOver());
        }
    }

    public void OnLanding() 
    {
        animator.SetBool("IsRolling", false);
    }

    public void OnCrouching(bool isCrouching) 
    {
        animator.SetBool("IsCrouching", isCrouching);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (currentHealth > 0)
        {
            if (collision.gameObject.CompareTag("EnemyHit") && !animator.GetBool("IsAttacking") && !animator.GetBool("IsRolling"))
            {
                currentHealth -= collision.GetComponent<HitBox>().hitBoxDamage;
                animator.SetBool("GetHit", true);
                hurt.Play();
            }
        }
        
    }

    IEnumerator GameOver() 
    {
        yield return new WaitForSeconds(1f);
        Time.timeScale = 0;
        music.pitch = 0.8f;
        panel.SetActive(true);
    }

}
