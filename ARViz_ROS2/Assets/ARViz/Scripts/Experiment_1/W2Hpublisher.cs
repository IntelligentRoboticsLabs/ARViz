using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ROS2;
using ROS2.TF2;

public class W2Hpublisher : MonoBehaviour
{
    Node node;
    INode inode;
    TransformBroadcaster tfbr_;
    TransformListener tflt_;
    
    void Start()
    {
        RCLdotnet.Init();
        node = RCLdotnet.CreateNode("hololens2world_pos_publisher");
        tfbr_ = new TransformBroadcaster(ref inode);
        tflt_ = new TransformListener(ref inode);
    }
    
    void Update()
    {
        if (RCLdotnet.Ok())
        {
            //ROS2.TF2.Transform tf_W2H = tflt_.LookUpLastTransform("world", "hololens_camera");
            ROS2.TF2.Transform W2M = tflt_.LookUpLastTransform("world", "marker");
            ROS2.TF2.Transform M2HI = tflt_.LookUpLastTransform("marker", "hololens_camera_init");
            ROS2.TF2.Transform HI2H = tflt_.LookUpLastTransform("hololens_camera_init", "hololens_camera");
            
            // W2H = W2M * M2HI * HI2H
            ROS2.TF2.Transform W2HI = TF2dotnet.MultiplyTransform(W2M, M2HI);
            ROS2.TF2.Transform W2H = TF2dotnet.MultiplyTransform(W2HI, HI2H);

            geometry_msgs.msg.TransformStamped w2h = new geometry_msgs.msg.TransformStamped();
            w2h.Header.Frame_id = "world";
            System.Tuple<int, uint> ts = RCLdotnet.Now();
            w2h.Header.Stamp.Sec = ts.Item1;
            w2h.Header.Stamp.Nanosec = ts.Item2;
            w2h.Child_frame_id = "hololens_estimated";
            w2h.Transform.Translation.X = W2H.Translation_x;
            w2h.Transform.Translation.Y = W2H.Translation_y;
            w2h.Transform.Translation.Z = W2H.Translation_z;
            w2h.Transform.Rotation.X = W2H.Rotation_x;
            w2h.Transform.Rotation.Y = W2H.Rotation_y;
            w2h.Transform.Rotation.Z = W2H.Rotation_z;
            w2h.Transform.Rotation.W = W2H.Rotation_w;
            tfbr_.SendTransform(ref w2h);

            RCLdotnet.SpinOnce(node, 500);
        }
    }
}
