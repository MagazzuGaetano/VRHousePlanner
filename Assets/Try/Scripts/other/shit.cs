using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shit : MonoBehaviour
{

    public string id;
    public new string name;
    public enum types
    {
        interior,
        construction,
        room
    };

    public types type;
    public GameObject go;
    public GameObject border;
    public Sprite icon;

    public void MoveForward(GameObject pointer, float speed) { this.transform.position += pointer.transform.forward * speed; }
    public void MoveBack(GameObject pointer, float speed) { this.transform.position -= pointer.transform.forward * speed; }
    public void MoveLeft(GameObject pointer, float speed) { this.transform.position -= pointer.transform.right * speed; }
    public void MoveRight(GameObject pointer, float speed) { this.transform.position += pointer.transform.right * speed; }

    public void MoveUp(GameObject pointer, float speed) { this.transform.position += pointer.transform.up * speed; }
    public void MoveDown(GameObject pointer, float speed) { this.transform.position -= pointer.transform.up * speed; }

    private Vector3 _mouseReference;
    private Vector3 _mouseOffset;
    private Vector3 _rotation = Vector3.zero;

    public bool isRotating = false;
    public bool isDragging = false;

    public void Rotate(float sensibility)
    {
        isRotating = true;

        _mouseOffset = (Input.mousePosition - _mouseReference);
        _rotation.y = -(_mouseOffset.x + _mouseOffset.y) * sensibility;
        this.transform.Rotate(_rotation);
        _mouseReference = Input.mousePosition;
    }

    public void EndRotate()
    {
        isRotating = false;
    }

    Vector3 dist;
    float posX;
    float posY;

    public void OnMouseDown()
    {
        dist = Camera.main.WorldToScreenPoint(transform.position);
        posX = Input.mousePosition.x - dist.x;
        posY = Input.mousePosition.y - dist.y;
    }

    public void Move()
    {
        /* Vector3 mousePosition = Vector3.zero;
         mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y,30f);
         Vector3 objectPosition = Camera.main.ScreenToWorldPoint(mousePosition);
         this.transform.position = new Vector3(objectPosition.x, this.transform.position.y, objectPosition.z);*/

        Vector3 curPos = new Vector3(Input.mousePosition.x - posX, Input.mousePosition.y - posY, dist.z);
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(curPos);
        this.transform.position = new Vector3(worldPos.x, this.transform.position.y, worldPos.z);
    }

    public void UpDown()
    {
        /*Vector3 mousePosition = Vector3.zero;
        mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 30f);
        Vector3 objectPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        this.transform.position = new Vector3(this.transform.position.x, objectPosition.y, this.transform.position.z);*/

        Vector3 curPos = new Vector3(Input.mousePosition.x - posX, Input.mousePosition.y - posY, dist.z);
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(curPos);
        this.transform.position = new Vector3(this.transform.position.x, worldPos.y, this.transform.position.z);

        isDragging = true;//messo qui perchè non posso metterlo nella OnMouseDrag perchè non draggo sull'oggetto ma sul updown button
    }

    public void EndUpDown() { isDragging = false; }

    public void OnMouseDrag()
    {
        if (PlanGraphics.currentMode == PlanGraphics.Modes.VR || SelectFurniture.currentFurniture != this) return;
        isDragging = true;
        Move();
    }

    public void OnMouseExit()
    {
        isDragging = false;
        isRotating = false;
        //maybe unset the current furniture
    }

}
