using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableWall : MonoBehaviour
{

    public Animator upperWall;
    public Animator lowerWall;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate() {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.5f, LayerMask.GetMask("Player"));
        for (int i = 0; i < colliders.Length; i++) {
            //if (colliders[i].gameObject.GetComponent<PlayerMovement>().inCriss && colliders[i].gameObject.GetComponent<PlayerMovement>().isRamming)
            if (colliders[i].gameObject.GetComponent<PlayerMovement>().inCriss){
                //Destroy wall
                upperWall.SetTrigger("destroy");
                lowerWall.SetTrigger("destroy");
                GetComponent<BoxCollider2D>().enabled = false;
            }
        }
    }

    /*private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.tag == "Player") {
            //check if in rage mode and if hitting
            if (collision.gameObject.GetComponent<PlayerMovement>().inCriss && collision.gameObject.GetComponent<PlayerMovement>().isRamming)
            {
                //Destroy wall
                upperWall.SetTrigger("destroy");
                lowerWall.SetTrigger("destroy");
                GetComponent<BoxCollider2D>().enabled = false;
            }

        }
    }*/

    IEnumerator destroyItself() {
        yield return new WaitForSeconds(2);

        Destroy(gameObject);
    }
}
