using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killable : MonoBehaviour
{
    public Animator animator;
    public bool isDying;
    public float dyingTimer;
    // Start is called before the first frame update
    void Start()
    {
        isDying = false;
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isDying)
        {
            if (dyingTimer > 0)
                dyingTimer -= Time.fixedDeltaTime;
            else
                Destroy(gameObject);
        }
    }

    public void dieInMotion()
    {
        isDying = true;
        dyingTimer = 1f;
        animator.SetBool("isDying", true);
    }
}
