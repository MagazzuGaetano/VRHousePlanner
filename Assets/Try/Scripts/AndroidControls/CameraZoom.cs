using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{

    public float perspectiveZoomSpeed = 0.5f;
    public float orthoZoomSpeed = 0.5f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetAxis("Mouse ScrollWheel") > 0 && (!SelectFurniture.currentFurniture || (SelectFurniture.currentFurniture && (!SelectFurniture.currentFurniture.isDragging))))
        {
            GetComponent<Camera>().fieldOfView--;
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0 && (!SelectFurniture.currentFurniture || (SelectFurniture.currentFurniture && (!SelectFurniture.currentFurniture.isDragging))))
        {
            GetComponent<Camera>().fieldOfView++;
        }

        //se touchCount == 2 e l'oggetto è selezionato è sta draggando o se l'oggetto nonè selezionato
        if (Input.touchCount == 2 && (!SelectFurniture.currentFurniture || (SelectFurniture.currentFurniture && (!SelectFurniture.currentFurniture.isDragging))))
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);
            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;
            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;
            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            if (GetComponent<Camera>().orthographic)
            {

                if (GetComponent<Camera>().orthographicSize >= 20f) GetComponent<Camera>().orthographicSize = 19.9f;
                if (GetComponent<Camera>().orthographicSize <= 2f) GetComponent<Camera>().orthographicSize = 2.1f;

                GetComponent<Camera>().orthographicSize += deltaMagnitudeDiff * orthoZoomSpeed;
                GetComponent<Camera>().orthographicSize = Mathf.Max(GetComponent<Camera>().orthographicSize, .1f);
            }
            else
            {
                if (GetComponent<Camera>().fieldOfView >= 80f) GetComponent<Camera>().fieldOfView = 79.9f;
                if (GetComponent<Camera>().fieldOfView <= 2f) GetComponent<Camera>().fieldOfView = 2.1f;

                GetComponent<Camera>().fieldOfView += deltaMagnitudeDiff * perspectiveZoomSpeed;
                GetComponent<Camera>().fieldOfView = Mathf.Clamp(GetComponent<Camera>().fieldOfView, .1f, 179.9f);
            }
        }
    }
}
