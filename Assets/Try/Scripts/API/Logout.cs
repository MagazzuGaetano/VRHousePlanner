using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Logout : MonoBehaviour
{

    public Button logout;

    // Use this for initialization
    void Start()
    {
        logout.onClick.AddListener(() =>
        {
            SharedVariables.user = null;
            SharedVariables.token = "";

            SceneManager.LoadScene(0);
        });
    }
}
