using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FurnitureMenu : MonoBehaviour
{
    public GameObject plane;
    public GameObject roomPrefab;
    public GameObject house;

    public GameObject Menu;
    public static GameObject _FurnitureMenu;

    public Button Add_btn;
    public Button Exit_btn;

    public Button Constructions_btn, Interiors_btn, Rooms_btn;
    private List<Button> btns
    {
        get
        {
            return new List<Button>() {
                Constructions_btn,
                Interiors_btn,
                Rooms_btn
            };
        }
    }

    public GameObject Constructions_panel, Interiors_panel, Rooms_panel;
    public static GameObject _Constructions_panel, _Interiors_panel, _Rooms_panel;

    public GameObject Constructions_container, Interios_container, Rooms_container;

    public List<Furniture> items;

    public GameObject itemPrefab;

    public static FurnitureMenu instance;

    // Use this for initialization
    void Start()
    {
        instance = this;

        _FurnitureMenu = Menu;
        _Constructions_panel = Constructions_panel;
        _Rooms_panel = Rooms_panel;
        _Interiors_panel = Interiors_panel;

        //apro il menu
        Add_btn.onClick.AddListener(() => { Menu.SetActive(true); });

        //chiudo il menu
        Exit_btn.onClick.AddListener(() =>
        {
            Menu.SetActive(false);
        });

        //la funzione Active gestisce il cambio di liste 
        Active();
        //la funzione GenerateUI genera la grafica dei vari item e l'istanzia 
        GenerateUI();
    }

    void GenerateUI()
    {
        foreach (Furniture item in items)
        {
            Transform t = null;
            Furniture newItem = null;

            //decido il container in cui andrà l'item da istanziare in base al tipo di oggetto
            if (item is Construction) { newItem = item.GetComponent<Construction>(); t = Constructions_container.transform; }
            else if (item is HouseRoom) { newItem = item.GetComponent<HouseRoom>(); t = Rooms_container.transform; }
            else { newItem = item.GetComponent<Furniture>(); t = Interios_container.transform; }

            GameObject go = Instantiate(itemPrefab, t, false);
            go.transform.FindChild("Text").GetComponent<Text>().text = newItem.name;
            go.transform.FindChild("Image").GetComponent<Image>().sprite = newItem.icon;

            go.GetComponent<Button>().onClick.AddListener(() =>
            {
                //se l'item corrente è di tipo construction
                if (item is Construction)
                {
                    //se c'è un 'oggetto selezionato di tipo construction cambia quel muro con questo se no evito d'istanziarlo
                    if (SelectFurniture.currentFurniture is Construction)
                    {
                        GameObject _go = Instantiate(item.gameObject, SelectFurniture.currentFurniture.transform.parent, false);

                        _go.transform.localRotation = SelectFurniture.currentFurniture.transform.localRotation;
                        _go.transform.localPosition = SelectFurniture.currentFurniture.transform.localPosition;

                        //prendo solo la parte prima dei (clone) e la assegno come nome
                        _go.name = _go.name.Split('(')[0];
                        //salvo in in una variabile l'oggetto che poi distruggerò
                        GameObject _lastWall = SelectFurniture.currentFurniture.gameObject;
                        //deseleziono l'oggetto corrente
                        SelectFurniture._DeselectFurniture();
                        //seleziono il nuovo oggetto
                        SelectFurniture._SelectFurniture(_go.GetComponent<Construction>());

                        ///////////////////////////////
                        //_go.transform.GetComponent<Construction>().name = _lastWall.transform.GetComponent<Construction>().name;

                        //la stanza corrente
                        HouseRoom hr = _go.transform.parent.GetComponent<HouseRoom>();

                        for (int i = 0; i < hr.walls.Length; i++)
                        {
                            //trovo nell'array la posizione del vecchio muro per metterci al suo posto il nuovo muro
                            if (hr.walls[i].gameObject.Equals(_lastWall))
                            {
                                hr.walls[i] = _go.transform.GetComponent<Construction>();

                                if (hr.walls[i].Direction == Construction.Directions.Left)
                                {
                                    hr.left = _go.transform.GetComponent<Construction>();
                                    //imposto la direction del muro che sto cambiando con la direction del muro vecchio
                                    _go.transform.GetComponent<Construction>().Direction = Construction.Directions.Left;
                                }
                                else if (hr.walls[i].Direction == Construction.Directions.Right)
                                {
                                    hr.right = _go.transform.GetComponent<Construction>();
                                    //imposto la direction del muro che sto cambiando con la direction del muro vecchio
                                    _go.transform.GetComponent<Construction>().Direction = Construction.Directions.Right;
                                }
                                else if (hr.walls[i].Direction == Construction.Directions.Front)
                                {
                                    hr.front = _go.transform.GetComponent<Construction>();
                                    //imposto la direction del muro che sto cambiando con la direction del muro vecchio
                                    _go.transform.GetComponent<Construction>().Direction = Construction.Directions.Front;
                                }
                                else if (hr.walls[i].Direction == Construction.Directions.Back)
                                {
                                    hr.back = _go.transform.GetComponent<Construction>();
                                    //imposto la direction del muro che sto cambiando con la direction del muro vecchio
                                    _go.transform.GetComponent<Construction>().Direction = Construction.Directions.Back;
                                }
                            }
                        }

                        //distruggo l'oggetto precedente
                        Destroy(_lastWall);
                        return;
                    }
                    else
                        return;
                }

                GameObject obj = Instantiate(item.gameObject, house.transform);

                Furniture f = null;
                Vector3 pos = Vector3.zero;

                if (newItem is HouseRoom)
                {
                    pos = roomPrefab.transform.position;
                    f = obj.GetComponent<HouseRoom>();
                }
                else if (newItem is Construction)
                {
                    f = obj.GetComponent<Construction>();
                }
                else
                {
                    pos = new Vector3(roomPrefab.transform.position.x, roomPrefab.transform.position.y + 0.5f, roomPrefab.transform.position.z);
                    f = obj.GetComponent<Furniture>();
                }

                obj.transform.position = pos;
                obj.transform.rotation = obj.transform.rotation;

                //seleziono l'oggetto corrente
                SelectFurniture._SelectFurniture(f);
            });
        }
    }

    void Active()
    {
        foreach (Button btn in btns)
        {
            btn.onClick.AddListener(() =>
            {
                switch (btn.name)
                {
                    case "Constructions_btn":
                        _Constructions_panel.gameObject.SetActive(true);
                        _Rooms_panel.gameObject.SetActive(false);
                        _Interiors_panel.gameObject.SetActive(false);
                        break;
                    case "Rooms_btn":
                        _Constructions_panel.gameObject.SetActive(false);
                        _Rooms_panel.gameObject.SetActive(true);
                        _Interiors_panel.gameObject.SetActive(false);
                        break;
                    case "Interiors_btn":
                        _Constructions_panel.gameObject.SetActive(false);
                        _Rooms_panel.gameObject.SetActive(false);
                        _Interiors_panel.gameObject.SetActive(true);
                        break;
                    default:
                        break;
                }
            });
        }
    }

}
