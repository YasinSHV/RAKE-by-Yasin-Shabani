using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller2D;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private float speed = 20f;

    float horizontalDirection = 0f;

    bool jump = false, crouch = false;

    private void Update()
    {
        horizontalDirection = Input.GetAxisRaw("Horizontal") * speed;
        animator.SetFloat("Speed", Mathf.Abs(horizontalDirection));

        if (Input.GetButtonDown("Jump"))
            jump = true;

        if (Input.GetButtonDown("Crouch"))
            crouch = true;
        else if (Input.GetButtonUp("Crouch"))
            crouch = false;

           
    }
    private void FixedUpdate()
    {
        controller2D.Move(horizontalDirection * Time.fixedDeltaTime, crouch, jump);
        jump = false;
    }

}
