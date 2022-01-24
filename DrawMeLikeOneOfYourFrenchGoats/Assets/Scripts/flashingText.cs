using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class flashingText : MonoBehaviour
{
    public GameObject[] texts;
    public int current;

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
        else if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
        {
            StartCoroutine(waiter());
            
        }
    }
    IEnumerator waiter()
    {
        string scene = "";
        if (current == 0) {
            //play game
            scene = "Tuto";
        }else if (current == 2) {
            //load credits
            scene = "credits";
        }
        texts[current].SetActive(true);  
        yield return new WaitForSecondsRealtime(0.1f);
        texts[current].SetActive(false);
        yield return new WaitForSecondsRealtime(0.1f);
        texts[current].SetActive(true);
        yield return new WaitForSecondsRealtime(0.1f);
        texts[current].SetActive(false);
        yield return new WaitForSecondsRealtime(0.2f);

        if (current == 3)
            Application.Quit();
        else if(current != 1){
            SceneManager.LoadScene(scene);
        }

    }




}
