using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DelayedText : MonoBehaviour
{
    private Text text;
    private string leadingChar = "_";
    private string fullString;
    private AudioSource audioSource;

    [SerializeField] AudioClip typingSound;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        fullString = text.text;
        text.text = "";

        audioSource = gameObject.GetComponent<AudioSource>();

        StartCoroutine("PrintDelayedText");
    }

    IEnumerator PrintDelayedText()
    {
        text.text = leadingChar;

        yield return new WaitForSeconds(1);

        foreach (char c in fullString)
        {
            if (text.text.Length > 0)
            {
                text.text = text.text.Substring(0, text.text.Length - leadingChar.Length);
            }
            text.text += c;
            text.text += leadingChar;
            audioSource.PlayOneShot(typingSound, 0.1f);

            yield return new WaitForSeconds(0.1f);
        }
                
        text.text = text.text.Substring(0, text.text.Length - leadingChar.Length);        
    }
}
