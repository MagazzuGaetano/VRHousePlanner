using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Construction : Furniture
{
    public GameObject inside;
    public GameObject outside;

    public Directions Direction;
    public enum Directions
    {
        Left,
        Right,
        Front,
        Back,
        none
    }

    public Texture GetinsideColor()
    {
        return this.inside.GetComponent<Renderer>().material.mainTexture;
    }

    public void SetinsideColor(Texture2D text)
    {
        this.inside.GetComponent<Renderer>().material.mainTexture = text;
    }

    public Texture GetoutsideColor()
    {
        return this.outside.GetComponent<Renderer>().material.mainTexture;
    }

    public void SetoutsideColor(Texture2D text)
    {
        this.outside.GetComponent<Renderer>().material.mainTexture = text;
    }
    
}
