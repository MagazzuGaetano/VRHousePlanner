using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AddFurniture : MonoBehaviour
{
   /* public GameObject plane;
    public GameObject house;
    public GameObject roomPrefab;
    public GameObject UpDown_btn;

    public Text currentPanelname;

    public Button btn_constructions, btn_rooms, btn_interior;
    private List<Button> btns
    {
        get
        {
            return new List<Button>() {
                btn_rooms,
                btn_interior,
                btn_constructions
            };
        }
    }

    public object EditBehavior { get; private set; }

    public GameObject constructionsPanel, roomsPanel, interiorsPanel;

    public List<GameObject> items;
    public GameObject itemPrefab;

    void Start()
    {
        GenerateUI();
        Active();
    }

    void GenerateUI()
    {
        foreach (GameObject item in items)
        {
            Transform t = null;

            Furniture i = item.GetComponent<Furniture>();
            switch (i.type)
            {
                case Furniture.types.construction:
                    t = constructionsPanel.transform;
                    break;
                case Furniture.types.interior:
                    t = interiorsPanel.transform;
                    break;
                case Furniture.types.room:
                    t = roomsPanel.transform;
                    break;
            }

            if (t == null) return;

            GameObject go = Instantiate(itemPrefab, t, false);
            go.GetComponentInChildren<Text>().text = i.name;
            go.transform.GetChild(0).GetComponent<Image>().sprite = i.icon;

            go.GetComponent<Button>().onClick.AddListener(() =>
            {
                if (SelectFurniture.currentFurniture != null) return;

                GameObject obj = Instantiate(item, house.transform);

                Furniture f = obj.GetComponent<Furniture>();
                Vector3 pos = Vector3.zero;

                if (obj.GetComponent<Furniture>().type == Furniture.types.interior)
                {
                    pos = new Vector3(roomPrefab.transform.position.x, roomPrefab.transform.position.y + 0.5f, roomPrefab.transform.position.z);
                }
                else if (obj.GetComponent<Furniture>().type == Furniture.types.room)
                {

                    pos = roomPrefab.transform.position;
                }
                else if (obj.GetComponent<Furniture>().type == Furniture.types.construction)
                {

                }

                SelectFurniture.lastFurniture = SelectFurniture.currentFurniture;
                SelectFurniture.furnitures.Add(f);
                SelectFurniture.currentFurniture = f;

                obj.transform.position = pos;
                obj.transform.rotation = plane.transform.rotation;

                SelectFurniture.selectFurniture(obj.GetComponent<Furniture>());

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
                    case "Constructions":
                        constructionsPanel.gameObject.SetActive(true);
                        roomsPanel.gameObject.SetActive(false);
                        interiorsPanel.gameObject.SetActive(false);
                        break;
                    case "Rooms":
                        constructionsPanel.gameObject.SetActive(false);
                        roomsPanel.gameObject.SetActive(true);
                        interiorsPanel.gameObject.SetActive(false);
                        break;
                    case "Interior":
                        constructionsPanel.gameObject.SetActive(false);
                        roomsPanel.gameObject.SetActive(false);
                        interiorsPanel.gameObject.SetActive(true);
                        break;
                    default:
                        break;
                }

                currentPanelname.text = btn.name;
            });
        }
    }*/
}
