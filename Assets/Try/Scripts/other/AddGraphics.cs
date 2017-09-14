using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddGraphics : MonoBehaviour {

    public GameObject Add;
    public Button btn_add;
    public Button btn_exit;
 
	// Use this for initialization
	void Start () {
		btn_add.onClick.AddListener(() => { Add.SetActive(true); });
        btn_exit.onClick.AddListener(() => { Add.SetActive(false); });
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
