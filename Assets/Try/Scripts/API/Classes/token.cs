using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selected
{
}

public class Getters
{
}

public class Paths
{
    public string admin { get; set; }
    public string password { get; set; }
    public string name { get; set; }
    public string _id { get; set; }
}

public class Ignore
{
}

public class Default
{
}

public class Init
{
    public bool admin { get; set; }
    public bool password { get; set; }
    public bool name { get; set; }
    public bool _id { get; set; }
}

public class Modify
{
}

public class Require
{
}

public class States
{
    public Ignore ignore { get; set; }
    public Default _default { get; set; }
    public Init init { get; set; }
    public Modify modify { get; set; }
    public Require require { get; set; }
}

public class ActivePaths
{
    public Paths paths { get; set; }
    public States states { get; set; }
    public IList<string> stateNames { get; set; }
}

public class Events
{
}

public class Emitter
{
    public object domain { get; set; }
    public Events _events { get; set; }
    public int _eventsCount { get; set; }
    public int _maxListeners { get; set; }
}

public class Controls
{
    public bool strictMode { get; set; }
    public Selected selected { get; set; }
    public Getters getters { get; set; }
    public bool wasPopulated { get; set; }
    public ActivePaths activePaths { get; set; }
    public Emitter emitter { get; set; }
}

public class Doc
{
    public bool admin { get; set; }
    public string password { get; set; }
    public string name { get; set; }
    public string _id { get; set; }
}

public class Example
{
    public string __a { get; set; }
    public bool isNew { get; set; }
    public Doc _doc { get; set; }
    public int iat { get; set; }
}