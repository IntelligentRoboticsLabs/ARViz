using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ROS2;
using ROS2.TF2;

public class FakeKobuki : MonoBehaviour
{
    INode kobuki_tfbr_node;
    TransformBroadcaster kobuki_tfbr_;
    int sec;
    float moveX;

    void Start()
    {
        RCLdotnet.Init();
        kobuki_tfbr_node = RCLdotnet.CreateNode("kobuki_fake_tfbr");
        kobuki_tfbr_ = new TransformBroadcaster(ref kobuki_tfbr_node);
        sec = 0;
        moveX = 0;
    }

    void Update()
    {
        if (RCLdotnet.Ok())
        {
            geometry_msgs.msg.TransformStamped w2o = new geometry_msgs.msg.TransformStamped();
            w2o.Header.Frame_id = "odom";
            System.Tuple<int, uint> r_ts2 = RCLdotnet.Now();
            w2o.Header.Stamp.Sec = r_ts2.Item1;
            w2o.Header.Stamp.Nanosec = r_ts2.Item2;
            w2o.Child_frame_id = "base_link";
            w2o.Transform.Translation.X = moveX;
            w2o.Transform.Translation.Y = 0;
            w2o.Transform.Translation.Z = 0;
            w2o.Transform.Rotation.X = 0;
            w2o.Transform.Rotation.Y = 0;
            w2o.Transform.Rotation.Z = 0;
            w2o.Transform.Rotation.W = 1;
            kobuki_tfbr_.SendTransform(ref w2o);
            RCLdotnet.SpinOnce(kobuki_tfbr_node, 200);
            if (r_ts2.Item1 != sec)
            {
                sec = r_ts2.Item1;
                moveX+= 0.05f;
            }
        }
    }
}
