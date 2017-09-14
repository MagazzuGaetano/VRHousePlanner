using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PatternsManager : MonoBehaviour
{
    public GameObject house;
    public GameObject menu;
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

    public static List<Sprite> _sprites { get { return instance.sprites; } }
    public static GameObject _Floor, _Roof, _Inside1, _Outside1, _Inside2, _Outside2;

    public static PatternsManager instance;

    // Use this for initialization
    void Start()
    {
        instance = this;

        _menu = menu;
        _House = House;
        _Wall = Wall;
        _Patterns = Patterns;

        _Floor = Floor;
        _Roof = Roof;
        _Inside1 = Inside1;
        _Outside1 = Outside1;
        _Inside2 = Inside2;
        _Outside2 = Outside2;

        Events();
        GenerateUI();
        InitColor();
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

    public static void ChangePanel()
    {
        currentPanel.SetActive(false);

        GameObject newpanel = null;

        switch (currentPanel.name)
        {
            case "House":
                newpanel = _Patterns;
                break;
            case "Wall":
                newpanel = _Patterns;
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
            Close();
        };

        Patternsbtnexit.onClick.AddListener(close_menu);
        Housebtnexit.onClick.AddListener(close_menu);
        Wallbtnexit.onClick.AddListener(close_menu);
    }

    void SetColor(Texture2D newText2D)
    {
        //evito che ilcolorevenga settato quando non c'è alcun elemento selezionato e quando non è stato impostato un currentbuttonpressed
        if (!SelectFurniture.currentFurniture || !currentbuttonpressed) return;

        //in base al nome del button premuto setto la texture
        switch (currentbuttonpressed.name)
        {
            case "Floor":
                SelectFurniture.currentFurniture.transform.FindChild("Floor").GetComponent<Construction>().SetinsideColor(newText2D);
                break;
            case "Roof":
                SelectFurniture.currentFurniture.transform.FindChild("Roof").GetComponent<Renderer>().material.mainTexture = newText2D;
                break;
            case "Inside1":
                for (var i = 0; i < SelectFurniture.currentFurniture.transform.childCount; i++)
                {
                    if (SelectFurniture.currentFurniture.transform.GetChild(i).tag == "wall")
                    {
                        //se il muro corrente è Whole Door
                        if (SelectFurniture.currentFurniture.transform.GetChild(i).name == "Whole" || SelectFurniture.currentFurniture.transform.GetChild(i).name == "Door" || SelectFurniture.currentFurniture.transform.GetChild(i).name == "Window")
                        {
                            //prendo tutti i child dentro insides e li coloro
                            for (var k = 0; k < SelectFurniture.currentFurniture.transform.GetChild(i).FindChild("insides").transform.childCount; k++)
                            {
                                SelectFurniture.currentFurniture.transform.GetChild(i).FindChild("insides").transform.GetChild(k).GetComponent<Renderer>().material.mainTexture = newText2D;
                            }
                        }
                        else
                        {
                            SelectFurniture.currentFurniture.transform.GetChild(i).GetComponent<Construction>().SetinsideColor(newText2D);
                        }
                    }

                }
                break;
            case "Outside1":
                for (var i = 0; i < SelectFurniture.currentFurniture.transform.childCount; i++)
                {
                    if (SelectFurniture.currentFurniture.transform.GetChild(i).tag == "wall")
                    {
                        //se il muro corrente è Whole Door
                        if (SelectFurniture.currentFurniture.transform.GetChild(i).name == "Whole" || SelectFurniture.currentFurniture.transform.GetChild(i).name == "Door" || SelectFurniture.currentFurniture.transform.GetChild(i).name == "Window")
                        {
                            //prendo tutti i child con inside nel nome e li coloro
                            for (var k = 0; k < SelectFurniture.currentFurniture.transform.GetChild(i).FindChild("outsides").transform.childCount; k++)
                            {
                                SelectFurniture.currentFurniture.transform.GetChild(i).FindChild("outsides").transform.GetChild(k).GetComponent<Renderer>().material.mainTexture = newText2D;
                            }
                        }
                        else
                        {
                            SelectFurniture.currentFurniture.transform.GetChild(i).GetComponent<Construction>().SetoutsideColor(newText2D);
                        }
                    }

                    //setto il colore del terreno del colore applicato a tutti i muri esterni
                    SelectFurniture.currentFurniture.transform.GetComponent<HouseRoom>().floor.SetoutsideColor(newText2D);
                    ////
                    ///setto il colore a tutti i pilastri
                    foreach (Pillar p in SelectFurniture.currentFurniture.transform.GetComponent<HouseRoom>().pillars)
                    {
                        p.SetleftColor(newText2D);
                        p.SetrightColor(newText2D);
                    }
                }
                break;
            case "Inside2":
                //se il muro corrente è Whole Door
                if (SelectFurniture.currentFurniture.GetComponent<Construction>().name == "Whole" || SelectFurniture.currentFurniture.GetComponent<Construction>().name == "Door" || SelectFurniture.currentFurniture.GetComponent<Construction>().name == "Window")
                {
                    //prendo tutti i child con inside nel nome e li coloro
                    for (var k = 0; k < SelectFurniture.currentFurniture.GetComponent<Construction>().transform.FindChild("insides").childCount; k++)
                    {
                        SelectFurniture.currentFurniture.GetComponent<Construction>().transform.FindChild("insides").GetChild(k).GetComponent<Renderer>().material.mainTexture = newText2D;
                    }
                }
                else
                {
                    SelectFurniture.currentFurniture.GetComponent<Construction>().SetinsideColor(newText2D);
                }
                break;
            case "Outside2":
                //se il muro corrente è Whole Door
                if (SelectFurniture.currentFurniture.GetComponent<Construction>().name == "Whole" || SelectFurniture.currentFurniture.GetComponent<Construction>().name == "Door" || SelectFurniture.currentFurniture.GetComponent<Construction>().name == "Window")
                {
                    //prendo tutti i child con outside nel nome e li coloro
                    for (var k = 0; k < SelectFurniture.currentFurniture.GetComponent<Construction>().transform.FindChild("outsides").childCount; k++)
                    {
                        SelectFurniture.currentFurniture.GetComponent<Construction>().transform.FindChild("outsides").GetChild(k).GetComponent<Renderer>().material.mainTexture = newText2D;
                    }
                }
                else
                {
                    SelectFurniture.currentFurniture.GetComponent<Construction>().SetoutsideColor(newText2D);
                }

                switch (SelectFurniture.currentFurniture.GetComponent<Construction>().Direction)
                {
                    case Construction.Directions.Front:
                        SelectFurniture.currentFurniture.GetComponent<Construction>().transform.parent.GetComponent<HouseRoom>().topLeft.right.GetComponent<Renderer>().material.mainTexture = newText2D;
                        SelectFurniture.currentFurniture.GetComponent<Construction>().transform.parent.GetComponent<HouseRoom>().topRight.left.GetComponent<Renderer>().material.mainTexture = newText2D;
                        break;
                    case Construction.Directions.Back:
                        SelectFurniture.currentFurniture.GetComponent<Construction>().transform.parent.GetComponent<HouseRoom>().bottomLeft.right.GetComponent<Renderer>().material.mainTexture = newText2D;
                        SelectFurniture.currentFurniture.GetComponent<Construction>().transform.parent.GetComponent<HouseRoom>().bottomRight.left.GetComponent<Renderer>().material.mainTexture = newText2D;
                        break;
                    case Construction.Directions.Left:
                        SelectFurniture.currentFurniture.GetComponent<Construction>().transform.parent.GetComponent<HouseRoom>().bottomLeft.left.GetComponent<Renderer>().material.mainTexture = newText2D;
                        SelectFurniture.currentFurniture.GetComponent<Construction>().transform.parent.GetComponent<HouseRoom>().topLeft.left.GetComponent<Renderer>().material.mainTexture = newText2D;
                        break;
                    case Construction.Directions.Right:
                        SelectFurniture.currentFurniture.GetComponent<Construction>().transform.parent.GetComponent<HouseRoom>().topRight.right.GetComponent<Renderer>().material.mainTexture = newText2D;
                        SelectFurniture.currentFurniture.GetComponent<Construction>().transform.parent.GetComponent<HouseRoom>().bottomRight.right.GetComponent<Renderer>().material.mainTexture = newText2D;
                        break;
                    case Construction.Directions.none:
                    default:
                        break;
                }

                break;
            default: break;
        }

        //dopo aver settato il nuovo colore eseguo un update
        UpdateColor(SelectFurniture.currentFurniture);
    }

    void InitColor()
    {
        for (var i = 0; i < house.transform.childCount; i++)
        {
            //evito la stanza 0 perche è un default da non contare 
            //evito il player
            if (house.transform.GetChild(i).name == "Room0" || house.transform.GetChild(i).name == "Player") continue;

            if (house.transform.GetChild(i).GetComponent<HouseRoom>())
            {
                //update di tutte le stanze
                UpdateColor(house.transform.GetChild(i).GetComponent<HouseRoom>());

                //update di tutti gli oggetti singoli(muri,pavimento)
                foreach (Construction k in house.transform.GetChild(i).GetComponent<HouseRoom>().walls)
                {
                    UpdateColor(k);
                }
            }
        }
    }

    static void UpdateColor(Furniture f)
    {
        //faccio un foreach per vedere quale fra i tanti sprites e quello che corrisponde alla texture dell'oggetto corrente
        foreach (Sprite s in _sprites)
        {
            if (f is HouseRoom)
            {
                if (s.name == ((HouseRoom)f).roof.GetComponent<Renderer>().material.mainTexture.name)
                {
                    //cambio l'immaggine del bottone con l'immaggine del pattern dell'oggetto selezionato
                    _Roof.transform.GetChild(1).GetComponent<Image>().sprite = s;
                }

                if (s.name == ((HouseRoom)f).floor.inside.GetComponent<Renderer>().material.mainTexture.name)
                {
                    //cambio l'immaggine del bottone con l'immaggine del pattern dell'oggetto selezionato
                    _Floor.transform.GetChild(1).GetComponent<Image>().sprite = s;
                }

                for (int i = 0; i < ((HouseRoom)f).walls.Length; i++)
                {
                    if (s.name == ((HouseRoom)f).walls[i].inside.GetComponent<Renderer>().material.mainTexture.name)
                    {
                        //cambio l'immaggine del bottone con l'immaggine del pattern dell'oggetto selezionato
                        _Inside1.transform.GetChild(1).GetComponent<Image>().sprite = s;
                    }

                    if (s.name == ((HouseRoom)f).walls[i].outside.GetComponent<Renderer>().material.mainTexture.name)
                    {
                        //cambio l'immaggine del bottone con l'immaggine del pattern dell'oggetto selezionato
                        _Outside1.transform.GetChild(1).GetComponent<Image>().sprite = s;
                    }
                }
            }
            else if (f is Construction)
            {
                //controllo solo i muri perchè nel SelectFurniture non permetto la selezione di un pavimento ma solo di tutta la stanza
                if (s.name == ((Construction)f).inside.GetComponent<Renderer>().material.mainTexture.name)
                {
                    //cambio l'immaggine del bottone con l'immaggine del pattern dell'oggetto selezionato
                    _Inside2.transform.GetChild(1).GetComponent<Image>().sprite = s;
                }

                //controllo solo i muri perchè nel SelectFurniture non permetto la selezione di un pavimento ma solo di tutta la stanza
                if (s.name == ((Construction)f).outside.GetComponent<Renderer>().material.mainTexture.name)
                {
                    //cambio l'immaggine del bottone con l'immaggine del pattern dell'oggetto selezionato
                    _Outside2.transform.GetChild(1).GetComponent<Image>().sprite = s;
                }
            }
        }
    }

    void HandlePatternType()
    {
        if (currentbuttonpressed)
        {
            //mostrare solo i patterns adeguati al oggetto ai quali s'intende applicarli
            for (int k = 0; k < container.transform.childCount; k++)
            {
                if (container.transform.GetChild(k).GetComponent<Pattern>().Type == Pattern.Types.floor && currentbuttonpressed.name != "Floor" && currentPanel == Patterns)
                {
                    container.transform.GetChild(k).gameObject.SetActive(false);
                }
                else { container.transform.GetChild(k).gameObject.SetActive(true); }
            }
        }
    }

    public static void Close()
    {
        lastPanel = null;
        currentPanel = null;

        _menu.SetActive(false);
        _House.SetActive(false);
        _Wall.SetActive(false);
        _Patterns.SetActive(false);
        FurnitureActions._ActionsMenu.SetActive(true);

        currentbuttonpressed = null;
    }

    public static void Init()
    {
        GameObject newpanel = null;

        if (SelectFurniture.currentFurniture is HouseRoom)
        {
            newpanel = PatternsManager._House;
        }
        else if (SelectFurniture.currentFurniture is Construction)
        {
            newpanel = PatternsManager._Wall;
        }

        PatternsManager.currentPanel = newpanel;

        if (PatternsManager.currentPanel)
        {
            PatternsManager.lastPanel = PatternsManager.currentPanel;
        }

        UpdateColor(SelectFurniture.currentFurniture);
    }
}
