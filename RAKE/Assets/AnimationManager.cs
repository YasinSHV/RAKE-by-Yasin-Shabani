using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{

    Collider2D[] inRadius = null;
    [SerializeField] private float explosionForceMulti = 5;
    [SerializeField] private float explosionRadius = 5;
    [SerializeField] AudioSource explode = null;

    public Animator playerAnimator;

    [SerializeField]
    private AudioSource enemySound = null;

    private AudioSource playerSound;
    private void Awake()
    {
        playerSound = GameObject.FindGameObjectWithTag("PlayerAudio").GetComponent<AudioSource>();
    }
    public void EndTurnning()
    {
        playerAnimator.SetBool("IsTurnning", false);
    }  
    public void EndAttack()
    {
        playerAnimator.SetBool("IsAttacking", false);
        playerAnimator.SetBool("FirstAttack", false);
        playerAnimator.SetBool("SecondAttack", false);
    }

    public void EndHurt() 
    {
        playerAnimator.SetBool("GetHit", false);
    }

    public void EnemyIsDead() 
    {
        Destroy(gameObject);
    }

    public void EndAttackMushroom() 
    {
        playerAnimator.SetBool("Attack", false);
    }

    public void PlayWalkingSound() 
    {
        playerSound.pitch = Random.Range(1.2f, 1.3f);
        playerSound.Play();
    }

    public void PlayRobotWalk() 
    {
        enemySound.pitch = Random.Range(1.18f, 1.3f);
        enemySound.Play();
    }

    public void EndOpen() 
    {
        playerAnimator.SetBool("Open", false);
    }

    void Explosion()
    {
        explode.Play();
        inRadius = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

        foreach (Collider2D o in inRadius)
        {
            Rigidbody2D o_rb = o.GetComponent<Rigidbody2D>();
            if (o_rb != null)
            {
                Vector2 distance = o.transform.position - transform.position;

                if (o.tag == "Player")
                {
                    
                    o.GetComponent<PlayerMovement>().fallen = true;

                    

                    if (distance.magnitude > 0)
                    {
                        float explosionForce = explosionForceMulti;
                        o_rb.AddForce(distance * explosionForce);
                    }
                }
            }
        }
    }

}
