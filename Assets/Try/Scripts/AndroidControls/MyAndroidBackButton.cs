using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MyAndroidBackButton : MonoBehaviour
{
    public GameObject Player = null;
    public GameObject VrMain = null;
    public GameObject Canvas = null;
    public GameObject Cam3D = null;
    public GameObject Cam2D = null;

    public static bool done = true;

    public MyAndroidBackButton()
    {
        done = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && done)
        {
            if (SceneManager.GetActiveScene().buildIndex == 0)
            {
                Application.Quit();
            }
            else if (SceneManager.GetActiveScene().buildIndex == 1)
            {
                if (!string.IsNullOrEmpty(SharedVariables.user.name))
                {
                    return;
                }
                else
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
                    done = false;
            }
            else if (PlanGraphics.currentMode == PlanGraphics.Modes.VR && SceneManager.GetActiveScene().buildIndex == 2) 
            {
                PlanGraphics.currentMode = PlanGraphics.lastMode;
                Cam3D.SetActive(PlanGraphics.lastMode == PlanGraphics.Modes._3D ? true : false);
                Cam2D.SetActive(PlanGraphics.lastMode == PlanGraphics.Modes._2D ? true : false);
                Player.SetActive(false);
                VrMain.GetComponent<GvrViewer>().VRModeEnabled = false;
                Canvas.SetActive(true);
                done = true;
                PlanGraphics.lastMode = PlanGraphics.Modes.VR;
            }
            else
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
                done = false;
            }
        }
    }
}
