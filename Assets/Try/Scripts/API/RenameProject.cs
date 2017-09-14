using LitJson;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RenameProject : MonoBehaviour {

    public GameObject RenameMenu;
    public Button OK;
    public Button CANCEL;
    public InputField Name;

    // Use this for initialization
    void Start()
    {

        CANCEL.onClick.AddListener(() => { RenameMenu.SetActive(false);});

        OK.onClick.AddListener(() =>
        {
            Project ProjectToPUT = SharedVariables.currentproject;//prendo il progetto corrente
            ProjectToPUT.name = Name.text;//ne assegno il nuovo nome

            //eseguo la PUT
            WebClient wc = new WebClient();
            wc.Headers["Content-Type"] = "application/json";
            wc.Headers["Authorization"] = SharedVariables.token;
            string json = JsonMapper.ToJson(ProjectToPUT);
            byte[] _byte = System.Text.Encoding.ASCII.GetBytes(json);
            byte[] reply = wc.UploadData(SharedVariables.address + "/api/"+SharedVariables.user._id+"/update/projects/" + SharedVariables.currentproject._id, "PUT", _byte);
            string r = System.Text.Encoding.ASCII.GetString(reply);
            Project updatedProject = JsonMapper.ToObject<Project>(r);
            SharedVariables.currentproject = updatedProject;//cambio il progetto corrente

            RenameMenu.SetActive(false);//chiudo la form
            SceneManager.LoadScene(1);//ricarico la scena

        });
    }
}
