using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ROS2;
using System;

public class RendererMap : MonoBehaviour
{
    private Node node;
    ISubscription<nav_msgs.msg.OccupancyGrid> map_sub;

    private GameObject quad;
    private byte[] imageData;
    private sbyte[] imageDataSB;
    private Texture2D texture2D;
    private MeshRenderer meshRenderer;

    public static bool startRendering;
    public static bool stopRendering;

    void Start()
    {
        RCLdotnet.Init();
        node = RCLdotnet.CreateNode("map_listener");

        quad = GameObject.CreatePrimitive(PrimitiveType.Quad);
        quad.transform.position = new Vector3(0, -1, 3);
        quad.transform.rotation = Quaternion.Euler(90, 90, 180);
        meshRenderer = quad.GetComponent<MeshRenderer>();
        quad.SetActive(false);

        map_sub = node.CreateSubscription<nav_msgs.msg.OccupancyGrid>(
            "map", msg =>
            {
                Debug.Log("############## I saw a map!");
                //imageDataSB = new sbyte[msg.Info.Width * msg.Info.Height];
                imageDataSB = msg.Data.ToArray();
                
                for (int y = 0; y < texture2D.height; y++)
                {
                    for (int x = 0; x < texture2D.width; x++)
                    {
                        Color color = SetPixelMapColor(texture2D.height * y + x);
                        texture2D.SetPixel(x, y, color);
                    }
                }
                texture2D.Apply();
                meshRenderer.material.SetTexture("_MainTex", texture2D);
                quad.transform.localScale = new Vector3(10, 10, 10);
                
                //CancelInvoke("Spinning");
                Debug.Log("############## I saw a map and rendered it!");
            }
        );
        texture2D = new Texture2D(250, 250, TextureFormat.RGBA32, false);
        meshRenderer.material = new Material(Shader.Find("Standard"));
        startRendering = false;
        stopRendering = false;
    }

    void Update()
    {
        if (startRendering)
        {
            quad.SetActive(true);
            InvokeRepeating("Spinning", 0, 0.5f);
            startRendering = false;
        }
        if (stopRendering)
        {
            quad.SetActive(false);
            CancelInvoke("Spinning");
            stopRendering = false;
        }
    }

    void Spinning()
    {
        RCLdotnet.SpinOnce(node, 100);
    }

    Color SetPixelMapColor(int i)
    {
        
        switch (imageDataSB[i])
        {
            case 100:
                Debug.Log("############## SetPixelMapColor - imageDataSB[" + i + "] = " + imageDataSB[i]);
                return Color.black;
            case 0:
                return Color.white;
            default:
                return new Color(0.7f, 0.7f, 0.7f, 0);
        }
    }
}
