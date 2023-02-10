using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManDeath : MonoBehaviour
{
    // class for playing human's death
    public GameObject parent;
    public GameObject red;

    private bool isDead = false;
    private Animation anim;

    private void Start()
    {
        // get animation component and hide red human
        anim = red.GetComponent<Animation>();
        red.SetActive(false);
    }

    private void Update()
    {
        // if dead and animation stoped playing destroy parent object
        if (isDead)
        {
            if (!anim.isPlaying)
            {
                Destroy(parent);
            }
        }
    }

    public void Death()
    {
        // play death animation on red human and destroy blue human
        red.SetActive(true);
        RedPlay();
        Destroy(gameObject);
    }

    public void RedPlay()
    {
        // play animation
        anim.Play();
        isDead = true;
    }
}
