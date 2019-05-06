using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ROS2;
using ROS2.TF2;

public class W2Hvisualizer : MonoBehaviour
{
    INode node;
    TransformListener tflt_;
    std_msgs.msg.String msg;
    ROS2.TF2.Transform tf;

    void Start()
    {
        //Debug.Log("************ (00) ************");
        RCLdotnet.Init();
        //Debug.Log("************ (01) ************");
        //TF2dotnet.Init();
        //Debug.Log("************ (02) ************");
        node = RCLdotnet.CreateNode("camera_pos_listener");
        tf = new ROS2.TF2.Transform();
        //Debug.Log("************ (1) ************");
        tflt_ = new TransformListener(ref node);
        //Debug.Log("************ (2) ************");
        if (tflt_ == null)
        {
            Debug.Log("************ (3) ************");
            Debug.Log("tflt_ SI es null :(");
        }
        else
        {
            Debug.Log("************ (4) ************");
            Debug.Log("tflt_ NO es null :)");
        }
        //Debug.Log("************ (5) ************");
    }

    void Update()
    {
        if (RCLdotnet.Ok())
        {
            Debug.Log("RCLdotnet.Ok");
            System.Tuple<int, uint> ts = RCLdotnet.Now();
            try
            {
                //tf = tflt_.LookUpLastTransform("/world", "/hololens_camera");
                tflt_.LookUpTransform("world", "hololens_camera", ts);
            }
            catch (Exception e)
            {
                Debug.Log("Catch: world, hololens_camera");
                Debug.LogException(e, this);
            }
            try
            {
                //tf = tflt_.LookUpLastTransform("/hololens_camera", "/world");
                tflt_.LookUpTransform("hololens_camera", "world", ts);
            }
            catch (Exception e)
            {
                Debug.Log("Catch: hololens_camera, world");
                Debug.LogException(e, this);
            }
        }
    }
}
