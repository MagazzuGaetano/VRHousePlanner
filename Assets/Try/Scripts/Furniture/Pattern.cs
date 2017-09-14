using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pattern : MonoBehaviour {

    public int id;
    public string name;
    public Types Type;
    public Texture2D text2d;
    public Sprite icon;
    public GameObject pattern;

    public enum Types {
        house,
        floor,
        roof
    }

}
