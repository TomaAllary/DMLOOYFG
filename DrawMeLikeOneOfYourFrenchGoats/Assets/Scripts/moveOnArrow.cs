using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveOnArrow : MonoBehaviour
{
    public GameObject[] arrows;
    public int current;
    // Start is called before the first frame update
    void Start()
    {
        current = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            
            arrows[current].SetActive(false);
            current--;
            if (current < 0)
            {
                current = 3;
            }
            arrows[current].SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            arrows[current].SetActive(false);
            
            current++;
            if (current == 4)
            {
                current = 0;
            }
            arrows[current].SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
        {

        }
    }
}
