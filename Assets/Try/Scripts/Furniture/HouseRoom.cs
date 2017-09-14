using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseRoom : Furniture
{
    public Construction front, left, right, back;
    public Pillar topLeft, topRight, bottomLeft, bottomRight;
    public Construction floor;
    public GameObject roof;
    
    public Pillar[] pillars
    {
        get
        {
            return new Pillar[4] { topLeft, topRight, bottomLeft, bottomRight };
        }
    }

    public Construction[] walls
    {
        get
        {
            return new Construction[4] { front, left, right, back };
        }
    }

    public GameObject[] GetBorder
    {
        get
        {
            return new GameObject[4] { front.border, back.border, right.border, left.border };
        }
    }

}
