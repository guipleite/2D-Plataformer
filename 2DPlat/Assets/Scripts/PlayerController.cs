using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
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
    public bool isFacingRight = false;		// Se está olhando pra direita 
    public static bool isAttacking;

 	private Vector3 moveDirection = Vector3.zero; // direção que o personagem se move
 	private CharacterController2D characterController;	//Componente do Char. Control
    public LayerMask mask;  // para filtrar os layers a serem analisados
    public LayerMask Box;  // para filtrar os layers a serem analisados
    public GameObject bbox;
    public GameObject plat; 
    public static bool hitBox = false;
    private AudioSource audioSource;
    public AudioClip sound;
    private bool SoundPlayed = false;


    IEnumerator Check_attack()
    {            
        yield return new WaitForSeconds(0.5f);
        isAttacking = false;
    }
    IEnumerator Check_Death()
    {            
        if (!SoundPlayed){
            audioSource.PlayOneShot(sound);
            SoundPlayed = true;
        }
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
        SceneManager.LoadScene("Death") ;
    }
    IEnumerator PassPlatform(GameObject platform) 
    {
       platform.GetComponent<EdgeCollider2D>().enabled = false;
       yield return new WaitForSeconds(1.0f);
       platform.GetComponent<EdgeCollider2D>().enabled = true;
    }

    void Start()
    {
    	characterController = GetComponent<CharacterController2D>(); //identif. o componente
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = sound;
    }

    void Update()
    {
        if(gameObject.transform.position.y<-5f)
        {
            StartCoroutine(Check_Death());
        }

        if(Input.GetKey(KeyCode.LeftArrow)||Input.GetKey(KeyCode.RightArrow)||Input.GetKey(KeyCode.A)||Input.GetKey(KeyCode.D)){
            animator.SetBool("isWalking",true);
        }

        else{
            animator.SetBool("isWalking",false);
        }
        if(!isAttacking){
        moveDirection.x = Input.GetAxis("Horizontal"); // recupera valor dos controles
        moveDirection.x *= walkSpeed;
        }

        if(moveDirection.x < 0){
            transform.eulerAngles = new Vector3(0,180,0);
            isFacingRight = false;
            RaycastHit2D hit_box = Physics2D.Raycast(transform.position, Vector2.left, 0.8f, Box);     

             if (hit_box.collider != null && isAttacking) {
                hitBox = true;
            }   
        }
        if(moveDirection.x > 0){
            transform.eulerAngles = new Vector3(0,0,0);
            isFacingRight = true;
            RaycastHit2D hit_boxr = Physics2D.Raycast(transform.position, Vector2.right, 0.8f, Box);     

            if (hit_boxr.collider != null && isAttacking) {
                hitBox = true;
            }      
        }
        
		if(isGrounded) {				// caso esteja no chão
			moveDirection.y = 0.0f;
			isJumping = false;

			if(Input.GetButton("Jump"))
			{   
                animator.SetBool("isJumping",true);
				moveDirection.y = jumpSpeed;
				isJumping = true;
			}
            else{
                animator.SetBool("isJumping",false);
                if(Input.GetButtonDown("Jump") && moveDirection.y > 0){//Soltando botão diminui pulo
                    moveDirection.y *= 0.5f;}

            }
            if(Input.GetButton("Fire1"))
			{ 
                animator.SetBool("isAttacking",true);
                isAttacking = true;
                StartCoroutine(Check_attack());
            }
            else{
                animator.SetBool("isAttacking",false);
            }
		}
 
		moveDirection.y -= gravity * Time.deltaTime;	// aplica a gravidade
		characterController.move(moveDirection * Time.deltaTime);	// move personagem	

		flags = characterController.collisionState; 	// recupera flags
		isGrounded = flags.below;				// define flag de chão

        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, 4f, mask);
        if (hit.collider != null && isGrounded) {
            // Debug.Log(hit.transform);
            transform.SetParent(plat.transform);

            if(Input.GetAxis("Vertical") < 0 && Input.GetButtonDown("Jump")) {
                moveDirection.y = -jumpSpeed;
               StartCoroutine(PassPlatform(hit.transform.gameObject));
            }
        }
        else 
        {
        transform.SetParent(null);
        }
       
    }

}


