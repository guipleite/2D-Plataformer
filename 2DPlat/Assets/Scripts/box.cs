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

    void OnTriggerEnter2D(Collider2D collision){
        Debug.Log("1");
        if (collision.gameObject.tag  == "Player"){
                    Debug.Log("2");

            if(PlayerController.isAttacking){
                Debug.Log("3");

                animator.SetBool("boxisDestroyed",true);
                Destroy(gameObject);

            }
        }
    }
    IEnumerator Check_attack()
    {    Debug.Log("2");
        if(PlayerController.isAttacking){
            Debug.Log("3");

            animator.SetBool("boxisDestroyed",true);
            Debug.Log("4");
            yield return new WaitForSeconds(0.85f);
        
            Destroy(gameObject);
        }
    }
    void Update()
    {
         StartCoroutine(Check_attack());
    }
}
