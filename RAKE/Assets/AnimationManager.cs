using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
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

}
