using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundOnArrow : MonoBehaviour
{
    public AudioSource bebeepSound;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            bebeepSound.Play();
        }
    }
}
