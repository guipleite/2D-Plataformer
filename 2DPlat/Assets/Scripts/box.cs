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
    public AudioSource audioSource;
    public AudioClip sound;
    private bool SoundPlayed = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = sound;
    }

    // Update is called once per frame
    public IEnumerator DestroyBox()
    {    
        yield return new WaitForSeconds(0.2f);

        animator.SetBool("boxisDestroyed",true);
        yield return new WaitForSeconds(0.1f);

         if (!SoundPlayed){
            audioSource.PlayOneShot(sound);
            SoundPlayed = true;
        }
        yield return new WaitForSeconds(0.7f);
        PlayerController.hitBox = false;
        Destroy(gameObject);
    }
    
    void Update()
    {      
        if(PlayerController.hitBox){
            StartCoroutine(DestroyBox());
        }
    }
}