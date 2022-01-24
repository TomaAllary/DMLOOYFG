using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundOnSelect : MonoBehaviour
{
    public AudioSource selectSound;
    public int current = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            current--;
            if (current < 0)
            {
                current = 3;
            }
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            current++;
            if (current == 4)
            {
                current = 0;
            }
        }
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return) && current != 1)
        {
            selectSound.Play();
        }
    }
}
