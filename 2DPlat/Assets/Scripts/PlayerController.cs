using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prime31;

public class PlayerController : MonoBehaviour
{
    Animator animator;

 	public CharacterController2D.CharacterCollisionState2D flags;
 	public float walkSpeed = 4.0f;     // Depois de incluido, alterar no Unity Editor
 	public float jumpSpeed = 8.0f;     // Depois de incluido, alterar no Unity Editor
 	public float gravity = 9.8f;       // Depois de incluido, alterar no Unity Editor

 	public bool isGrounded;		// Se está no chão
 	public bool isJumping;		// Se está pulando

 	private Vector3 moveDirection = Vector3.zero; // direção que o personagem se move
 	private CharacterController2D characterController;	//Componente do Char. Control

    void Start()
    {
    	characterController = GetComponent<CharacterController2D>(); //identif. o componente
        animator = GetComponent<Animator>();

    }

    void Update()
    {
        if(Input.GetKey(KeyCode.LeftArrow)||Input.GetKey(KeyCode.RightArrow)){
            animator.SetBool("isWalking",true);
        }

        else{
            animator.SetBool("isWalking",false);
        }

        moveDirection.x = Input.GetAxis("Horizontal"); // recupera valor dos controles
        moveDirection.x *= walkSpeed;

        if(moveDirection.x < 0) {
            transform.eulerAngles = new Vector3(0,180,0);
        }
        else
        {
            transform.eulerAngles = new Vector3(0,0,0);
        }
        
		if(isGrounded) {				// caso esteja no chão
			moveDirection.y = 0.0f;
			isJumping = false;

			if(Input.GetButton("Jump"))
			{   
                Debug.Log("jmp");
                animator.SetBool("isJumping",true);
				moveDirection.y = jumpSpeed;
				isJumping = true;
			}
            else{
                animator.SetBool("isJumping",false);
            }

		}

		moveDirection.y -= gravity * Time.deltaTime;	// aplica a gravidade
		characterController.move(moveDirection * Time.deltaTime);	// move personagem	

		flags = characterController.collisionState; 	// recupera flags
		isGrounded = flags.below;				// define flag de chão

    }
}
