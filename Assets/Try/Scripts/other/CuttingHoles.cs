using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingHoles : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.triangleIndex == -1) return;

                int[] triangles = transform.GetComponent<MeshFilter>().mesh.triangles;
                Vector3[] vertices = transform.GetComponent<MeshFilter>().mesh.vertices;

                int hitTri = hit.triangleIndex;

                Vector3 p0 = vertices[triangles[hitTri * 3 + 0]];
                Vector3 p1 = vertices[triangles[hitTri * 3 + 1]];
                Vector3 p2 = vertices[triangles[hitTri * 3 + 2]];

                float edge1 = Vector3.Distance(p0, p1);
                float edge2 = Vector3.Distance(p0, p2);
                float edge3 = Vector3.Distance(p1, p2);

                Vector3 shared1=Vector3.zero;
                Vector3 shared2 = Vector3.zero;

                if (edge1 > edge2 && edge1 > edge3)
                {
                    shared1 = p0;
                    shared2 = p1;
                }
                else if (edge2 > edge1 && edge2 > edge3)
                {
                    shared1 = p0;
                    shared2 = p2;
                }
                else if (edge3 > edge1 && edge3 > edge2)
                {
                    shared1 = p1;
                    shared2 = p2;
                }

                int v1 = findVertex(shared1);
                int v2 = findVertex(shared2);

                deleteSquare(hitTri, findTriangle(vertices[v1], vertices[v2], hitTri));
            }
        }
    }

    private int findVertex(Vector3 v)
    {
        Vector3[] vertices = transform.GetComponent<MeshFilter>().mesh.vertices;
        for (int i = 0; i < vertices.Length; i++)
        {
            if (vertices[i] == v)
                return i;
        }
        return -1;
    }

    private int findTriangle(Vector3 v1, Vector3 v2, int notTriIndex)
    {
        int[] triangles = transform.GetComponent<MeshFilter>().mesh.triangles;
        Vector3[] vertices = transform.GetComponent<MeshFilter>().mesh.vertices;
        int i = 0;
        int j = 0;
        int found = 0;
        while (j < triangles.Length)
        {
            if (j / 3 != notTriIndex)
            {
                if (vertices[triangles[j]] == v1 && (vertices[triangles[j + 1]] == v2 || vertices[triangles[j + 2]] == v2))
                    return j / 3;
                if (vertices[triangles[j]] == v2 && (vertices[triangles[j + 1]] == v1 || vertices[triangles[j + 2]] == v1))
                    return j / 3;
                if (vertices[triangles[j + 1]] == v2 && (vertices[triangles[j]] == v1 || vertices[triangles[j + 2]] == v1))
                    return j / 3;
                if (vertices[triangles[j + 1]] == v1 && (vertices[triangles[j]] == v2 || vertices[triangles[j + 2]] == v2))
                    return j / 3;
            }
            j += 3;
        }
        return -1;
    }

    private void deleteSquare(int index1, int index2)
    {
        Destroy(this.gameObject.GetComponent<MeshCollider>());
        Mesh mesh = transform.GetComponent<MeshFilter>().mesh;
        int[] oldTriangles = mesh.triangles;
        int[] newTriangles = new int[mesh.triangles.Length];
        int i = 0;
        int j = 0;
        while (j < mesh.triangles.Length)
        {
            if (j != index1 * 3 && j != index2 * 3)
            {
                newTriangles[i++] = oldTriangles[j++];
                newTriangles[i++] = oldTriangles[j++];
                newTriangles[i++] = oldTriangles[j++];
            }
            else
            {
                j += 3;
            }
        }
        transform.GetComponent<MeshFilter>().mesh.triangles = newTriangles;
        this.gameObject.AddComponent<MeshCollider>();
    }
}
