using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProjectUI : MonoBehaviour {

	public string id { get; set; }
    public string name { get; set; }
    public string author { get; set; }
    public Sprite image { get; set; }
    public DateTime create_date { get; set; }
    public DateTime lastchanges_date { get; set; }

    public ProjectUI(string id,string name,string author,Sprite image,DateTime create_date,DateTime lastchanges_date)
    {
        this.id = id;
        this.name = name;
        this.author = author;
        this.image = image;
        this.create_date = create_date;
        this.lastchanges_date = lastchanges_date;
    }
}
