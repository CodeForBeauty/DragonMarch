using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelScroll : MonoBehaviour
{
    // Scroll level root with predefined speed
    public float speed;
    public int humansMultiplyer = 3;
    public int starDivider = 5;

    void Update()
    {
        // scroll level root down on speed
        transform.position = new Vector3(0, transform.position.y + -speed*Time.deltaTime, 0);
    }
}
