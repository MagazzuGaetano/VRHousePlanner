using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.UI;

class LoadProject : MonoBehaviour
{
    public string address;
    public Project currentproject;
    public string token = "";
    public Text currentprojectname;

    public GameObject fixcenter;
    public List<Furniture> items = new List<Furniture>();
    public GameObject RoomPrefab;

    // Use this for initialization
    void Start()
    {
        //prendo la lista di item del menu d'arredi
        items = FurnitureMenu.instance.items;

        token = SharedVariables.token;
        currentproject = SharedVariables.currentproject;
        currentprojectname.text = currentproject.name;

        //appena caricato il progetto la variabile canLoad la setto false
        LoadAllProjects.canLoad = false;

       /* WebClient wc = new WebClient();
        wc.Headers["Content-Type"] = "application/json";
        wc.Headers["Authorization"] = token;
        byte[] reply = wc.DownloadData(address + "/api/" + SharedVariables.user._id + "/projects/" + currentproject._id);
        string r = System.Text.Encoding.ASCII.GetString(reply);
        Project project = JsonMapper.ToObject<Project>(r);*/

        if (currentproject.house == null) return;

        //rooms
        for (var i = 0; i < currentproject.house.rooms.Count; i++)
        {
            //room
            //non faccio il foreach perchè qualsiasi stanza dovrò generare dovrò partire da RoomPrefab
            GameObject room = null;
            room = Instantiate(RoomPrefab, fixcenter.transform, false);
            room.transform.position = new Vector3(currentproject.house.rooms[i].position.x, currentproject.house.rooms[i].position.y, currentproject.house.rooms[i].position.z);
            room.transform.rotation = new Quaternion(currentproject.house.rooms[i].rotation.x, currentproject.house.rooms[i].rotation.y, currentproject.house.rooms[i].rotation.z, room.transform.rotation.w);
            room.transform.localScale = new Vector3(currentproject.house.rooms[i].scale.x, currentproject.house.rooms[i].scale.y, currentproject.house.rooms[i].scale.z);
            room.name = currentproject.house.rooms[i].name;

            //deve rimanere dopo il for perchè nel for assegno il svalore di room
            HouseRoom currentroom = room.GetComponent<HouseRoom>();

            //roof
            currentroom.roof.name = currentproject.house.rooms[i].roof.name;
            currentroom.roof.transform.position = new Vector3(currentproject.house.rooms[i].roof.position.x, currentproject.house.rooms[i].roof.position.y, currentproject.house.rooms[i].roof.position.z);
            currentroom.roof.transform.rotation = new Quaternion(currentproject.house.rooms[i].roof.rotation.x, currentproject.house.rooms[i].roof.rotation.y, currentproject.house.rooms[i].roof.rotation.z, currentroom.roof.transform.rotation.w);
            currentroom.roof.transform.localScale = new Vector3(currentproject.house.rooms[i].roof.scale.x, currentproject.house.rooms[i].roof.scale.y, currentproject.house.rooms[i].roof.scale.z);

            foreach (Sprite s in PatternsManager._sprites)
            {
                //il soffitto prende solo la texture inside perchè l'outside non esiste
                if (s.name == currentproject.house.rooms[i].roof.inside) currentroom.roof.GetComponent<Renderer>().material.mainTexture = s.texture;
            }

            //floor
            currentroom.floor.name = currentproject.house.rooms[i].floor.name;
            currentroom.floor.transform.position = new Vector3(currentproject.house.rooms[i].floor.position.x, currentproject.house.rooms[i].floor.position.y, currentproject.house.rooms[i].floor.position.z);
            currentroom.floor.transform.rotation = new Quaternion(currentproject.house.rooms[i].floor.rotation.x, currentproject.house.rooms[i].floor.rotation.y, currentproject.house.rooms[i].floor.rotation.z, currentroom.floor.transform.rotation.w);
            currentroom.floor.transform.localScale = new Vector3(currentproject.house.rooms[i].floor.scale.x, currentproject.house.rooms[i].floor.scale.y, currentproject.house.rooms[i].floor.scale.z);

            foreach (Sprite s in PatternsManager._sprites)
            {
                if (s.name == currentproject.house.rooms[i].floor.inside) currentroom.floor.inside.GetComponent<Renderer>().material.mainTexture = s.texture;
                if (s.name == currentproject.house.rooms[i].floor.outside) currentroom.floor.outside.GetComponent<Renderer>().material.mainTexture = s.texture;
            }

            //pillars
            foreach (Sprite s in PatternsManager._sprites)
            {
                //topleft
                if (s.name == currentproject.house.rooms[i].topleftpillar.left) currentroom.topLeft.SetleftColor(s.texture);
                if (s.name == currentproject.house.rooms[i].topleftpillar.right) currentroom.topLeft.SetrightColor(s.texture);
                //topright
                if (s.name == currentproject.house.rooms[i].toprightpillar.left) currentroom.topLeft.SetleftColor(s.texture);
                if (s.name == currentproject.house.rooms[i].toprightpillar.right) currentroom.topLeft.SetrightColor(s.texture);
                //bottomleft
                if (s.name == currentproject.house.rooms[i].bottomleftpillar.left) currentroom.bottomLeft.SetleftColor(s.texture);
                if (s.name == currentproject.house.rooms[i].bottomleftpillar.right) currentroom.bottomLeft.SetrightColor(s.texture);
                //bottomright
                if(s.name == currentproject.house.rooms[i].bottomrightpillar.left) currentroom.bottomRight.SetleftColor(s.texture);
                if (s.name == currentproject.house.rooms[i].bottomrightpillar.right) currentroom.bottomRight.SetrightColor(s.texture);
            }

            //walls
            for (var j = 0; j < currentproject.house.rooms[i].walls.Count; j++)
            {
                //scorro la lista degli item per verificare il nome ed instanziare l'oggetto esatto
                for (var z = 0; z < items.Count; z++)
                {
                    //controllo che l'item dentro la lista sia di tipo Construction perchè la lista presenta oggetti di diverso tipo
                    if (items[z].name == currentproject.house.rooms[i].walls[j].name && items[z] is Construction)
                    {
                        GameObject wall = Instantiate(items[z].gameObject, room.transform, false);
                        wall.transform.localPosition = new Vector3(currentproject.house.rooms[i].walls[j].position.x, currentproject.house.rooms[i].walls[j].position.y, currentproject.house.rooms[i].walls[j].position.z);
                        wall.transform.eulerAngles = new Vector3(currentproject.house.rooms[i].walls[j].rotation.x, currentproject.house.rooms[i].walls[j].rotation.y, currentproject.house.rooms[i].walls[j].rotation.z);
                        wall.transform.localScale = new Vector3(currentproject.house.rooms[i].walls[j].scale.x, currentproject.house.rooms[i].walls[j].scale.y, currentproject.house.rooms[i].walls[j].scale.z);
                        wall.name = currentproject.house.rooms[i].walls[j].name;

                        if (wall.name == "Whole" || wall.name == "Door" || wall.name == "Window")
                        {
                            for (var k = 0; k < wall.transform.FindChild("insides").childCount; k++)
                            {
                                foreach (Sprite s in PatternsManager._sprites)
                                {
                                    if (s.name == currentproject.house.rooms[i].walls[j].insides[k])
                                        wall.transform.FindChild("insides").GetChild(k).GetComponent<Renderer>().material.mainTexture = s.texture;
                                }
                            }

                            for (var k = 0; k < wall.transform.FindChild("outsides").childCount; k++)
                            {
                                foreach (Sprite s in PatternsManager._sprites)
                                {
                                    if (s.name == currentproject.house.rooms[i].walls[j].outsides[k])
                                        wall.transform.FindChild("outsides").GetChild(k).GetComponent<Renderer>().material.mainTexture = s.texture;
                                }
                            }
                        }
                        else
                        {
                            foreach (Sprite s in PatternsManager._sprites)
                            {
                                if (s.name == currentproject.house.rooms[i].walls[j].inside) wall.GetComponent<Construction>().inside.GetComponent<Renderer>().material.mainTexture = s.texture;
                                if (s.name == currentproject.house.rooms[i].walls[j].outside) wall.GetComponent<Construction>().outside.GetComponent<Renderer>().material.mainTexture = s.texture;
                            }
                        }

                        wall.GetComponent<Construction>().Direction = (Construction.Directions)Enum.Parse(typeof(Construction.Directions), currentproject.house.rooms[i].walls[j].direction);

                        //aggiungo i muri nella stanza
                        //assegno il muro nella sua posizione in base al nome
                        if (wall.GetComponent<Construction>().Direction == Construction.Directions.Front) currentroom.front = wall.GetComponent<Construction>();
                        else if (wall.GetComponent<Construction>().Direction == Construction.Directions.Back) currentroom.back = wall.GetComponent<Construction>();
                        else if (wall.GetComponent<Construction>().Direction == Construction.Directions.Left) currentroom.left = wall.GetComponent<Construction>();
                        else if (wall.GetComponent<Construction>().Direction == Construction.Directions.Right) currentroom.right = wall.GetComponent<Construction>();

                        currentroom.walls[j] = wall.GetComponent<Construction>();
                    }
                }

            }

        }

        //fornitures
        for (var k = 0; k < currentproject.house.fornitures.Count; k++)
        {
            //scorro la lista degli item per verificare il nome ed instanziare l'oggetto esatto
            foreach (Furniture i in items)
            {
                //controllo che l'item dentro la lista non sia di tipo HouseRoom 0 Construction perchè la lista presenta oggetti di diverso tipo
                if (i is HouseRoom || i is Construction) continue;
                
                if (i.name == currentproject.house.fornitures[k].name)
                {
                    GameObject forniture = Instantiate(i.gameObject, fixcenter.transform, false);
                    forniture.transform.position = new Vector3(currentproject.house.fornitures[k].position.x, currentproject.house.fornitures[k].position.y, currentproject.house.fornitures[k].position.z);
                    forniture.transform.rotation = new Quaternion(currentproject.house.fornitures[k].rotation.x, currentproject.house.fornitures[k].rotation.y, currentproject.house.fornitures[k].rotation.z, forniture.transform.rotation.w);
                    forniture.transform.localScale = new Vector3(currentproject.house.fornitures[k].scale.x, currentproject.house.fornitures[k].scale.y, currentproject.house.fornitures[k].scale.z);
                    forniture.name = currentproject.house.fornitures[k].name;
                }
            }
        }

    }
}

