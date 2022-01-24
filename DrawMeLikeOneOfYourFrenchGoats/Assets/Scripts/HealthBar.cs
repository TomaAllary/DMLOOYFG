using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Transform bar;
    public Gradient gradient;
    public SpriteRenderer fill;
    private bool left = true;
    private float[] shakeTable = new float[] { .1f, .2f, .3f, .4f, .5f, .7f };
    public ParticleSystem particles;
    public GameObject crack;

    // Start is called before the first frame update
    void Start()
    {
        bar = transform.Find("Bar");
        bar.localScale = new Vector3(0, 1, 1);
        particles.Stop();
        fill.color = gradient.Evaluate(0f);
        crack.SetActive(false);
    }

    public void setHealth (float sizeNormalized)
    {
        bar.localScale = new Vector3(sizeNormalized, 1f);
        if (sizeNormalized == 0)
        {
            particles.Stop();
            crack.SetActive(false);
        }
            

    }

    public void FixedUpdate()
    {
        if(bar.localScale.x >= .5 && bar.localScale.x < 1.1)
        {

            if (left)
            {
                this.gameObject.transform.position += new Vector3(shakeTable[(int)(bar.localScale.x * 10) -5], 0, 0);
                left = false;
            }
            else
            {
                this.gameObject.transform.position += new Vector3(-shakeTable[(int)(bar.localScale.x * 10) - 5], 0, 0);
                left = true;
            }
            fill.color = gradient.Evaluate(bar.localScale.x);
        }                  
    }

    public void addHealth(float amountNormalized)
    {
        Vector3 modVector = new Vector3(amountNormalized, 0, 0);
        bar.localScale += modVector;
        if (bar.localScale.x >= 1)
        {
            particles.Play();
            Vector3 current = bar.localScale;
            current.x = 1;
            bar.localScale = current;
            crack.SetActive(true);
        }
        else if (bar.localScale.x < 0)
        {
            Vector3 current = bar.localScale;
            current.x = 0;
            bar.localScale = current;
            particles.Stop();
            crack.SetActive(false);
        }
        else
        {
            particles.Stop();
            crack.SetActive(false);
        }
    }

    public float getRageLevel()
    {
        return bar.localScale.x;
    }
}
