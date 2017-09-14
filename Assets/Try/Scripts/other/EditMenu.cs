using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditMenu : MonoBehaviour
{
   /* public GameObject gvrviewer;
    public GameObject plane;
    public RectTransform canvas;

    public Button slider;

    public GameObject Menu;
    public static GameObject _Menu;

    public GameObject UpDownGO, RotateGO;
    public static GameObject _UpDownGO, _RotateGO;

    public Button UpDown_btn, Rotate_btn, Destroy_btn, Paint_btn, Scale_btn, Exit_btn;

    public GameObject scale_menù;
    public Slider X_scale;
    public Slider Y_scale;
    public Slider Z_scale;

    private float speed = 0.5f;
    private float sensibility = 0.2f;

    // Use this for initialization
    void Start()
    {
        _Menu = Menu;
        _UpDownGO = UpDownGO;
        _RotateGO = RotateGO;

        Scale_btn.onClick.AddListener(() =>
        {
            scale_menù.SetActive(true);
        });

        slider.onClick.AddListener(() =>
        {
            if (_Menu.activeSelf && SelectFurniture.currentFurniture.type == Furniture.types.interior ||
                    _Menu.activeSelf && SelectFurniture.currentFurniture.type == Furniture.types.construction)
                UpDownGO.SetActive(PlanGraphics.currentMode == PlanGraphics.Modes._2D ? false : true);
        });

        Exit_btn.onClick.AddListener(() =>
        {
            SelectFurniture.lastFurniture = SelectFurniture.currentFurniture;

            Menu.SetActive(false);
            UpDownGO.SetActive(false);
            RotateGO.SetActive(false);
            scale_menù.SetActive(false);

            switch (SelectFurniture.currentFurniture.type)
            {
                case Furniture.types.construction:
                case Furniture.types.interior:
                    SelectFurniture.currentFurniture.border.SetActive(false);
                    break;
                case Furniture.types.room:
                    if (SelectFurniture.currentFurniture.transform.childCount > 1 && SelectFurniture.currentWall == null)
                    {
                        SelectFurniture.currentFurniture.GetComponent<BoxCollider>().enabled = false;
                        SelectFurniture.currentFurniture.transform.gameObject.getChildrenByTag("wall").ForEach((GameObject g) => { g.transform.FindChild("border").gameObject.SetActive(false); });
                    }
                    else if (SelectFurniture.currentWall != null)
                    {
                        SelectFurniture.currentWall.transform.parent.FindChild("border").gameObject.SetActive(false);
                        SelectFurniture.currentWall = null;
                    }
                    break;
                default:
                    break;
            }

            SelectFurniture.currentFurniture = null;
        });

        Destroy_btn.onClick.AddListener(() =>
        {
            Menu.SetActive(false);
            UpDownGO.SetActive(false);
            RotateGO.SetActive(false);
            scale_menù.SetActive(false);

            GameObject target = null;

            switch (SelectFurniture.currentFurniture.type)
            {
                case Furniture.types.construction:
                case Furniture.types.interior:
                    target = SelectFurniture.currentFurniture.gameObject;
                    break;
                case Furniture.types.room:
                    if (SelectFurniture.currentFurniture.transform.childCount > 1 && SelectFurniture.currentWall == null)
                    {
                        target = SelectFurniture.currentFurniture.gameObject;
                    }
                    else if (SelectFurniture.currentWall != null)
                    {
                        target = SelectFurniture.currentWall.transform.parent.gameObject;
                        SelectFurniture.currentWall = null;
                    }
                    break;
                default:
                    break;
            }

            Destroy(target);
            SelectFurniture.currentFurniture = null;
        });

        Paint_btn.onClick.AddListener(() =>
        {
            if (SelectFurniture.currentFurniture.type != Furniture.types.room) return;
            PatternsManager._menu.SetActive(true);
            PatternsManager.currentPanel.SetActive(true);
            Menu.SetActive(false);
        });

    }

    // Update is called once per frame
    void Update()
    {
        if (_Menu.activeSelf && SelectFurniture.currentFurniture.type == Furniture.types.interior ||
            _Menu.activeSelf && SelectFurniture.currentFurniture.type == Furniture.types.construction)
            UpDownGO.SetActive(PlanGraphics.currentMode == PlanGraphics.Modes._2D ? false : true);

        if (SelectFurniture.currentFurniture != null)
        {
            Vector3 v = Camera.main.WorldToScreenPoint(SelectFurniture.currentFurniture.transform.position);
            EditMenu._RotateGO.transform.GetComponent<RectTransform>().position = new Vector2(v.x + 400f, v.y);
            EditMenu._UpDownGO.transform.GetComponent<RectTransform>().position = new Vector2(v.x - 400f, v.y);

            if (scale_menù.activeSelf)
            {
                if (PlanGraphics.currentMode == PlanGraphics.Modes._2D)
                {
                    Y_scale.gameObject.SetActive(false);
                }
                else
                {
                    Y_scale.gameObject.SetActive(true);
                }

                X_scale.onValueChanged.AddListener((float val) =>
                 {
                     if (val != 0)
                         SelectFurniture.currentFurniture.transform.localScale = new Vector3(val, SelectFurniture.currentFurniture.transform.localScale.y, SelectFurniture.currentFurniture.transform.localScale.z);
                 });

                Y_scale.onValueChanged.AddListener((float val) =>
                {
                    if (val > 0)
                        SelectFurniture.currentFurniture.transform.localScale = new Vector3(SelectFurniture.currentFurniture.transform.localScale.x, val, SelectFurniture.currentFurniture.transform.localScale.z);
                });

                Z_scale.onValueChanged.AddListener((float val) =>
                {
                    if (val != 0)
                        SelectFurniture.currentFurniture.transform.localScale = new Vector3(SelectFurniture.currentFurniture.transform.localScale.x, SelectFurniture.currentFurniture.transform.localScale.y, val);
                });
            }
        }

    }

    public void Rotate() { SelectFurniture.currentFurniture.Rotate(sensibility); }
    public void EndRotate() { SelectFurniture.currentFurniture.EndRotate(); }
    public void Updown() { SelectFurniture.currentFurniture.UpDown(); }
    public void EndUpDown() { SelectFurniture.currentFurniture.EndUpDown(); }

    //public static void CalculateDistance()
    //{
    //    Transform currentwallTop = null;
    //    Transform currentwallBottom = null;
    //    Transform currentwallRight = null;
    //    Transform currentwallLeft = null;

    //    if (currentRoom.GetComponent<Transform>().FindChild("wall-front"))
    //        currentwallTop = currentRoom.GetComponent<Transform>().FindChild("wall-front").FindChild("wall-inside").transform;
    //    if (currentRoom.GetComponent<Transform>().FindChild("wall-back"))
    //        currentwallBottom = currentRoom.GetComponent<Transform>().FindChild("wall-back").FindChild("wall-inside").transform;
    //    if (currentRoom.GetComponent<Transform>().FindChild("wall-right"))
    //        currentwallRight = currentRoom.GetComponent<Transform>().FindChild("wall-right").FindChild("wall-inside").transform;
    //    if (currentRoom.GetComponent<Transform>().FindChild("wall-left"))
    //        currentwallLeft = currentRoom.GetComponent<Transform>().FindChild("wall-left").FindChild("wall-inside").transform;

    //    float FromWallTopToCenter = Vector3.Distance(currentRoom.transform.position, currentwallTop.position);
    //    float FromWallBottomToCenter = Vector3.Distance(currentRoom.transform.position, currentwallBottom.position);
    //    float FromWallRightToCenter = Vector3.Distance(currentRoom.transform.position, currentwallRight.position);
    //    float FromWallLeftToCenter = Vector3.Distance(currentRoom.transform.position, currentwallLeft.position);

    //    print("\n" + currentRoom + ":\ntop" + FromWallTopToCenter + "\nbottom" +
    //                                          FromWallBottomToCenter + "\nleft" +
    //                                          FromWallLeftToCenter + "\nright" +
    //                                          FromWallRightToCenter);

    //    foreach (GameObject forniture in fornitures)
    //    {
    //        Vector2 pos = new Vector2(currentRoom.transform.position.x, currentRoom.transform.position.z) - new Vector2(forniture.transform.position.x, forniture.transform.position.z);

    //        if (pos.x <= +FromWallRightToCenter &&
    //            pos.x >= -FromWallLeftToCenter &&
    //            pos.y >= -FromWallBottomToCenter &&
    //            pos.y <= +FromWallTopToCenter)
    //        {
    //            forniture.transform.SetParent(currentRoom.transform);
    //        }

    //    }

    //}





    //GameObject All = Palette.transform.FindChild("All").gameObject;
    //GameObject Wall = Palette.transform.FindChild("Wall").gameObject;
    //GameObject WallPatterns = Palette.transform.FindChild("WallPatterns").gameObject;
    //GameObject FloorPatterns = Palette.transform.FindChild("FloorPatterns").gameObject;

    //GameObject[] currentTarget = new GameObject[4];

    //Paint_btn.onClick.AddListener(() =>
    //{
    //    if (SelectFurniture.currentFurniture.tag == "room")
    //    {
    //        Palette.SetActive(true);
    //        Wall.SetActive(false);
    //        WallPatterns.SetActive(false);
    //        FloorPatterns.SetActive(false);
    //        All.SetActive(true);
    //    }
    //    else if (SelectFurniture.currentFurniture.tag.Contains("wall"))
    //    {
    //        Palette.SetActive(true);
    //        WallPatterns.SetActive(false);
    //        FloorPatterns.SetActive(false);
    //        All.SetActive(false);
    //        Wall.SetActive(true);
    //    }
    //});

    //Paint_Exit_btn.onClick.AddListener(() =>
    //{
    //    Palette.SetActive(false);
    //    WallPatterns.SetActive(false);
    //    FloorPatterns.SetActive(false);
    //});

    //btn_floor.onClick.AddListener(() =>
    //{
    //    All.SetActive(false);
    //    FloorPatterns.SetActive(true);
    //    currentTarget = new GameObject[1];
    //    currentTarget[0] = target.transform.FindChild("floor").FindChild("floor-inside").gameObject;
    //});

    //btn_roof.onClick.AddListener(() =>
    //{
    //    WallPatterns.SetActive(true);
    //    All.SetActive(false);
    //    currentTarget = new GameObject[1];
    //    currentTarget[0] = target.transform.FindChild("floor").FindChild("floor-outside").gameObject;
    //});

    //btn_Allwallinside.onClick.AddListener(() =>
    //{
    //    All.SetActive(false);
    //    WallPatterns.SetActive(true);

    //    currentTarget = new GameObject[4];

    //    currentTarget[0] = target.transform.FindChild("wall-front").FindChild("wall-inside").gameObject;
    //    currentTarget[1] = target.transform.FindChild("wall-back").FindChild("wall-inside").gameObject;
    //    currentTarget[2] = target.transform.FindChild("wall-left").FindChild("wall-inside").gameObject;
    //    currentTarget[3] = target.transform.FindChild("wall-right").FindChild("wall-inside").gameObject;
    //});
    //btn_Allwalloutside.onClick.AddListener(() =>
    //{
    //    All.SetActive(false);
    //    WallPatterns.SetActive(true);

    //    currentTarget = new GameObject[4];

    //    currentTarget[0] = target.transform.FindChild("wall-front").FindChild("wall-outside").gameObject;
    //    currentTarget[1] = target.transform.FindChild("wall-back").FindChild("wall-outside").gameObject;
    //    currentTarget[2] = target.transform.FindChild("wall-left").FindChild("wall-outside").gameObject;
    //    currentTarget[3] = target.transform.FindChild("wall-right").FindChild("wall-outside").gameObject;
    //});

    //btn_Wallwallinside.onClick.AddListener(() =>
    //{
    //    Wall.SetActive(false);
    //    WallPatterns.SetActive(true);
    //    currentTarget = new GameObject[1];
    //    currentTarget[0] = target.transform.parent.FindChild("wall-inside").gameObject;
    //});
    //btn_Wallwalloutside.onClick.AddListener(() =>
    //{
    //    Wall.SetActive(false);
    //    WallPatterns.SetActive(true);
    //    currentTarget = new GameObject[1];
    //    currentTarget[0] = target.transform.parent.FindChild("wall-outside").gameObject;
    //});

    //foreach (Button btn in WallButtons)
    //{
    //    btn.onClick.AddListener(() =>
    //    {
    //        foreach (Texture text in Patterns)
    //        {
    //            if (text.name == btn.name && WallPatterns.activeSelf)
    //            {
    //                foreach (GameObject ct in currentTarget)
    //                {
    //                    ct.GetComponent<Renderer>().material.mainTexture = text;

    //                    foreach (Sprite s in UIPatterns)
    //                    {
    //                        if (s.name == btn.name)
    //                        {
    //                            if (target.tag == "room")
    //                            {
    //                                if (ct == target.transform.FindChild("floor").FindChild("floor-outside").gameObject)
    //                                {
    //                                    btn_roof.GetComponent<Image>().sprite = s;
    //                                }
    //                                else if (ct == target.transform.FindChild("wall-front").FindChild("wall-inside").gameObject && currentTarget.Length > 1)
    //                                {
    //                                    btn_Allwallinside.GetComponent<Image>().sprite = s;
    //                                    btn_Wallwallinside.GetComponent<Image>().sprite = s;
    //                                }
    //                                else if (ct == target.transform.FindChild("wall-front").FindChild("wall-outside").gameObject && currentTarget.Length > 1)
    //                                {
    //                                    btn_Allwalloutside.GetComponent<Image>().sprite = s;
    //                                    btn_Wallwalloutside.GetComponent<Image>().sprite = s;
    //                                }
    //                            }
    //                            else if (target.tag.Contains("wall"))
    //                            {
    //                                if (ct == target.transform.parent.FindChild("wall-inside").gameObject)
    //                                {
    //                                    btn_Wallwallinside.GetComponent<Image>().sprite = s;
    //                                }
    //                                else if (ct == target.transform.parent.FindChild("wall-outside").gameObject)
    //                                {
    //                                    btn_Wallwalloutside.GetComponent<Image>().sprite = s;
    //                                }
    //                            }

    //                        }
    //                    }
    //                }
    //                break;
    //            }
    //        }
    //        if (target.tag == "room") { All.SetActive(true); }
    //        else if (target.tag.Contains("wall")) { Wall.SetActive(true); }
    //        WallPatterns.SetActive(false);
    //    });
    //}

    //foreach (Button btn in FloorButtons)
    //{
    //    btn.onClick.AddListener(() =>
    //    {
    //        foreach (Texture text in Patterns)
    //        {
    //            if (text.name == btn.name && FloorPatterns.activeSelf)
    //            {
    //                foreach (GameObject ct in currentTarget)
    //                {
    //                    ct.GetComponent<Renderer>().material.mainTexture = text;
    //                    foreach (Sprite s in UIPatterns)
    //                    {
    //                        if (s.name == btn.name)
    //                        {
    //                            if (ct == target.transform.FindChild("floor").FindChild("floor-inside").gameObject)
    //                            {
    //                                btn_floor.GetComponent<Image>().sprite = s;
    //                            }
    //                        }
    //                    }
    //                }
    //                break;
    //            }
    //        }

    //        All.SetActive(true);
    //        FloorPatterns.SetActive(false);
    //    });
    //}*/
}
