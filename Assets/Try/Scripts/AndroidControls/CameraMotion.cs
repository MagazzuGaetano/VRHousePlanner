using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraMotion : MonoBehaviour
{
    public float speed;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved && !CollisionDetector2D.is2DColliding)
        {
            if (SelectFurniture.currentFurniture && SelectFurniture.currentFurniture.isDragging)
                //&& Input.GetTouch(0).phase == TouchPhase.Moved)
                return;
            else
                transform.position = new Vector3(transform.position.x - Input.GetAxis("Mouse X") * speed * Time.deltaTime, transform.position.y, transform.position.z - Input.GetAxis("Mouse Y") * speed * Time.deltaTime);
        }
       
       /* if ( Input.GetMouseButton(0) && !CollisionDetector2D.is2DColliding) 
        {
            if (SelectFurniture.currentFurniture && SelectFurniture.currentFurniture.isDragging)
                //&& Input.GetTouch(0).phase == TouchPhase.Moved)
                return;
            else
                transform.position = new Vector3(transform.position.x - Input.GetAxis("Mouse X") * speed * Time.deltaTime, transform.position.y, transform.position.z - Input.GetAxis("Mouse Y") * speed * Time.deltaTime);
        }*/

    }

}

