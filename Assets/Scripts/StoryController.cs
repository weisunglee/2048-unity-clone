using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoryController : MonoBehaviour
{
    private Animator fadeAnimation;

    // Start is called before the first frame update
    void Start()
    {
        fadeAnimation = gameObject.transform.Find("Fade").GetComponent<Animator>();
    }

    private IEnumerator WaitForSceneLoad()
    {
        yield return new WaitForSeconds(2);        
        SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            fadeAnimation.SetTrigger("StartFadeOut");
            StartCoroutine(WaitForSceneLoad());
        }
    }
}
