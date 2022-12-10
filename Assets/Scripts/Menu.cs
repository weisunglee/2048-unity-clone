using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    private AudioSource audioSource;
    private Animator textAnimation;
    [SerializeField] AudioClip startGameSound;

    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.Play();

        textAnimation = gameObject.GetComponentInChildren<Animator>();        
    }

    private IEnumerator WaitForSceneLoad()
    {
        yield return new WaitForSeconds(2);
        audioSource.Stop();
        SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
    }    

    // Update is called once per frame
    void Update()
    {           
        if (Input.anyKeyDown)
        {            
            textAnimation.SetTrigger("Pressed");
            audioSource.PlayOneShot(startGameSound);
            StartCoroutine(WaitForSceneLoad());
        }
    }
}
