using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DelayedText : MonoBehaviour
{
    private Text text;
    private string fullString;
    private AudioSource audioSource;
    private const float delayTime = 0.05f;

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
        yield return new WaitForSeconds(1);

        for (int i = 0; i < fullString.Length; ++i)
        {
            string newText = fullString.Substring(0, i);
            newText += "<color=#00000000>" + fullString.Substring(i) + "</color>";
            text.text = newText;

            audioSource.PlayOneShot(typingSound, 0.1f);

            yield return new WaitForSeconds(delayTime);
        }

        text.text = fullString;
    }
}
