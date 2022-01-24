using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingDeadAttack : MonoBehaviour
{
    [SerializeField] HealthBar rageBar;
    [SerializeField] BoxCollider2D attackCollider;
    private float timer = 0;
    private bool isAttacking = false;

    public int facingRight = -1;

    public void StartAttack() {
        attackCollider.enabled = true;
    }

    public void StopAttack() {
        attackCollider.enabled = false;
    }
    public void FixedUpdate()
    {
        if (timer > 0)
        {
            timer -= Time.fixedDeltaTime;
        }
        else if (isAttacking)
        {
            rageBar.addHealth(.4f);
            isAttacking = false;
        }
    }


    private void OnCollisionEnter2D(Collision2D collision) {

        if (collision.gameObject.tag == "Player") {
            Vector3 dir = ((Vector3.up * 5.7f) + (Vector3.right * facingRight * 4.0f)).normalized;
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(collision.gameObject.GetComponent<Rigidbody2D>().velocity.x, 0);
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Constants.walkindDeadPushForce * dir);

            //reset player jump
            collision.gameObject.GetComponent<CharacterController2D>().ResetJump();

            //hurt
            collision.gameObject.GetComponent<PlayerMovement>().PlayHurtSound();

            rageBar = collision.gameObject.GetComponent<PlayerMovement>().rageBar;
            isAttacking = true;
            timer = .1f;
            
        }
    }

}
