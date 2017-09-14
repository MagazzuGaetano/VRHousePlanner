using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System;
using UnityEngine.UI;
using System.Net;
using UnityEngine.SceneManagement;
using System.Collections.Specialized;

public class LoadAllProjects : MonoBehaviour
{
    public string bundleurl = "file:///F:/TESI(worka)/RESTful%20API%20Using%20Node.js%20Express%20and%20MongoDB/assetbundles/ab";
    public ProjectUI prefab;
    public GameObject container;

    public Text username;
    public GameObject RenameMenu;
    public InputField Name;

    public GameObject TimesMenu;
    public Text ProjectName;
    public Text CreateDate;
    public Text LastChangesDate;
    public Button TimesMenuExit;

    public List<Project> projects = new List<Project>();

    //variabile booleana che server per stabilire quando viene cliccato un progetto e deve essere caricato
    public static bool canLoad = false;

    private AssetBundle assetbundle = null;
    private WWW www = null;
    private string token = "";
    private GameObject _Project;

    // Use this for initialization
    void Start()
    {
        username.text = SharedVariables.user.name;
        token = SharedVariables.token;

        WebClient wc = new WebClient();
        wc.Headers["Content-Type"] = "application/json";
        wc.Headers["Authorization"] = token;
        var str = SharedVariables.address + "/api/" + SharedVariables.user._id + "/projects";
        byte[] reply = wc.DownloadData(str);
        string r = System.Text.Encoding.ASCII.GetString(reply);

        projects = JsonMapper.ToObject<List<Project>>(r);

        SharedVariables.projects = projects;

        foreach (Project p in projects)
        {
            GenerateUI(p);
        }

        TimesMenuExit.onClick.AddListener(() => { TimesMenu.SetActive(false); });
    }

    void GenerateUI(Project p)
    {
        GameObject go = Instantiate(prefab.gameObject, container.transform, false);
        go.transform.FindChild("Image").transform.FindChild("name").GetComponent<Text>().text = p.name;

        go.GetComponent<ProjectUI>().id = p._id;
        go.GetComponent<ProjectUI>().name = p.name;
        go.GetComponent<ProjectUI>().author = p.author;
        go.GetComponent<ProjectUI>().image = null;
        go.GetComponent<ProjectUI>().create_date = DateTime.Parse(p.create_date);
        go.GetComponent<ProjectUI>().lastchanges_date = DateTime.Parse(p.lastchanges_date);

        go.transform.FindChild("Image").GetComponent<Button>().onClick.AddListener(() =>
        {
            SharedVariables.currentproject = p;
            SceneManager.LoadScene(2);

            //setto true la variabile canLoad di LoadAllProjects
            canLoad = true;
        });

        go.transform.FindChild("Image").transform.FindChild("name").GetComponent<Button>().onClick.AddListener(() =>
        {
            RenameMenu.SetActive(true);//mostro la form per la rinominazione
            Name.text = p.name;//imposto il nome del progetto corrente
            SharedVariables.currentproject = p;//imposto questo progetto come quello corrente
        });

        go.transform.FindChild("Image").transform.FindChild("delete").GetComponent<Button>().onClick.AddListener(() =>
        {
            SharedVariables.currentproject = p;//imposto questo progetto come quello corrente
            Delete(p);
            SceneManager.LoadScene(1);//ricarico la scena
        });

        go.transform.FindChild("Image").transform.FindChild("times").GetComponent<Button>().onClick.AddListener(() =>
        {
            TimesMenu.SetActive(true);
            SharedVariables.currentproject = p;
            ProjectName.text = SharedVariables.currentproject.name;
            CreateDate.text = SharedVariables.currentproject.create_date;
            LastChangesDate.text = SharedVariables.currentproject.lastchanges_date;
        });

    }

    private void Delete(Project p)
    {
        WebRequest request = WebRequest.Create(SharedVariables.address + "/api/"+SharedVariables.user._id+"/remove/projects/" + p._id);
        request.ContentType = "application/json";
        request.Headers["Authorization"] = token;
        request.Method = "PUT";
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
    }

    void LoadBundle(string name, Transform container)
    {
        Instantiate(assetbundle.LoadAsset(name), container);
    }
    
}
