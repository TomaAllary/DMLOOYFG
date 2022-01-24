using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pancarte : MonoBehaviour
{
    public GameObject panneau;
    public AudioSource mainAudio;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        panneau.gameObject.SetActive(true);
        mainAudio.volume = .05f;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        panneau.gameObject.SetActive(false);
        mainAudio.volume = .25f;
    }
}
