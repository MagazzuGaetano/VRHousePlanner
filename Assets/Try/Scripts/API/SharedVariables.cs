using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharedVariables : MonoBehaviour {

    public static User user = null;
    public static List<Project> projects;
    public static string token = "";
    public static Project currentproject;

    public static string address = "http://vrhouseplanner.herokuapp.com";

    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
}
