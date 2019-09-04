using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ROS2;

public class RendererImageSensor : MonoBehaviour
{
    private Node node;
    ISubscription<sensor_msgs.msg.CompressedImage> image_sub;

    private GameObject quad;
    private MeshRenderer meshRenderer;
    private Texture2D texture2D;
    private byte[] imageData;

    public static bool startRendering;
    public static bool stopRendering;

    void Start()
    {
        Debug.Log("ImageSensorRenderer started");
        RCLdotnet.Init();
        node = RCLdotnet.CreateNode("image_listener");

        quad = GameObject.CreatePrimitive(PrimitiveType.Quad);
        quad.transform.position = new Vector3(-3, 0, 1);
        meshRenderer = quad.GetComponent<MeshRenderer>();
        quad.SetActive(false);

        image_sub = node.CreateSubscription<sensor_msgs.msg.CompressedImage>(
            "camera/rgb/image_raw/compressed", msg =>
            {
                Debug.Log("############## I saw a frame!");
                imageData = msg.Data.ToArray();
                texture2D.LoadImage(imageData);
                texture2D.Apply();
                meshRenderer.material.SetTexture("_MainTex", texture2D);
            }
        );
        
        texture2D = new Texture2D(1, 1);
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
}
