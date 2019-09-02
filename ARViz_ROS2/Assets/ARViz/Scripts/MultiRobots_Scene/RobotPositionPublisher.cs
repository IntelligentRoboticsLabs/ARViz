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
            geometry_msgs.msg.TransformStamped w2o1 = new geometry_msgs.msg.TransformStamped();
            w2o1.Header.Frame_id = "world";
            System.Tuple<int, uint> ts = RCLdotnet.Now();
            w2o1.Header.Stamp.Sec = ts.Item1;
            w2o1.Header.Stamp.Nanosec = ts.Item2;
            w2o1.Child_frame_id = "robot1/odom";
            w2o1.Transform.Translation.X = 0;
            w2o1.Transform.Translation.Y = 0;
            w2o1.Transform.Translation.Z = 0;
            w2o1.Transform.Rotation.X = 0;
            w2o1.Transform.Rotation.Y = 0;
            w2o1.Transform.Rotation.Z = 0;
            w2o1.Transform.Rotation.W = 1;
            tfbr_.SendTransform(ref w2o1);

            geometry_msgs.msg.TransformStamped w2o2 = new geometry_msgs.msg.TransformStamped();
            w2o2.Header.Frame_id = "world";
            System.Tuple<int, uint> ts2 = RCLdotnet.Now();
            w2o2.Header.Stamp.Sec = ts2.Item1;
            w2o2.Header.Stamp.Nanosec = ts2.Item2;
            w2o2.Child_frame_id = "robot2/odom";
            w2o2.Transform.Translation.X = 3;
            w2o2.Transform.Translation.Y = 0;
            w2o2.Transform.Translation.Z = 0;
            w2o2.Transform.Rotation.X = 0;
            w2o2.Transform.Rotation.Y = 0;
            w2o2.Transform.Rotation.Z = 0;
            w2o2.Transform.Rotation.W = 1;
            tfbr_.SendTransform(ref w2o2);

            RCLdotnet.SpinOnce(node, 500);
            //RCLdotnet.Spin(node);
            ROS2.TF2.Transform W2HI = tflt_.LookUpLastTransform("world", "hololens_camera_init");
            ROS2.TF2.Transform O12BF1 = tflt_.LookUpLastTransform("robot1/odom", "robot1/base_footprint");
            ROS2.TF2.Transform O22BF2 = tflt_.LookUpLastTransform("robot2/odom", "robot2/base_footprint");
            ROS2.TF2.Transform W2HI_1 = TF2dotnet.InverseTransform(W2HI);
            // W2HI_1 * W2BF = HI2BF
            // HI2W   * W2BF = HI2BF 
            ROS2.TF2.Transform HI2BF1 = TF2dotnet.MultiplyTransform(W2HI_1, O12BF1);
            ROS2.TF2.Transform HI2BF2 = TF2dotnet.MultiplyTransform(W2HI_1, O22BF2);
            //Debug.Log("########## BaseFootprintEstimated2HololensInit " + (float)BF2HI.Translation_x + " " + (float)BF2HI.Translation_y + " " + (float)BF2HI.Translation_z);
            //LaserVisualizer_test.origin = new Vector3((float)HI2BF.Translation_x, (float)HI2BF.Translation_y, (float)HI2BF.Translation_z);
            RobotPositionVisualization.origin_1 = new Vector3((float)HI2BF1.Translation_x, (float)HI2BF1.Translation_y, (float)HI2BF1.Translation_z);
            RobotPositionVisualization.origin_2 = new Vector3((float)HI2BF2.Translation_x, (float)HI2BF2.Translation_y, (float)HI2BF2.Translation_z);


            geometry_msgs.msg.TransformStamped h2bf1 = new geometry_msgs.msg.TransformStamped();
            h2bf1.Header.Frame_id = "hololens_camera_init";
            System.Tuple<int, uint> ts3 = RCLdotnet.Now();
            h2bf1.Header.Stamp.Sec = ts3.Item1;
            h2bf1.Header.Stamp.Nanosec = ts3.Item2;
            h2bf1.Child_frame_id = "robot1/base_footprint_estimated";
            h2bf1.Transform.Translation.X = HI2BF1.Translation_x;
            h2bf1.Transform.Translation.Y = HI2BF1.Translation_y;
            h2bf1.Transform.Translation.Z = HI2BF1.Translation_z;
            h2bf1.Transform.Rotation.X = 0;
            h2bf1.Transform.Rotation.Y = 0;
            h2bf1.Transform.Rotation.Z = 0;
            h2bf1.Transform.Rotation.W = 1;
            tfbr_.SendTransform(ref h2bf1);

            geometry_msgs.msg.TransformStamped h2bf2 = new geometry_msgs.msg.TransformStamped();
            h2bf2.Header.Frame_id = "hololens_camera_init";
            System.Tuple<int, uint> ts4 = RCLdotnet.Now();
            h2bf2.Header.Stamp.Sec = ts4.Item1;
            h2bf2.Header.Stamp.Nanosec = ts4.Item2;
            h2bf2.Child_frame_id = "robot2/base_footprint_estimated";
            h2bf2.Transform.Translation.X = HI2BF2.Translation_x;
            h2bf2.Transform.Translation.Y = HI2BF2.Translation_y;
            h2bf2.Transform.Translation.Z = HI2BF2.Translation_z;
            h2bf2.Transform.Rotation.X = 0;
            h2bf2.Transform.Rotation.Y = 0;
            h2bf2.Transform.Rotation.Z = 0;
            h2bf2.Transform.Rotation.W = 1;
            tfbr_.SendTransform(ref h2bf2);
        }
    }
}
