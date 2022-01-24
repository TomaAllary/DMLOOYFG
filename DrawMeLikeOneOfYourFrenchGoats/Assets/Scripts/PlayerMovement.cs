using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public Camera cam;
    public GameObject goatSurroundings;
    public Animator deathAnim;

    public AudioSource hurtAudio;

    public AnimatorOverrideController inCrissController;
    public AnimatorOverrideController happyController;

    public Rigidbody2D[] hunters;

    public float runspeed = 40f;
    public float rageMeter = 0f;
    public ParticleSystem goatSparkle;
    float horizontalMove = 0f;
    bool isJumping = false;
    private Animator myAnimator;
    public HealthBar rageBar;
    public bool inCriss;
    public bool isRamming;

    private float rageTimer;


    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        inCriss = isRamming = false;
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

        if(rageBar.getRageLevel() >= 1)
        {
            if (!inCriss) //to call it less often
            {
                myAnimator.runtimeAnimatorController = inCrissController;
                rageTimer = 1;
                inCriss = true;
                goatSparkle.gameObject.SetActive(true);
                controller.m_JumpForce = Constants.goatRageJumpForce;
            }
            
        }
        else if(rageBar.getRageLevel() <= 0)
        {
            if(inCriss) //to call it less often
                myAnimator.runtimeAnimatorController = happyController;
            inCriss = false;

            goatSparkle.gameObject.SetActive(false);
            controller.m_JumpForce = Constants.goatNormalJumpForce;
        }
        if(gameObject.GetComponent<CharacterController2D>().looseRage && !inCriss)
        {
            rageBar.setHealth(0);
        }
    }

    private void FixedUpdate()
    {

        cam.transform.position = new Vector3(transform.position.x, transform.position.y, cam.transform.position.z);

        goatSurroundings.transform.position = transform.position;

        controller.Move(horizontalMove * Time.fixedDeltaTime, false, isJumping);
        isJumping = false;

        if (inCriss)
        {
            if (rageTimer > 0)
            {
                rageTimer -= Time.fixedDeltaTime;
            }
            else
            {
                rageTimer = 1;
                rageBar.addHealth(-Constants.rageLostPerSecond);
            }
        }

        if(transform.position.y < -6) {
            deathAnim.SetTrigger("death");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "weapon")
        {
            rageBar.GetComponent<HealthBar>().addHealth(.2f);
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
