using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User
{
    public string _id { get; set; }
    public string name { get; set; }
    public string password { get; set; }
    public bool admin { get; set; }
    public List<Project> projects { get; set; }
}