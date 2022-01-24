using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playOnWalk : MonoBehaviour
{
    public AudioSource walkywalky;
    public AudioClip clip;
    private float steptick;
    public CharacterController2D pm;
    // Start is called before the first frame update
    void Start()
    {
        walkywalky.gameObject.GetComponent<AudioSource>();
        clip = walkywalky.GetComponent<AudioClip>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void FixedUpdate()
    {
        if (steptick <= 0 && Input.GetAxisRaw("Horizontal") != 0 && pm.getGrounded() == true)
        {
            walkywalky.PlayOneShot(clip);
            steptick = .1f;
        }

        if (steptick > 0)
            steptick -= Time.deltaTime;
    }
}
