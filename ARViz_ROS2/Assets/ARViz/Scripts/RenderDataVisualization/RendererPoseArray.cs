using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ROS2;

public class RendererPoseArray : MonoBehaviour
{
    private Node node;
    ISubscription<geometry_msgs.msg.PoseArray> posearray_sub;
    public static bool startRendering;
    public static bool stopRendering;
    GameObject poseArrayGroup;
    public GameObject poseArrow;

    public string topic = "particlecloud";

    void Start()
    {
        RCLdotnet.Init();
        node = RCLdotnet.CreateNode("posearray_listener");
        poseArrayGroup = new GameObject("PoseArrayGroup");
        poseArrayGroup.transform.position = new Vector3(-3, -1.5f, -7); // z: -3
        poseArrayGroup.SetActive(false);
        posearray_sub = node.CreateSubscription<geometry_msgs.msg.PoseArray>(
            topic, msg =>
            {
                Debug.Log("############## PoseArray length: " + msg.Poses.Count);
                for (int i = 0; i < msg.Poses.Count/4; i++)
                {
                    GameObject pose_arrow = Instantiate(poseArrow,
                                                        new Vector3((float)msg.Poses[i].Position.Y - 3, -1.5f, (float)msg.Poses[i].Position.X + 2),
                                                        new Quaternion((float)msg.Poses[i].Orientation.X, (float)msg.Poses[i].Orientation.Y, (float)msg.Poses[i].Orientation.Z, (float)msg.Poses[i].Orientation.Z));
                                                        //poseArrow.transform.rotation);
                    pose_arrow.transform.parent = poseArrayGroup.transform;
                    //Debug.Log("############## PoseArray position: " + msg.Poses[i].Position.X * 0.5f + ", -1, " + msg.Poses[i].Position.Y * 0.5f);// + msg.Poses[i].Position.Y + " " + msg.Poses[i].Position.Z);
                    pose_arrow.transform.rotation = Quaternion.Euler(90, 90, 0);
                }
                Debug.Log("############## PoseArray RENDERED! ");
            }
        );
        //startRendering = true;
        startRendering = false;
        stopRendering = false;
    }

    void Update()
    {
        if (startRendering)
        {
            poseArrayGroup.SetActive(true);
            InvokeRepeating("Spinning", 0, 0.5f);
            startRendering = false;
        }
        if (stopRendering)
        {
            poseArrayGroup.SetActive(false);
            CancelInvoke("Spinning");
            //DestroyRender();
            stopRendering = false;
        }
    }

    void Spinning()
    {
        RCLdotnet.SpinOnce(node, 100);
    }

    void Create()
    {
        poseArrayGroup = new GameObject("PoseArrayGroup");
    }

    protected void DestroyRender()
    {
        Destroy(poseArrayGroup);
    }
}
