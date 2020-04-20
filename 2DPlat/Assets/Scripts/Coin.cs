using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Coin : MonoBehaviour
{
    public LayerMask Player;
    public float aa = 0.1f;
    private AudioSource audioSource;
    public AudioClip sound;
    private bool SoundPlayed = false;

    
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = sound;
    }
    IEnumerator Check_win()
    {       
        // SceneManager.LoadScene("Victory") ; 
       if (!SoundPlayed){
            audioSource.PlayOneShot(sound);
            SoundPlayed = true;
        }
        

        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene("Victory") ; 
        // SceneManager.LoadScene("Victory") ; 
    }
    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit_box = Physics2D.Raycast(transform.position, Vector2.right, aa, Player);        
        RaycastHit2D hit_boxl = Physics2D.Raycast(transform.position,Vector2.left, aa, Player);        
            if (hit_box.collider != null ||hit_boxl.collider != null) {
                StartCoroutine(Check_win());
            }
    }
}
