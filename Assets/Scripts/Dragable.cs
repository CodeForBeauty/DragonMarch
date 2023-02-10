using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Dragable : MonoBehaviour
{
    public GameObject HUD;
    private bool isUI = false;
    /*
     * Drag on touch
     */
    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
            {
                isUI = true;
            }
        }
        // get is touching
        if (Input.touchCount > 0 && !isUI)
        {
            // get position of 1 touch
            Vector3 touchPos = new(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, 25);
            // get position in world space
            var pos = Camera.main.ScreenToWorldPoint(touchPos);
            pos.z = transform.position.z;
            // set position
            transform.position = pos;
        }
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            isUI = false;
        }
    }
}