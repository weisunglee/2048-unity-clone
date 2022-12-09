using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    [SerializeField] Text valueDisplay;
    private float speed = 2000f;
    private int value;
    public int Value    
    {
        get 
        { 
            return value; 
        }
        set 
        {
            this.value = value;
            valueDisplay.text = value.ToString();
        }
    }

    private void Update()
    {
        if (transform.localPosition != Vector3.zero)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, Vector3.zero, speed * Time.deltaTime);
        }
    }    
}
