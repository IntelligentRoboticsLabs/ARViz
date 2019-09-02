using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ROS2;
using ROS2.TF2;

public class HololensInitPositionPublisher : MonoBehaviour
{
    INode node;
    //TransformBroadcaster tfbr_;
    StaticTransformBroadcaster tfbr_;
    TransformListener tflt_;
    
    public static bool init_info;
    bool is_W2HI;
    ROS2.TF2.Transform W2HI;

    void Start()
    {
        RCLdotnet.Init();
        node = RCLdotnet.CreateNode("hololens_init_pos_publisher");
        //tfbr_ = new TransformBroadcaster(ref node);
        tfbr_ = new StaticTransformBroadcaster(ref node);
        tflt_ = new TransformListener(ref node);
        init_info = false;
        is_W2HI = false;

        // Invokes the method PublishHololensInit2World every second
        InvokeRepeating("PublishHololensInit2World", 0, 0.1f);
    }

    void PublishHololensInit2World()
    //void Update()
    {
        if (RCLdotnet.Ok())
        {
            if (init_info == true)
            {
                Debug.Log("########## HololensInitPositionPublisher init_info == true");
                if (is_W2HI == true)
                {
                    //init_info = false;
                    geometry_msgs.msg.TransformStamped w2hi = new geometry_msgs.msg.TransformStamped();

                    w2hi.Header.Frame_id = "world";

                    System.Tuple<int, uint> ts = RCLdotnet.Now();

                    w2hi.Header.Stamp.Sec = ts.Item1;
                    w2hi.Header.Stamp.Nanosec = ts.Item2;
                    w2hi.Child_frame_id = "hololens_camera_init";

                    w2hi.Transform.Translation.X = W2HI.Translation_x;
                    w2hi.Transform.Translation.Y = W2HI.Translation_y;
                    w2hi.Transform.Translation.Z = W2HI.Translation_z;
                    w2hi.Transform.Rotation.X = W2HI.Rotation_x;
                    w2hi.Transform.Rotation.Y = W2HI.Rotation_y;
                    w2hi.Transform.Rotation.Z = W2HI.Rotation_z;
                    w2hi.Transform.Rotation.W = W2HI.Rotation_w;

                    tfbr_.SendTransform(ref w2hi);
                    Debug.Log("########## HololensInitPositionPublisher END");
                    RCLdotnet.SpinOnce(node, 500);
                }
                else
                {
                    getTransformTree();
                }
            }
        }
    }

    void getTransformTree()
    {
        Debug.Log("########## HololensInitPositionPublisher init getTransformTree");
        RCLdotnet.SpinOnce(node, 500);
        ROS2.TF2.Transform W2M = tflt_.LookUpLastTransform("world", "marker");
        ROS2.TF2.Transform M2HI = tflt_.LookUpLastTransform("marker", "hololens_camera_init");
        W2HI = TF2dotnet.MultiplyTransform(W2M, M2HI);
        Debug.Log("############## Hololens Init: " + W2HI.Translation_x + " " + W2HI.Translation_y + " " + W2HI.Translation_z);
        if (W2HI.Translation_x > 0.01 && W2HI.Translation_x > 0.01 && W2HI.Translation_z < 0.01)
        {
            is_W2HI = true;
            Debug.Log("########## HololensInitPositionPublisher end getTransformTree");
        }
    }
}
