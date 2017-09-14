using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour {

    public static string address;
    public Button settings;
    public GameObject panel;
    public InputField InP_address;
    public Button CANCEL;
    public Button OK;

	// Use this for initialization
	void Start () {
        InP_address.text = SharedVariables.address;
        settings.onClick.AddListener(() => { panel.SetActive(true); });
        CANCEL.onClick.AddListener(() => { panel.SetActive(false); });
        OK.onClick.AddListener(() => {
            if (!SharedVariables.address.Equals(InP_address.text))
            {
                SharedVariables.address = InP_address.text;
            }
            panel.SetActive(false);
        });
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
