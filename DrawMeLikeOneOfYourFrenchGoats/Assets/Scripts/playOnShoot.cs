using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playOnShoot : MonoBehaviour
{
    public AudioSource pow;
    public AudioClip clip;
    private float steptick;
    // Start is called before the first frame update
    void Start()
    {
        pow.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if (steptick <= 0)
        {
            pow.PlayOneShot(clip);
            steptick = .1f;
        }

        if (steptick > 0)
            steptick -= Time.deltaTime;
    }
}
