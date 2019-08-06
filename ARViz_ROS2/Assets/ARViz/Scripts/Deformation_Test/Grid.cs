using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class Grid : MonoBehaviour
{
    public int xSize, ySize;
    private Vector3[] vertices;
    private Mesh mesh;
    private float cnt;
    private float dist_factor;

    private void Awake()
    {
        cnt = 0;
        dist_factor = 0.5f;
        Generate();
    }

    void Start()
    {
        
    }
    
    void Update()
    {
        vertices = mesh.vertices;
        /*
        Debug.Log("[0] " + vertices[0]);
        Debug.Log("[1] " + vertices[1]);
        Debug.Log("[15] " + vertices[15]);
        */
        vertices[14] = new Vector3(3 * dist_factor, 1 * dist_factor, Mathf.Cos(cnt));
        //vertices[14] = new Vector3(3, 1, Mathf.Cos(cnt));
        cnt += 0.1f;
        mesh.vertices = vertices;
    }
    
    private void Generate()
    {
        GetComponent<MeshFilter>().mesh = mesh = new Mesh();
        mesh.name = "Procedural Grid";
        vertices = new Vector3[(xSize + 1) * (ySize + 1)];
        for (int i = 0, y = 0; y <= ySize; y++)
        {
            for (int x = 0; x <= xSize; x++, i++)
            {
                vertices[i] = new Vector3(x * dist_factor, y * dist_factor, 0);
                //vertices[i] = new Vector3(x, y, 0);
                Debug.Log("["+i+"] " + vertices[i]);
                Debug.Log("dist_factor " + dist_factor);
            }
        }
        mesh.vertices = vertices;

        int[] triangles = new int[xSize * ySize * 6];
        for (int ti = 0, vi = 0, y = 0; y < ySize; y++, vi++)
        {
            for (int x = 0; x < xSize; x++, ti += 6, vi++)
            {
                triangles[ti] = vi;
                triangles[ti + 3] = triangles[ti + 2] = vi + 1;
                triangles[ti + 4] = triangles[ti + 1] = vi + xSize + 1;
                triangles[ti + 5] = vi + xSize + 2;
            }
        }
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }

    private void OnDrawGizmos()
    {
        if (vertices == null)
        {
            return;
        }
        Gizmos.color = Color.black;
        for (int i = 0; i < vertices.Length; i++)
        {
            Gizmos.DrawSphere(vertices[i], 0.1f);
        }
    }
}

// https://catlikecoding.com/unity/tutorials/procedural-grid/ 
