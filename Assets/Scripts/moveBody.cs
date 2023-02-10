using UnityEngine;
using TouchPhase = UnityEngine.TouchPhase;

public class moveBody : MonoBehaviour
{
    public MoveToPos moveToPos;
    public Rigidbody rb;
    public GameObject previous;

    private RaycastHit hit;
    private bool isTouch;

    void Start()
    {
        
    }

    
    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);

            if (Physics.Raycast(ray, out hit))
            {
                if(hit.transform.gameObject == gameObject) 
                {
                    isTouch = true;
                    moveToPos.isControlable = false;
                }
            }
        }
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            isTouch = false;
            moveToPos.isControlable = true;
        }
        if (isTouch)
        {
            Vector3 touchPos = new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, 23.5f);
            var touchPosWorld = Camera.main.ScreenToWorldPoint(touchPos);
            rb.MovePosition(touchPosWorld);

            var lookPos = previous.transform.position - transform.position;
            var rotation = Quaternion.LookRotation(lookPos, transform.up);
            rotation.x = 0;
            rotation.y = 0;
            rb.MoveRotation(rotation.normalized);
            //transform.rotation = rotation;
        }
    }
}
