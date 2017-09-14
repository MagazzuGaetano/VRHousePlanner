using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Position
{
    public float z { get; set; }
    public float y { get; set; }
    public float x { get; set; }

    public Position() { }

    public Position(float x, float y, float z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }
}

public class Rotation
{
    public float z { get; set; }
    public float y { get; set; }
    public float x { get; set; }

    public Rotation() { }

    public Rotation(float x, float y, float z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }
}

public class Scale
{
    public float z { get; set; }
    public float y { get; set; }
    public float x { get; set; }

    public Scale() { }

    public Scale(float x, float y, float z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }
}

public class Floor
{
    public string name { get; set; }
    public Position position { get; set; }
    public Rotation rotation { get; set; }
    public Scale scale { get; set; }
    public string inside { get; set; }
    public string outside { get; set; }
}

public class Roof
{
    public string name { get; set; }
    public Position position { get; set; }
    public Rotation rotation { get; set; }
    public Scale scale { get; set; }
    public string inside { get; set; }
    public string outside { get; set; }
}

public class Column
{
    public string left { get; set; }
    public string right { get; set; }
}

public class Wall
{
    public string name { get; set; }
    public Position position { get; set; }
    public Rotation rotation { get; set; }
    public Scale scale { get; set; }
    public string inside { get; set; }
    public string[] insides { get; set; }
    public string outside { get; set; }
    public string[] outsides { get; set; }
    public string direction { get; set; }
}

public class Room
{
    public string name { get; set; }
    public Position position { get; set; }
    public Rotation rotation { get; set; }
    public Scale scale { get; set; }
    public Floor floor { get; set; }
    public Roof roof { get; set; }
    public Column topleftpillar { get; set; }
    public Column toprightpillar { get; set; }
    public Column bottomleftpillar { get; set; }
    public Column bottomrightpillar { get; set; }
    public List<Wall> walls { get; set; }
}

public class Forniture
{
    public string name { get; set; }
    public Position position { get; set; }
    public Rotation rotation { get; set; }
    public Scale scale { get; set; }
}

public class House
{
    public Position position { get; set; }
    public Rotation rotation { get; set; }
    public Scale scale { get; set; }
    public List<Room> rooms { get; set; }
    public List<Forniture> fornitures { get; set; }
}


public class Project
{
    public string _id { get; set; }
    public string name { get; set; }
    public string author { get; set; }
    public string create_date { get; set; }
    public string lastchanges_date { get; set; }
    public House house { get; set; }
}
