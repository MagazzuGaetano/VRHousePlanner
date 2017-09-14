using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Furniture : MonoBehaviour
{

    public string id;
    public new string name;
    public GameObject GO;
    public GameObject border;
    public Sprite icon;

    private Vector3 _mouseReference;
    private Vector3 _mouseOffset;
    private Vector3 _rotation = Vector3.zero;
    private Vector3 dist;
    private float PosX;
    private float PosY;

    //variabile booleana per saper se l'oggetto presenta un evento Drag su di esso attivo o meno
    public bool isDragging = false;

    //il metodo EndRotate sarà richiamato nell'event Trigger DragEnd dei relativi Buttons(Rotate,UpDown)
    public void EndDragging() { isDragging = false; }

    public void OnMouseDown()
    {
        dist = Camera.main.WorldToScreenPoint(transform.position);
        PosX = Input.mousePosition.x - dist.x;
        PosY = Input.mousePosition.y - dist.y;
    }

    public void OnMouseDrag()
    {
        if (PlanGraphics.currentMode == PlanGraphics.Modes.VR || SelectFurniture.currentFurniture != this)
            return;
        isDragging = true;
        Move();
    }

    public void OnMouseExit()
    {
        isDragging = false;
    }

    public void Move()
    {
        //se viene effettuato più di un tocco contemporaneamente non muovo l'oggetto
        if (Input.touchCount > 1) return;

        ////////////////////Attenzione!!!!!!!!!!!!!!/////////////////////////
        //da cambiare
        //se l'oggetto ovvero l'istanza di questa classe e di tipo Construction
        if (this is Construction) return;

        //se sta avvenendo una collisione 2D non drago l'oggetto
        if (CollisionDetector2D.is2DColliding) return;

        Vector3 curPos = new Vector3(Input.mousePosition.x - PosX, Input.mousePosition.y - PosY, dist.z);
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(curPos);
        transform.position = new Vector3(worldPos.x, transform.position.y, worldPos.z);
    }

    //il metodo Rotate sarà richiamato nell'event Trigger Drag del Button Rotate
    public void Rotate(float sensibility)
    {
        //se sta avvenendo una collisione 2D non drago l'oggetto
        if (CollisionDetector2D.is2DColliding) return;

        isDragging = true;
        _mouseOffset = (Input.mousePosition - _mouseReference);
        _rotation.y = -(_mouseOffset.x + _mouseOffset.y) * sensibility;
        this.transform.Rotate(_rotation);
        _mouseReference = Input.mousePosition;
    }

    //il metodo UpDown sarà richiamato nell'event Trigger Drag del Button UpDown
    public void UpDown()
    {
        //se sta avvenendo una collisione 2D non drago l'oggetto
        if (CollisionDetector2D.is2DColliding) return;

        isDragging = true;
        Vector3 curPos = new Vector3(Input.mousePosition.x - PosX, Input.mousePosition.y - PosY, dist.z);
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(curPos);
        this.transform.position = new Vector3(this.transform.position.x, worldPos.y, this.transform.position.z);
    }

    public void init()
    {
        dist = Camera.main.WorldToScreenPoint(transform.position);
        PosX = Input.mousePosition.x - dist.x;
        PosY = Input.mousePosition.y - dist.y;
    }
}
