using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoatAnimation : MonoBehaviour
{

    public Animator anim;

    public GameObject droppingTitle;

    public Animator EndingAnim;

    private bool canWalk = false;

    public float speed = 6;

    public AudioSource audioFall;
    public AudioSource goatAudio;
    public AudioClip audioClip;

    // Start is called before the first frame update
    void Start()
    {
        anim.SetBool("isWalking", true);

    }

    // Update is called once per frame
    void Update()
    {
        if(canWalk)
            GetComponent<Rigidbody2D>().MovePosition(transform.position + (Time.deltaTime * speed * Vector3.right));

        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            canWalk = true;
            goatAudio.PlayDelayed(0.5f);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            droppingTitle.SetActive(true);
            audioFall.PlayDelayed(0.27f);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3)) {
            EndingAnim.SetTrigger("play");

        }

        if (Input.GetKeyDown(KeyCode.Alpha4)) {
            SceneManager.LoadScene("Menu");

        }

    }

    private IEnumerator end() {
        yield return new WaitForSeconds(3);


        yield return new WaitForSeconds(4);

    }
}
