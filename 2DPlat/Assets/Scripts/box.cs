using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class box : MonoBehaviour
{
    // Start is called before the first frame update
    // public bool isAttacking;
    public bool boxisDestroyed;
    public bool destroy = false;


    Animator animator;


    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame

    private void OnCollisionEnter2D(Collision2D  collision){
        Debug.Log("1");
        if (collision.gameObject.tag  == "Player"){
            StartCoroutine(Check_attack());
        }
    }

    // Update is called once per frame
    IEnumerator Check_attack()
    {   
        if(PlayerController.isAttacking){
            animator.SetBool("boxisDestroyed",true);
            yield return new WaitForSeconds(0.85f);
            Destroy(gameObject);
        }
    }
    void Update()
    {   
        // RaycastHit2D hit_box = Physics2D.Raycast(transform.position, -Vector2.up, aa, Player);        
        // Debug.Log(hit_box.collider);
        //     if (hit_box.collider != null && PlayerController.isAttacking) {
        //         StartCoroutine(Check_attack());
        //     }
    }
}
