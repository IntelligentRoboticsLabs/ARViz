using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ROS2;
using ROS2.TF2;

public class RobotPositionPublisher : MonoBehaviour
{
    INode node;
    TransformBroadcaster tfbr_;
    TransformListener tflt_;

    void Start()
    {
        RCLdotnet.Init();
        node = RCLdotnet.CreateNode("robot_pos_publisher");
        tfbr_ = new TransformBroadcaster(ref node);
        tflt_ = new TransformListener(ref node);
    }
    
    void Update()
    {
        if (RCLdotnet.Ok())
        {
            // Publish World2Odom (both are at the same position)
            geometry_msgs.msg.TransformStamped w2o = new geometry_msgs.msg.TransformStamped();
            w2o.Header.Frame_id = "world";
            System.Tuple<int, uint> ts = RCLdotnet.Now();
            w2o.Header.Stamp.Sec = ts.Item1;
            w2o.Header.Stamp.Nanosec = ts.Item2;
            w2o.Child_frame_id = "odom";
            w2o.Transform.Translation.X = 0;
            w2o.Transform.Translation.Y = 0;
            w2o.Transform.Translation.Z = 0;
            w2o.Transform.Rotation.X = 0;
            w2o.Transform.Rotation.Y = 0;
            w2o.Transform.Rotation.Z = 0;
            w2o.Transform.Rotation.W = 1;
            tfbr_.SendTransform(ref w2o);

            RCLdotnet.SpinOnce(node, 500);
            //RCLdotnet.Spin(node);
            ROS2.TF2.Transform W2HI = tflt_.LookUpLastTransform("world", "hololens_camera_init");
            ROS2.TF2.Transform O2BF = tflt_.LookUpLastTransform("odom", "base_footprint");
            ROS2.TF2.Transform W2HI_1 = TF2dotnet.InverseTransform(W2HI);
            // W2HI_1 * W2BF = HI2BF
            // HI2W   * W2BF = HI2BF 
            ROS2.TF2.Transform HI2BF = TF2dotnet.MultiplyTransform(W2HI_1, O2BF);
            //Debug.Log("########## BaseFootprintEstimated2HololensInit " + (float)BF2HI.Translation_x + " " + (float)BF2HI.Translation_y + " " + (float)BF2HI.Translation_z);
            LaserVisualizer_test.origin = new Vector3((float)HI2BF.Translation_x, (float)HI2BF.Translation_y, (float)HI2BF.Translation_z);
            
            geometry_msgs.msg.TransformStamped h2bf = new geometry_msgs.msg.TransformStamped();
            h2bf.Header.Frame_id = "hololens_camera_init";
            System.Tuple<int, uint> ts2 = RCLdotnet.Now();
            h2bf.Header.Stamp.Sec = ts2.Item1;
            h2bf.Header.Stamp.Nanosec = ts2.Item2;
            h2bf.Child_frame_id = "base_footprint_estimated";
            h2bf.Transform.Translation.X = HI2BF.Translation_x;
            h2bf.Transform.Translation.Y = HI2BF.Translation_y;
            h2bf.Transform.Translation.Z = HI2BF.Translation_z;
            h2bf.Transform.Rotation.X = 0;
            h2bf.Transform.Rotation.Y = 0;
            h2bf.Transform.Rotation.Z = 0;
            h2bf.Transform.Rotation.W = 1;
            tfbr_.SendTransform(ref h2bf);
        }
    }
}
