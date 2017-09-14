using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IndexGraphics : MonoBehaviour
{
    public GameObject SelectName;
    public Button Btn;

    // Use this for initialization
    void Start()
    {
        Btn.onClick.AddListener(() =>{SelectName.SetActive(true);});
    }
}
