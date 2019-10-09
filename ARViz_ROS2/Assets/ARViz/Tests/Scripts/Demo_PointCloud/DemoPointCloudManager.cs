using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ROS2;
using System;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class DemoPointCloudManager : MonoBehaviour
{
    private Node node;
    ISubscription<sensor_msgs.msg.Image > image_sub;
    private byte[] imageData;
    private int imageWidth, imageHeight, imageWidth_bytes;
    private Vector3[] vertices;
    private Mesh mesh;
    float dist_factor = 0.2f;
    float width_norm_coef, height_norm_coef;
    private bool msgReceived = false;
    private bool meshGenerated = false;
    private int msg_cnt = 0;

    void Start()
    {
        RCLdotnet.Init();
        node = RCLdotnet.CreateNode("pointcloud_listener");
        
        image_sub = node.CreateSubscription<sensor_msgs.msg.Image>(
            "camera/depth/image_raw/arviz", msg =>
            {
                if (msg_cnt % 50 == 0) {
                    if (!msgReceived)
                    {
                        //msgReceived = true;
                        Debug.Log("I received a msg!!");
                        imageData = msg.Data.ToArray();
                        imageWidth = (int)msg.Width;
                        imageHeight = (int)msg.Height;
                        Debug.Log(imageWidth + "x" + imageHeight + " - Length " + imageData.Length);
                        width_norm_coef = 1f / 10f;
                        height_norm_coef = 1f / 10f;
                        //Debug.Log(imageData[0] + " " + imageData[1] + " = " + BitConverter.ToUInt16(imageData, 0));
                        //Debug.Log(imageData[614396] + " " + imageData[614397] + " = " + BitConverter.ToUInt16(imageData, 614396));
                        if (!meshGenerated)
                        {
                            meshGenerated = true;
                            Generate();
                        }
                        UpdateImage();
                    }
                }
                msg_cnt++;
            }
        );
    }

    void Update()
    {
        RCLdotnet.SpinOnce(node, 500);
        //Debug.Log("Spinning...");
    }

    private void Generate()
    {
        Debug.Log("########### Generate - Init");
        GetComponent<MeshFilter>().mesh = mesh = new Mesh();
        mesh.name = "PointCloudViz";
        vertices = new Vector3[(imageWidth + 1) * (imageHeight + 1)];
        Debug.Log("########### Generate - creating vertices");
        Debug.Log("vertices.Length "+vertices.Length);
        // Crea los vertices con coordenadas (X, Y, 0)
        for (int i = 0, y = 0; y < imageHeight; y++)
        {
            for (int x = 0; x < imageWidth; x++, i++)
            {
                //vertices[i] = new Vector3(x * dist_factor, y * dist_factor, 0);
                vertices[i] = new Vector3(x * width_norm_coef, y * height_norm_coef, 0);
            }
        }
        mesh.vertices = vertices;
        Debug.Log("########### Generate - vertices OK");
        Debug.Log("########### Generate - vertices[0] " + vertices[0].x + " " + vertices[0].y + " " + vertices[0].z);
        Debug.Log("########### Generate - vertices[100] " + vertices[100].x + " " + vertices[100].y + " " + vertices[100].z);

        // Define los triángulos entre los vértices creados (mesh)
        int[] triangles = new int[imageWidth * imageHeight * 6];
        Debug.Log("########### Generate - creating triangles");
        for (int ti = 0, vi = 0, y = 0; y < imageHeight; y++, vi++)
        {
            for (int x = 0; x < imageWidth; x++, ti += 6, vi++)
            {
                triangles[ti] = vi;
                triangles[ti + 3] = triangles[ti + 2] = vi + 1;
                triangles[ti + 4] = triangles[ti + 1] = vi + imageWidth + 1;
                triangles[ti + 5] = vi + imageWidth + 2;
            }
        }
        Debug.Log("########### Generate - triangles OK");
        Debug.Log("########### Generate - triangles.Length " + triangles.Length);
        mesh.triangles = triangles;
        Debug.Log("########### Generate - mesh.triangles OK");
        mesh.RecalculateNormals();
        Debug.Log("########### End Generate");
    }

    private void UpdateImage()
    {
        Debug.Log("########### Init UpdateImage");
        vertices = mesh.vertices;

        // Asigna profundidad a los vertices según el mensaje recibido (2 bytes = medida de 1 punto en mm)
        for (int h = 0; h < imageHeight; h++)
        {
            for (int w = 0; w < imageWidth; w++)
            {
                int index = w + h * imageWidth;
                vertices[index] = new Vector3(w * width_norm_coef, h * height_norm_coef, BitConverter.ToUInt16(imageData, w * 2)/200);
                if (vertices[index].z < 0)
                    Debug.Log("Z = 0 en punto: " + index);
            }
        }
        /*
        for (int h = imageHeight; h > 0; h--)
        {
            for (int w = 0; w < imageWidth; w++)
            {
                int index = ((imageHeight - h) * imageWidth) + w;
                //vertices[index] = new Vector3(w * dist_factor, h * dist_factor, BitConverter.ToUInt16(imageData, w * 2) / 1000);
                vertices[index] = new Vector3(w * width_norm_coef, h * height_norm_coef, BitConverter.ToUInt16(imageData, w * 2) / 200);
            }
        }
        */
        
        mesh.vertices = vertices;
        Debug.Log("########### End UpdateImage");
        //GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        //sphere.transform.position = new Vector3(vertices[0].x, vertices[0].y + 1, vertices[0].z);
        Debug.Log("########### End UpdateImage - Sphere at " + vertices[0].x + " " + (vertices[0].y + 1) + " " + vertices[0].z);
    }
}
