using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public GameObject bulletPop;

    private float bulletSpeed = Constants.bulletSpeed;

    private float lifetime = Constants.bulletLifetime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + new Vector3(bulletSpeed * Time.deltaTime, 0, 0);

        lifetime -= Time.deltaTime;

        if (lifetime < 0)
            Destroy(gameObject);
    }

    public void setDirection(bool left) {
        if (left)
            bulletSpeed *= -1;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.tag == "Player") {
            Vector3 dir;

            float yDiff = collision.gameObject.transform.position.y - transform.position.y;

            if (yDiff > 0.5)
                dir = Vector3.up;
            else
                dir = ((Vector3.up * 1.7f * Mathf.Abs(bulletSpeed)) + (Vector3.right * bulletSpeed)).normalized;

            //reset player jump
            collision.gameObject.GetComponent<CharacterController2D>().ResetJump();

            //hurt
            collision.gameObject.GetComponent<PlayerMovement>().PlayHurtSound();

            collision.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(collision.gameObject.GetComponent<Rigidbody2D>().velocity.x, 0);
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Constants.bulletPushForce * dir);

            StartCoroutine(PopBullet());
        }
        else if(collision.gameObject.layer == 7)
        {
            StartCoroutine(PopBullet());
        }
    }

    private IEnumerator PopBullet() {
        Destroy(transform.GetChild(0).gameObject);
        Destroy(GetComponent<SpriteRenderer>());
        Destroy(GetComponent<Rigidbody2D>());
        Destroy(GetComponent<BoxCollider2D>());

        Transform popEffect = Instantiate(bulletPop).transform;

        //direction of pop
        if (bulletSpeed < 0) {
            Vector3 current = popEffect.eulerAngles;
            current.y += 180;
            popEffect.eulerAngles = current;
        }
        popEffect.position = transform.position;

        yield return new WaitForSeconds(3);

        Destroy(popEffect.gameObject);

        Destroy(this.gameObject);

    }
}
