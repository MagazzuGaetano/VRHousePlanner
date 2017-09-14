using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlanGraphics : MonoBehaviour
{
    public GameObject terrain;
    public Button btn_VR;
    public Slider slider;
    public Button btn_Slider;

    public GameObject Player;
    public GameObject VrMain;

    public GameObject Cam2D;
    public GameObject Cam3D;

    public enum Modes { VR, _2D, _3D }
    public static Modes currentMode;
    public static Modes lastMode;

    private bool isClick = true;

    public static Quaternion oldRotation = Quaternion.identity;
    private Quaternion oldRotation2 = Quaternion.identity;

    public static GameObject _Cam3D, _Cam2D, _Camera, _terrain;

    // Use this for initialization
    void Start()
    {
        _Cam3D = Cam3D;
        _Cam2D = Cam2D;
        _Camera = this.gameObject;
        _terrain = terrain;

        slider.value = 0;
        lastMode = currentMode;
        btn2D();
        currentMode = Modes._2D;

        btn_VR.onClick.AddListener(() =>
        {
            if (SelectFurniture.currentFurniture != null) return; // se c'è un elemento selezionato return

            lastMode = currentMode;

            oldRotation2 = terrain.transform.rotation;
            terrain.transform.rotation = Quaternion.identity;

            GetComponent<Canvas>().gameObject.SetActive(false);
            Cam3D.SetActive(false);
            Cam2D.SetActive(false);
            VrMain.SetActive(true);
            VrMain.GetComponent<GvrViewer>().VRModeEnabled = true;
            Player.SetActive(true);

            currentMode = Modes.VR;
        });

        btn_Slider.onClick.AddListener(() =>
        {
            isClick = !isClick;
            slider.value = isClick ? 1 : 0;

            if (slider.value == 1)
            {
                lastMode = currentMode;
                btn3D();
                currentMode = Modes._3D;
            }
            else
            {
                lastMode = currentMode;
                btn2D();
                currentMode = Modes._2D;
            }
        });
    }

    public static void btn2D()
    {
        _Cam3D.SetActive(false);
        _Cam2D.SetActive(true);

        _Camera.GetComponent<Canvas>().worldCamera = _Cam2D.GetComponent<Camera>();

        oldRotation = _terrain.transform.rotation;
        _terrain.transform.rotation = Quaternion.identity;
    }

    void btn3D()
    {
        Cam2D.SetActive(false);
        Cam3D.SetActive(true);

        GetComponent<Canvas>().worldCamera = Cam3D.GetComponent<Camera>();

        if (oldRotation != null) { terrain.transform.rotation = oldRotation; }
    }

    // Update is called once per frame
    void Update()
    {
        if (oldRotation2 != null && lastMode == PlanGraphics.Modes.VR)
        {
            terrain.transform.rotation = oldRotation2;
        }
    }

}
