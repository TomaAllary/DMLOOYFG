using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public Camera cam;
    public GameObject goatSurroundings;

    public float runspeed = 40f;
    float horizontalMove = 0f;
    bool isJumping = false;
    public bool isRamming;

    private Animator myAnimator;
    public ParticleSystem goatSparkle;
    public Animator deathAnim;
    public AudioSource hurtAudio;
    public Rigidbody2D[] hunters;


    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        isRamming = false;
        goatSparkle.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runspeed;
        if (Input.GetButtonDown("Jump"))
        {
            isJumping = true;
        }
        if (horizontalMove != 0)
        {
            myAnimator.SetBool("isMoving", true);
        }
        else
        {
            myAnimator.SetBool("isMoving", false);
        }

        if (Input.GetMouseButtonDown(0)) {
            //play animation ramming. *bool is set through anim events*
            myAnimator.SetTrigger("slam");
        }

    }

    private void FixedUpdate()
    {

        cam.transform.position = new Vector3(transform.position.x, transform.position.y, cam.transform.position.z);

        goatSurroundings.transform.position = transform.position;

        controller.Move(horizontalMove * Time.fixedDeltaTime, false, isJumping);
        isJumping = false;

        if(transform.position.y < -6) {
            deathAnim.SetTrigger("death");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "weapon")
        {
            //rageBar.GetComponent<HealthBar>().addHealth(.2f);
        }

    }


    public void setRammingFalse() {
        isRamming = false;

        foreach (Rigidbody2D rb in hunters) {
            rb.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionX;
        }
    }
    public void setRammingTrue() {
        isRamming = true;
        foreach (Rigidbody2D rb in hunters) {
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }

    public void PlayHurtSound() {
        hurtAudio.Play();
    }


}
