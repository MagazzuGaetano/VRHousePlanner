using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Net;
using LitJson;
using UnityEngine.SceneManagement;

public class SetProjectName : MonoBehaviour
{
    public string address;
    public GameObject SelectName;
    public Button OK;
    public Button CANCEL;
    public InputField Name;

    public static bool canCreate = false;

    // Use this for initialization
    void Start()
    {

        CANCEL.onClick.AddListener(() => { SelectName.SetActive(false); Name.text = ""; canCreate = false; });

        OK.onClick.AddListener(() =>
        {
            canCreate = true;
            Project p = new Project();
            p.name = Name.text;
            p.author = SharedVariables.user.name;
            p.create_date = DateTime.Now.ToString();
            p.lastchanges_date = DateTime.Now.ToString();

            WebClient wc = new WebClient();
            wc.Headers["Content-Type"] = "application/json";
            wc.Headers["Authorization"] = SharedVariables.token;

            string json = JsonMapper.ToJson(p);
            //non cambiare "\"_id\" : null,"
            int s = json.IndexOf("\"_id\" : null,");
            //neanche questa
            json = json.Remove(s, "\"_id\" : null,".Length);
            byte[] _byte = System.Text.Encoding.ASCII.GetBytes(json);
            byte[] reply = wc.UploadData(SharedVariables.address + "/api/"+SharedVariables.user._id+"/projects/", "PUT", _byte);
            string r = System.Text.Encoding.ASCII.GetString(reply);
            Project newproject = JsonMapper.ToObject<Project>(r);
            
            SharedVariables.currentproject = newproject;
            SelectName.SetActive(false);
        });
    }
}
