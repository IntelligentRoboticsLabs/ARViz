using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ROS2;

public class DemoImageManager : MonoBehaviour
{
    private Node node;
    ISubscription<sensor_msgs.msg.CompressedImage> image_sub;

    public MeshRenderer meshRenderer;
    private Texture2D texture2D;
    private byte[] imageData;

    void Start()
    {
        RCLdotnet.Init();
        node = RCLdotnet.CreateNode("image_listener");
        /*
        image_sub = node.CreateSubscription<sensor_msgs.msg.CompressedImage>(
            "camera/rgb/image_raw/compressed", msg =>
            {
                Debug.Log("############## I saw a frame!");
                imageData = msg.Data.ToArray();
                ProcessMessage();
            }
        );
        */
        image_sub = node.CreateSubscription<sensor_msgs.msg.CompressedImage>(
            "camera/depth/image_raw/compressed", msg =>
            {
                Debug.Log("############## I saw a frame!");
                imageData = msg.Data.ToArray();
                ProcessMessage();
            }
        );
        

        texture2D = new Texture2D(1, 1);
        meshRenderer.material = new Material(Shader.Find("Standard"));
    }
    
    void Update()
    {
        RCLdotnet.SpinOnce(node, 500);
    }

    private void ProcessMessage()
    {
        texture2D.LoadImage(imageData);
        texture2D.Apply();
        meshRenderer.material.SetTexture("_MainTex", texture2D);
    }
}
