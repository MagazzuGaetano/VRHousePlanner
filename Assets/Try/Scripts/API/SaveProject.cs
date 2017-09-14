using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.Net;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SaveProject : MonoBehaviour
{
    public Project currentproject;
    public string token = "";
    //corrisponde all empty gameobject named fixcenter
    public GameObject fixcenter;

    //left arrow for turn back to the projects list
    public Button Back;

    //variabile booleana che indica quando mostrare lo screen di saving
    public static bool canSave = false;

    // Use this for initialization
    void Start()
    {
        token = SharedVariables.token;
        currentproject = SharedVariables.currentproject;

        //se premo la freccia per tornare indietro salvo anche
        Back.onClick.AddListener(() =>
        {
            PlanGraphics.btn2D();
            Save();
        });

    }

    void Update()
    {
        //se sto uscendo dal progetto lo salvo
        if ((Input.GetKeyDown(KeyCode.Escape) && PlanGraphics.currentMode != PlanGraphics.Modes.VR && SceneManager.GetActiveScene().buildIndex == 2))
        {
            PlanGraphics.btn2D();
            Save();
        }

    }


    void SomeFunction()
    {
        List<Room> rooms = new List<Room>();
        List<Forniture> fornitures = new List<Forniture>();

        //house
        House _house = new House();
        _house.position = new Position(fixcenter.transform.position.x, fixcenter.transform.position.y, fixcenter.transform.position.z);
        _house.rotation = new Rotation(fixcenter.transform.rotation.x, fixcenter.transform.rotation.y, fixcenter.transform.rotation.z);
        _house.scale = new Scale(fixcenter.transform.localScale.x, fixcenter.transform.localScale.y, fixcenter.transform.localScale.z);

        for (var i = 0; i < fixcenter.transform.childCount; i++)
        {
            List<Wall> walls = new List<Wall>();
            //evito la stanza 0 perche è un default da non contare 
            //evito il player
            if (fixcenter.transform.GetChild(i).name == "RoomHide" || fixcenter.transform.GetChild(i).name == "Player") continue;

            //prendo tutte le stanze evitando il player ed ulteriori oggetti
            if (fixcenter.transform.GetChild(i).GetComponent<HouseRoom>())
            {
                HouseRoom currentRoom = fixcenter.transform.GetChild(i).GetComponent<HouseRoom>();

                //room
                Room _room = new Room();
                _room.name = currentRoom.name;
                _room.position = new Position(currentRoom.transform.position.x, currentRoom.transform.position.y, currentRoom.transform.position.z);
                _room.rotation = new Rotation(currentRoom.transform.rotation.x, currentRoom.transform.rotation.y, currentRoom.transform.rotation.z);
                _room.scale = new Scale(currentRoom.transform.localScale.x, currentRoom.transform.localScale.y, currentRoom.transform.localScale.z);

                //floor
                Floor _floor = new Floor();
                _floor.name = currentRoom.floor.name;
                _floor.position = new Position(currentRoom.floor.transform.position.x, currentRoom.floor.transform.position.y, currentRoom.floor.transform.position.z);
                _floor.rotation = new Rotation(currentRoom.floor.transform.rotation.x, currentRoom.floor.transform.rotation.y, currentRoom.floor.transform.rotation.z);
                _floor.scale = new Scale(currentRoom.floor.transform.localScale.x, currentRoom.floor.transform.localScale.y, currentRoom.floor.transform.localScale.z);
                _floor.inside = currentRoom.floor.inside.transform.GetComponent<Renderer>().material.mainTexture.name;
                _floor.outside = currentRoom.floor.outside.transform.GetComponent<Renderer>().material.mainTexture.name;

                //roof
                Roof _roof = new Roof();
                _roof.name = currentRoom.roof.name;
                _roof.position = new Position(currentRoom.roof.transform.position.x, currentRoom.roof.transform.position.y, currentRoom.roof.transform.position.z);
                _roof.rotation = new Rotation(currentRoom.roof.transform.rotation.x, currentRoom.roof.transform.rotation.y, currentRoom.roof.transform.rotation.z);
                _roof.scale = new Scale(currentRoom.roof.transform.localScale.x, currentRoom.roof.transform.localScale.y, currentRoom.roof.transform.localScale.z);
                _roof.inside = currentRoom.roof.transform.GetComponent<Renderer>().material.mainTexture.name;
                _roof.outside = null;

                //topleftpillar
                Column _topleft = new Column();
                _topleft.left = currentRoom.topLeft.GetleftColor();
                _topleft.right = currentRoom.topLeft.GetrightColor();
                //toprightpillar
                Column _topright = new Column();
                _topright.left = currentRoom.topRight.GetleftColor();
                _topright.right = currentRoom.topRight.GetrightColor();
                //bottomleftpillar
                Column _bottomleft = new Column();
                _bottomleft.left = currentRoom.bottomLeft.GetleftColor();
                _bottomleft.right = currentRoom.bottomLeft.GetrightColor();
                //bottomrightpillar
                Column _bottomright = new Column();
                _bottomright.left = currentRoom.bottomRight.GetleftColor();
                _bottomright.right = currentRoom.bottomRight.GetrightColor();

                foreach (Construction con in currentRoom.walls)
                {
                    if (con)
                    {
                        Wall _wall = new Wall();
                        _wall.name = con.name;
                        _wall.position = new Position(con.transform.localPosition.x, con.transform.localPosition.y, con.transform.localPosition.z);
                        _wall.rotation = new Rotation(con.transform.localEulerAngles.x, con.transform.localEulerAngles.y, con.transform.localEulerAngles.z);
                        _wall.scale = new Scale(con.transform.localScale.x, con.transform.localScale.y, con.transform.localScale.z);

                        //se il muro corrente è Whole o Door
                        if (con.name == "Whole" || con.name == "Door" || con.name == "Window")
                        {
                            //creo un array con il numero di muri inside del muro corrente
                            _wall.insides = new string[4];

                            //scorro i child dentro insides e salvo il colore
                            for (var k = 0; k < con.transform.FindChild("insides").childCount; k++)
                            {
                                _wall.insides[k] = con.transform.FindChild("insides").GetChild(k).transform.GetComponent<Renderer>().material.mainTexture.name;
                            }

                            //creo un array con il numero di muri outside del muro corrente
                            _wall.outsides = new string[4];

                            //scorro i child alla ricerca degli outside ed inside e salvo il colore
                            for (var k = 0; k < con.transform.FindChild("outsides").childCount; k++)
                            {
                                _wall.outsides[k] = con.transform.FindChild("outsides").GetChild(k).transform.GetComponent<Renderer>().material.mainTexture.name;
                            }
                        }
                        else
                        {
                            _wall.inside = con.inside.transform.GetComponent<Renderer>().material.mainTexture.name;
                            _wall.outside = con.outside.transform.GetComponent<Renderer>().material.mainTexture.name;
                        }

                        //salvo la direction del muro
                        _wall.direction = con.Direction.ToString();

                        //aggiungo il muro alla lista
                        walls.Add(_wall);
                    }
                }

                //setto il pavimento
                _room.floor = _floor;
                //setto il soffitto
                _room.roof = _roof;
                //setto i muri
                _room.walls = walls;
                //setto i pilastri
                _room.topleftpillar = _topleft;
                _room.toprightpillar = _topright;
                _room.bottomleftpillar = _bottomleft;
                _room.bottomrightpillar = _bottomright;

                //aggiungo la stanza in una lista
                rooms.Add(_room);
            }

            //prendo i vari oggetti
            else if (!fixcenter.transform.GetChild(i).GetComponent<HouseRoom>())
            {
                Furniture currentfurniture = fixcenter.transform.GetChild(i).GetComponent<Furniture>();

                Forniture _forniture = new Forniture();
                _forniture.name = currentfurniture.name;
                _forniture.position = new Position(currentfurniture.transform.position.x, currentfurniture.transform.position.y, currentfurniture.transform.position.z);
                _forniture.rotation = new Rotation(currentfurniture.transform.rotation.x, currentfurniture.transform.rotation.y, currentfurniture.transform.rotation.z);
                _forniture.scale = new Scale(currentfurniture.transform.localScale.x, currentfurniture.transform.localScale.y, currentfurniture.transform.localScale.z);

                //aggiungo forniture alla lista
                fornitures.Add(_forniture);
            }
        }

        _house.rooms = rooms;
        _house.fornitures = fornitures;

        //cambio solo i parametri d'aggiornare tipo l'orario dell'ultima modifica e le pos rot scale dei vari oggetti
        currentproject.lastchanges_date = DateTime.Now.ToString();
        currentproject.house = _house;
    }

    void Save()
    {
        //imposta canSave true ora puoi mostrare lo screen di saving
        canSave = true;

        SomeFunction();

        WebClient wc = new WebClient();
        wc.Headers["Authorization"] = token;
        wc.Headers["Content-Type"] = "application/json";

        Project p = new Project();
        p._id = currentproject._id;
        p.name = currentproject.name;
        p.author = currentproject.author;
        p.create_date = currentproject.create_date;
        p.lastchanges_date = currentproject.lastchanges_date;
        p.house = currentproject.house;

        string json = JsonMapper.ToJson(p);
        byte[] _byte = System.Text.Encoding.ASCII.GetBytes(json);
        byte[] reply = wc.UploadData(SharedVariables.address + "/api/" + SharedVariables.user._id + "/update/projects/" + currentproject._id, "PUT", _byte);

        string r = System.Text.Encoding.ASCII.GetString(reply);
        Response res = JsonMapper.ToObject<Response>(r);
    }
}
