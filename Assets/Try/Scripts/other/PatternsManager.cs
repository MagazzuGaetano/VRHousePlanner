using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class _PatternsManager : MonoBehaviour
{
   /* public GameObject menu;
    public static GameObject _menu;
    public GameObject Floor, Roof, Inside1, Outside1, Inside2, Outside2;
    public GameObject House, Wall, Patterns;
    public Button Housebtnexit, Wallbtnexit, Patternsbtnexit, Patternsbtnturnback;

    public static GameObject currentPanel;
    public static GameObject lastPanel;
    public static GameObject _House, _Wall, _Patterns;

    public static Button currentbuttonpressed;

    public GameObject container;
    public Pattern prefab;
    public List<Texture2D> text2D = new List<Texture2D>();
    public List<Sprite> sprites = new List<Sprite>();

    // Use this for initialization
    void Start()
    {
        _menu = menu;
        _House = House;
        _Wall = Wall;
        _Patterns = Patterns;

        Events();
        GenerateUI();
    }

    // Update is called once per frame
    void Update()
    {
        if (SelectFurniture.currentFurniture && SelectFurniture.currentFurniture.type == Furniture.types.room)
            UpdateColor();

        if (currentbuttonpressed)
            for (int i = 0; i < container.transform.childCount; i++)
            {
                if (container.transform.GetChild(i).GetComponent<Pattern>().Type == Pattern.Types.floor && currentbuttonpressed.name != "Floor" && currentPanel == Patterns)
                {
                    container.transform.GetChild(i).gameObject.SetActive(false);
                }
                else { container.transform.GetChild(i).gameObject.SetActive(true); }
            }
    }

    void GenerateUI()
    {
        var i = 0;
        foreach (Texture2D t in text2D)
        {
            foreach (Sprite s in sprites)
            {
                if (t.name == s.name)
                {
                    GameObject go = Instantiate(prefab.gameObject, container.transform, false);
                    go.GetComponent<Pattern>().id = i;
                    go.GetComponent<Pattern>().name = t.name;

                    Pattern.Types type = Pattern.Types.house;

                    if (s.name.Contains("wall"))
                        type = Pattern.Types.house;
                    else if (s.name.Contains("floor"))
                        type = Pattern.Types.floor;
                    else if (s.name.Contains("roof"))
                        type = Pattern.Types.roof;

                    go.GetComponent<Pattern>().Type = type;
                    go.GetComponent<Pattern>().text2d = t;
                    go.GetComponent<Pattern>().icon = s;

                    go.transform.GetComponentInChildren<Text>().text = t.name;
                    go.transform.GetChild(1).GetComponent<Image>().sprite = s;

                    go.transform.GetChild(1).GetComponent<Image>().GetComponent<Button>().onClick.AddListener(() =>
                    {
                        SetColor(go.GetComponent<Pattern>().text2d);
                        ChangePanel();
                    });

                }
            }

            i++;
        }
    }

    void ChangePanel()
    {
        currentPanel.SetActive(false);

        GameObject newpanel = null;

        switch (currentPanel.name)
        {
            case "House":
                newpanel = Patterns;
                break;
            case "Wall":
                newpanel = Patterns;
                break;
            case "Patterns":
                newpanel = lastPanel;
                break;
            default:
                break;
        }

        lastPanel = currentPanel;
        currentPanel = newpanel;
        currentPanel.SetActive(true);
    }

    void Events()
    {
        Floor.transform.FindChild("Floor").GetComponent<Button>().onClick.AddListener(() => { ChangePanel(); currentbuttonpressed = Floor.transform.FindChild("Floor").GetComponent<Button>(); });
        Roof.transform.FindChild("Roof").GetComponent<Button>().onClick.AddListener(() => { ChangePanel(); currentbuttonpressed = Roof.transform.FindChild("Roof").GetComponent<Button>(); });
        Inside1.transform.FindChild("Inside1").GetComponent<Button>().onClick.AddListener(() => { ChangePanel(); currentbuttonpressed = Inside1.transform.FindChild("Inside1").GetComponent<Button>(); });
        Outside1.transform.FindChild("Outside1").GetComponent<Button>().onClick.AddListener(() => { ChangePanel(); currentbuttonpressed = Outside1.transform.FindChild("Outside1").GetComponent<Button>(); });

        Inside2.transform.FindChild("Inside2").GetComponent<Button>().onClick.AddListener(() => { ChangePanel(); currentbuttonpressed = Inside2.transform.FindChild("Inside2").GetComponent<Button>(); });
        Outside2.transform.FindChild("Outside2").GetComponent<Button>().onClick.AddListener(() => { ChangePanel(); currentbuttonpressed = Outside2.transform.FindChild("Outside2").GetComponent<Button>(); });

        Patternsbtnturnback.onClick.AddListener(() => { ChangePanel(); });

        UnityEngine.Events.UnityAction close_menu = () =>
        {
            lastPanel = null;
            currentPanel = null;

            menu.SetActive(false);
            _House.SetActive(false);
            _Wall.SetActive(false);
            _Patterns.SetActive(false);

            SelectFurniture.selectFurniture(SelectFurniture.currentFurniture, SelectFurniture.currentWall ? SelectFurniture.currentWall : null);

            currentbuttonpressed = null;
        };

        Patternsbtnexit.onClick.AddListener(close_menu);
        Housebtnexit.onClick.AddListener(close_menu);
        Wallbtnexit.onClick.AddListener(close_menu);
    }

    void UpdateColor()
    {
        if (!SelectFurniture.currentFurniture) return;

        foreach (Sprite s in sprites)
        {
            if (s.name == SelectFurniture.currentFurniture.transform.FindChild("floor").FindChild("floor-inside").GetComponent<Renderer>().material.mainTexture.name)
            {
                Floor.transform.GetChild(1).GetComponent<Image>().sprite = s;
            }
            if (s.name == SelectFurniture.currentFurniture.transform.FindChild("floor").FindChild("floor-outside").GetComponent<Renderer>().material.mainTexture.name)
            {
                Roof.transform.GetChild(1).GetComponent<Image>().sprite = s;
            }

            if (SelectFurniture.currentWall)
            {
                if (s.name == SelectFurniture.currentWall.transform.parent.FindChild("wall-inside").GetComponent<Renderer>().material.mainTexture.name)
                {
                    Inside2.transform.GetChild(1).GetComponent<Image>().sprite = s;
                }
                if (s.name == SelectFurniture.currentWall.transform.parent.FindChild("wall-outside").GetComponent<Renderer>().material.mainTexture.name)
                {
                    Outside2.transform.GetChild(1).GetComponent<Image>().sprite = s;
                }
            }

            if (SelectFurniture.currentFurniture.transform.childCount > 0)
            {
                for (var i = 0; i < SelectFurniture.currentFurniture.transform.childCount; i++)
                {
                    if (SelectFurniture.currentFurniture.transform.FindChild("wall-front").FindChild("wall-inside").GetComponent<Renderer>().material.mainTexture.name == s.name &&
                        SelectFurniture.currentFurniture.transform.FindChild("wall-back").FindChild("wall-inside").GetComponent<Renderer>().material.mainTexture.name == s.name &&
                        SelectFurniture.currentFurniture.transform.FindChild("wall-right").FindChild("wall-inside").GetComponent<Renderer>().material.mainTexture.name == s.name &&
                        SelectFurniture.currentFurniture.transform.FindChild("wall-left").FindChild("wall-inside").GetComponent<Renderer>().material.mainTexture.name == s.name)
                    {
                        Inside1.transform.GetChild(1).GetComponent<Image>().sprite = s;
                    }
                }

                for (var i = 0; i < SelectFurniture.currentFurniture.transform.childCount; i++)
                {
                    if (SelectFurniture.currentFurniture.transform.FindChild("wall-front").FindChild("wall-outside").GetComponent<Renderer>().material.mainTexture.name == s.name &&
                        SelectFurniture.currentFurniture.transform.FindChild("wall-back").FindChild("wall-outside").GetComponent<Renderer>().material.mainTexture.name == s.name &&
                        SelectFurniture.currentFurniture.transform.FindChild("wall-right").FindChild("wall-outside").GetComponent<Renderer>().material.mainTexture.name == s.name &&
                        SelectFurniture.currentFurniture.transform.FindChild("wall-left").FindChild("wall-outside").GetComponent<Renderer>().material.mainTexture.name == s.name)
                    {
                        Outside1.transform.GetChild(1).GetComponent<Image>().sprite = s;
                    }
                }
            }


        }
    }

    void SetColor(Texture2D newText2D)
    {
        if (!SelectFurniture.currentFurniture || !currentbuttonpressed) return;

        switch (currentbuttonpressed.name)
        {
            case "Floor":
                SelectFurniture.currentFurniture.transform.FindChild("floor").FindChild("floor-inside").GetComponent<Renderer>().material.mainTexture = newText2D;
                print("floor ciao new colo of pasagdfvasfemdsaafasfsafnsadtasdoo");
                break;
            case "Roof":
                SelectFurniture.currentFurniture.transform.FindChild("floor").FindChild("floor-outside").GetComponent<Renderer>().material.mainTexture = newText2D;
                break;
            case "Inside1":
                for (var i = 0; i < SelectFurniture.currentFurniture.transform.childCount; i++)
                {
                    SelectFurniture.currentFurniture.transform.FindChild("wall-front").FindChild("wall-inside").GetComponent<Renderer>().material.mainTexture = newText2D;
                    SelectFurniture.currentFurniture.transform.FindChild("wall-back").FindChild("wall-inside").GetComponent<Renderer>().material.mainTexture = newText2D;
                    SelectFurniture.currentFurniture.transform.FindChild("wall-right").FindChild("wall-inside").GetComponent<Renderer>().material.mainTexture = newText2D;
                    SelectFurniture.currentFurniture.transform.FindChild("wall-left").FindChild("wall-inside").GetComponent<Renderer>().material.mainTexture = newText2D;
                }
                break;
            case "Outside1":
                for (var i = 0; i < SelectFurniture.currentFurniture.transform.childCount; i++)
                {
                    SelectFurniture.currentFurniture.transform.FindChild("wall-front").FindChild("wall-outside").GetComponent<Renderer>().material.mainTexture = newText2D;
                    SelectFurniture.currentFurniture.transform.FindChild("wall-back").FindChild("wall-outside").GetComponent<Renderer>().material.mainTexture = newText2D;
                    SelectFurniture.currentFurniture.transform.FindChild("wall-right").FindChild("wall-outside").GetComponent<Renderer>().material.mainTexture = newText2D;
                    SelectFurniture.currentFurniture.transform.FindChild("wall-left").FindChild("wall-outside").GetComponent<Renderer>().material.mainTexture = newText2D;
                }
                break;
            case "Inside2":
                if (!SelectFurniture.currentWall) return;
                SelectFurniture.currentWall.transform.parent.FindChild("wall-inside").GetComponent<Renderer>().material.mainTexture = newText2D;
                break;
            case "Outside2":
                if (!SelectFurniture.currentWall) return;
                SelectFurniture.currentWall.transform.parent.FindChild("wall-outside").GetComponent<Renderer>().material.mainTexture = newText2D;
                break;
            default: break;
        }
    }*/
}

