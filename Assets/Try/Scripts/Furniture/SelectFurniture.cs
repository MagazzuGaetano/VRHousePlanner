using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectFurniture : MonoBehaviour
{
    public GameObject RoomPrefab;
    public static Furniture currentFurniture;
    public static Furniture lastFurniture;
    public static List<Furniture> furnitures = new List<Furniture>();

    public GameObject ActionsMenu, Rotate, UpDown;
    public static GameObject _ActionsMenu , _Rotate, _UpDown;

    public GameObject Change, Paint, Delete;
    public static GameObject _Change,_Paint,_Delete;
    //una variabile booleana che indica se sono su un oggetto col mouse oppure no
    public static bool isHover = false;

    // Use this for initialization
    void Start()
    {
        _ActionsMenu = ActionsMenu;
        _Rotate = Rotate;
        _UpDown = UpDown;

        _Change = Change;
        _Paint = Paint;
        _Delete = Delete;

        furnitures.Add(RoomPrefab.GetComponent<Furniture>());
    }

    // Update is called once per frame
    void Update()
    {
        //controllo se ho cliccato (una volta) e se la fase di touch è quella di inizio
        //controllo di non essere in modalità VR
        //controllo se c'è un oggetto selezionato e se è draggato o se non c'è alcun oggetto selezionato
        //controllo se l'evento Hover dei buttons(UpDown,Rotate) è disattivo per poter selezionare un 'altro oggetto
        //controllo se la collisione 2D non sia in atto
        if (Input.GetMouseButtonDown(0) && PlanGraphics.currentMode != PlanGraphics.Modes.VR && (!SelectFurniture.currentFurniture || (SelectFurniture.currentFurniture && (!SelectFurniture.currentFurniture.isDragging))) && !isHover && !CollisionDetector2D.is2DColliding && Input.GetTouch(0).phase == TouchPhase.Began )
        {

            if (Input.touchCount > 1 && Input.GetTouch(0).phase == TouchPhase.Moved) return;

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                Furniture f = null;

                //se collido con un oggetto d'arredo l'oggetto selezionato sarà l'oggetto stesso
                if (hit.collider.tag == "furniture")
                {
                    f = hit.collider.GetComponent<Furniture>();
                    _SelectFurniture(f);
                }

                //se collido con il pavimento allora l'oggetto selezionato sarà la stanza intera
                if (hit.collider.tag == "floor")
                {
                    f = hit.collider.transform.parent.parent.GetComponent<HouseRoom>();
                    _SelectFurniture(f);
                }

                //se collido con un muro l'oggetto selezionato sarà l'oggetto stesso
                if (hit.collider.tag.Contains("wall"))
                {
                    //f = hit.collider.transform.parent.GetComponent<Construction>();
                    f = hit.collider.GetComponent<Construction>();
                    _SelectFurniture(f);
                }
                
            }
        }
    }

    //evidenzio l'oggetto selezionato
    public static void highlightFurniture(Furniture furniture, bool show = true)
    {
        GameObject[] border = new GameObject[10];

        if (furniture is Construction)
        {
            Construction c = furniture as Construction;
            border[0] = c.border;
        }
        else if (furniture is HouseRoom)
        {
            HouseRoom hr = furniture as HouseRoom;
            border = hr.GetBorder;
        }
        else
        {
            border[0] = furniture.border;
        }

        for (var i = 0; i < border.Length; i++)
        {
            if (border[i])
                border[i].SetActive((show) ? true : false);
        }

    }

    //imposto l'oggeto corrente e quello precedentemente selezionato
    public static void _SelectFurniture(Furniture f)
    {
        //se ho un oggetto già selezionato l'ho deseleziono e se l'oggeto selezionato è diverso da quello di prima
        if (SelectFurniture.currentFurniture != null && !SelectFurniture.currentFurniture.Equals(f))
        {
            //deseleziono l'oggetto
            _DeselectFurniture();
        }

        //se l'oggetto corrente e di tipo HouseRoom attivo il collider
        if (f is HouseRoom)
            f.GetComponent<BoxCollider>().enabled = true;

        lastFurniture = currentFurniture;
        highlightFurniture(f);
        currentFurniture = f;


        //attivo l'ActionsMenu
         _ActionsMenu.SetActive(true);

        //controllare quali Action devo attivare
        if (f is HouseRoom) {
            _Change.SetActive(false);
            _Delete.SetActive(true);
            _Paint.SetActive(true);
        }
        else if(f is Construction) {
            _Change.SetActive(true);
            _Delete.SetActive(false);
            _Paint.SetActive(true);
        }
        else
        {
            _Change.SetActive(false);
            _Delete.SetActive(true);
            _Paint.SetActive(false);
        }

        //attivo i Buttons(UpDown,Rotate)
        //se f tipeOff HouseRoom non mostro i Buttons(Updown,Rotate)
        _UpDown.SetActive((f is HouseRoom || f is Construction) ? false : true);
        _Rotate.SetActive((f is HouseRoom || f is Construction) ? false : true);
    }

    public static void _DeselectFurniture()
    {
        //controllo se l'oggetto selezionato è di tipo HouseRoom 
        ///////////////////Attenzione!!!////////////////
        //controllo da effettuare prima di cambiare il currentFurniture
        if (SelectFurniture.currentFurniture is HouseRoom)
            SelectFurniture.currentFurniture.GetComponent<BoxCollider>().enabled = false;

        SelectFurniture.lastFurniture = SelectFurniture.currentFurniture;
        SelectFurniture.highlightFurniture(SelectFurniture.currentFurniture, false);
        SelectFurniture.currentFurniture = null;

        //unsetto il menu dei pattern
        PatternsManager.Close();
    }

    //methodo che sarà richiamato nell'event trigger dell' evento OnMouseEnter dei Buttons(Rotate,UpDown)
    public void MyOnMouseOver()
    {
        //setto le posizioni da sottrare nel drag
        SelectFurniture.currentFurniture.init();
        isHover = true;
    }
    //methodo che sarà richiamato nell'event trigger dell' evento OnMouseExit dei Buttons(Rotate,UpDown)
    public void MyOnMouseExit()
    {
        isHover = false;
    }
}

static class My
{
    public static List<GameObject> getChildrenByTag(this GameObject g, string tag)
    {
        List<GameObject> children = new List<GameObject>();

        for (int i = 0; i < g.transform.childCount; i++)
        {
            if (g.transform.GetChild(i).tag == tag)
                children.Add(g.transform.GetChild(i).gameObject);
        }

        return children;
    }
}

