using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{

    public static Furniture[,,] grid = new Furniture[50, 50, 10];
    public Transform house;
    private Transform _furnitures;

    // Use this for initialization
    void Start()
    {
        //_furnitures = house.GetChild(0).transform;

        /*for (int i = 0; i < _furnitures.childCount; i++)
        {
            //grid[] = _furnitures.GetChild(i);
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        DrawGrid();

        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
        {
            SelectionX = (int)hit.point.x;
            SelectionY = (int)hit.point.z;
        }
        else
        {
            SelectionX = -1;
            SelectionY = -1;
        }
    }

    public static bool isEmpty(float x, float y, float z)
    {
        Vector3int pos = FromRealtoGrid(x, y, z);

        if (grid[pos.x, pos.y, pos.z] == null)
        {
            return true;
        }
        return false;
    }

    public struct Vector3int
    {
        public int x, y, z;

        public Vector3int(int x, int y, int z) : this()
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
    }

    private static Vector3int FromRealtoGrid(float x, float y, float z)
    {

        return new Vector3int(0, 0, 0);
    }

    private int SelectionX = -1;
    private int SelectionY = -1;

    private void DrawGrid()
    {
        Vector3 widthLine = Vector3.right * 200;
        Vector3 heightLine = Vector3.forward * 200;

        Vector3 offset = new Vector3(transform.position.x - 100, 0, transform.position.z - 100);

        for (int i = 0; i < 200; i++)
        {
            Vector3 start = Vector3.forward * i;
            Debug.DrawLine(start, start + widthLine, Color.white);

            for (int j = 0; j < 200; j++)
            {
                start = Vector3.right * j;
                Debug.DrawLine(start, start + heightLine, Color.white);
            }
        }

        if (SelectionX >= 0 && SelectionY >= 0)
        {
            Debug.DrawLine(
                 Vector3.forward * SelectionY + Vector3.right * SelectionX
                , Vector3.forward * (SelectionY + 1) + Vector3.right * (SelectionX + 1), Color.red);

            Debug.DrawLine(
                 Vector3.forward * (SelectionY + 1) + Vector3.right * SelectionX
                , Vector3.forward * SelectionY + Vector3.right * (SelectionX + 1), Color.red);
        }

    }

    private const float TILE_SIZE = 1.0f;
    private const float TILE_OFFSET = 0.5f;

    private Vector3 GetTileCenter(int x, int y)
    {
        Vector3 origin = Vector3.zero;
        origin.x += (TILE_SIZE * x) + TILE_OFFSET;
        origin.z += (TILE_SIZE * y) + TILE_OFFSET;
        return origin;
    }
}
