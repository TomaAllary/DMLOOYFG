using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ldg : MonoBehaviour
{
    public AudioSource MainAudio;
    public AudioClip victorySound;
    float timer;
    bool victory =  false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if(victory)
        {
            if (timer > 0)
                timer -= Time.deltaTime;
            else
                SceneManager.LoadScene("win");
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !victory)
        {
            MainAudio.volume = 0;
            gameObject.GetComponent<AudioSource>().PlayOneShot(victorySound);
            victory = true;
            timer = 9;
        }           
    }
    /*private void OnCollisionEnter(Collision collision)
    {
        SceneManager.LoadScene("win");
    }*/
}
