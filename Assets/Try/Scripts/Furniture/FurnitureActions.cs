using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FurnitureActions : MonoBehaviour
{
    public GameObject ActionsMenu;
    public static GameObject _ActionsMenu;
    public Button color, scale, destroy, change, exit;

    public Button slider;

    public GameObject _Rotate;
    public GameObject _UpDown;

    private float sensibility = 0.2f;

    private void Start()
    {
        _ActionsMenu = ActionsMenu;

        slider.onClick.AddListener(() =>
        {
            //gestire la mostra del Button UpDown 
            if (SelectFurniture.currentFurniture)
            {
                if (SelectFurniture.currentFurniture is HouseRoom || SelectFurniture.currentFurniture is Construction)
                {
                    _UpDown.SetActive(false);
                    _Rotate.SetActive(false);
                }
                else
                {
                    _UpDown.SetActive((PlanGraphics.currentMode == PlanGraphics.Modes._2D) ? false : true);
                }
            }
        });

        destroy.onClick.AddListener(() =>
        {
            HideFurniture();
            Destroy(SelectFurniture.currentFurniture.gameObject);
            SelectFurniture.currentFurniture = null;
        });

        color.onClick.AddListener(() =>
        {
            //se l'oggetto selezionato è di tipo HouseRoom o Construcion
            if (SelectFurniture.currentFurniture is HouseRoom || SelectFurniture.currentFurniture is Construction)
            {
                //l'update dei colori viene eseguito nella init
                PatternsManager.Init();
                PatternsManager._menu.SetActive(true);
                PatternsManager.currentPanel.SetActive(true);
                ActionsMenu.SetActive(false);
            }
        });

        scale.onClick.AddListener(() => { });

        //attivo il FurnitureMenu se un muro è selezionato
        change.onClick.AddListener(() =>
        {
            if (SelectFurniture.currentFurniture is Construction) {
                FurnitureMenu._FurnitureMenu.SetActive(true);
                FurnitureMenu._Constructions_panel.SetActive(true);
                FurnitureMenu._Interiors_panel.SetActive(false);
                FurnitureMenu._Rooms_panel.SetActive(false);
            }
        });

        exit.onClick.AddListener(() =>
        {
            SelectFurniture._DeselectFurniture();
            HideFurniture();
        });
    }

    private void Update()
    {
        //gestire la mostra del Button UpDown 
        if (SelectFurniture.currentFurniture)
        {
            if (SelectFurniture.currentFurniture is HouseRoom || SelectFurniture.currentFurniture is Construction)
            {
                _UpDown.SetActive(false);
                _Rotate.SetActive(false);
            }
            else
            {
                _UpDown.SetActive((PlanGraphics.currentMode == PlanGraphics.Modes._2D) ? false : true);
            }
        }

        if (SelectFurniture.currentFurniture != null)
        {
            Vector3 v = Camera.main.WorldToScreenPoint(SelectFurniture.currentFurniture.transform.position);
            _Rotate.transform.GetComponent<RectTransform>().position = new Vector2(v.x + 400f, v.y);
            _UpDown.transform.GetComponent<RectTransform>().position = new Vector2(v.x - 400f, v.y);
        }

    }

    //il metodo Rotate sarà richiamato nell'event Trigger Drag del Button Rotate
    public void Rotate() { SelectFurniture.currentFurniture.Rotate(sensibility); }
    //il metodo UpDown sarà richiamato nell'event Trigger Drag del Button UpDown
    public void Updown() { SelectFurniture.currentFurniture.UpDown(); }
    //il metodo EndRotate sarà richiamato nell'event Trigger DragEnd dei relativi Buttons(Rotate,UpDown)
    public void EndDragging() { SelectFurniture.currentFurniture.EndDragging(); }

    public void HideFurniture()
    {
        ActionsMenu.SetActive(false);
        _UpDown.SetActive(false);
        _Rotate.SetActive(false);
    }


}
