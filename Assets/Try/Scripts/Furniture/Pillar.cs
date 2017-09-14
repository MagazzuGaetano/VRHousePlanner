using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Pillar
{
    public GameObject left, right;

    public string GetleftColor()
    {
        return this.left.GetComponent<Renderer>().material.mainTexture.name;
    }

    public string GetrightColor()
    {
        return this.right.GetComponent<Renderer>().material.mainTexture.name;
    }

    public void SetleftColor(Texture2D text)
    {
        this.left.GetComponent<Renderer>().material.mainTexture = text;
    }

    public void SetrightColor(Texture2D text)
    {
        this.right.GetComponent<Renderer>().material.mainTexture = text;
    }
}
