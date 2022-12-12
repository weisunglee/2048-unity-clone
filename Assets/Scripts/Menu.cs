using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    private AudioSource audioSource;
    private Animator textAnimation;
    private Animator fadeAnimation;
    [SerializeField] AudioClip startGameSound;

    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.Play();

        textAnimation = gameObject.transform.Find("Text").GetComponent<Animator>();
        fadeAnimation = gameObject.transform.Find("Fade").GetComponent<Animator>();
    }

    private IEnumerator WaitForSceneLoad()
    {
        yield return new WaitForSeconds(2);
        audioSource.Stop();
        SceneManager.LoadScene("StoryScene", LoadSceneMode.Single);
    }    

    // Update is called once per frame
    void Update()
    {           
        if (Input.anyKeyDown)
        {            
            textAnimation.SetTrigger("Pressed");
            fadeAnimation.SetTrigger("StartFadeOut");
            audioSource.PlayOneShot(startGameSound);
            StartCoroutine(WaitForSceneLoad());
        }
    }
}
