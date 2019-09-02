using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ROS2;
using ROS2.TF2;

public class TF_demo_turtle_leader : MonoBehaviour
{
    INode turtle_tfbr_node;
    TransformBroadcaster turtle_tfbr_;

    void Start()
    {
        RCLdotnet.Init();
        turtle_tfbr_node = RCLdotnet.CreateNode("tf_demo_turtle_tfbr");
        turtle_tfbr_ = new TransformBroadcaster(ref turtle_tfbr_node);
    }
    
    void Update()
    {
        if (RCLdotnet.Ok())
        {
            geometry_msgs.msg.TransformStamped w2c = new geometry_msgs.msg.TransformStamped();
            w2c.Header.Frame_id = "world";
            System.Tuple<int, uint> r_ts2 = RCLdotnet.Now();
            w2c.Header.Stamp.Sec = r_ts2.Item1;
            w2c.Header.Stamp.Nanosec = r_ts2.Item2;
            w2c.Child_frame_id = "turtle_1";
            w2c.Transform.Translation.X = transform.position.x;
            w2c.Transform.Translation.Y = transform.position.y;
            w2c.Transform.Translation.Z = transform.position.z;
            w2c.Transform.Rotation.X = 0;
            w2c.Transform.Rotation.Y = 0;
            w2c.Transform.Rotation.Z = 0;
            w2c.Transform.Rotation.W = 1;
            turtle_tfbr_.SendTransform(ref w2c);
            RCLdotnet.SpinOnce(turtle_tfbr_node, 200);
        }
    }
}
