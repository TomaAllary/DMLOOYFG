using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hunter : MonoBehaviour
{
    public GameObject Bullet;
    public Transform barrelEnd;
    public AudioSource audio;

    private Animator animator;

    private float timer = 0;
    private float maxTimer = 4;
    private float minTimer = 2;

    private float FireTimer = 2;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(timer < 0) {
            int randomAnim = Random.Range(1, 3);

            animator.SetInteger("idle_state", randomAnim);
            animator.SetTrigger("play_anim");

            timer = Random.Range(minTimer, maxTimer);
        }
        else {
            timer -= Time.deltaTime;
        }

        if(FireTimer < 0) {
            Fire();
            FireTimer = 2;
        }
        else {
            FireTimer -= Time.deltaTime;
        }

        
    }



    private void Fire() {

        animator.SetTrigger("fire");

        StartCoroutine("fireBullet");
    }

    IEnumerator fireBullet() {
        audio.PlayDelayed(0.2f);

        yield return new WaitForSeconds(0.35f);

        GameObject bullet = Instantiate(Bullet);

        bullet.transform.position = barrelEnd.position;
        bullet.transform.rotation = gameObject.transform.rotation;
        //bullet.GetComponent<ParticleSystem>().startRotation = -transform.rotation.eulerAngles.z / (180.0f / Mathf.PI);
        //bullet.GetComponent<ParticleSystem>().transform.rotation = gameObject.transform.rotation;
        bullet.GetComponent<bullet>().setDirection(barrelEnd.position.x < transform.position.x);
    }
}
