using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ROS2;
using ROS2.TF2;

public class TF_demo_turtle_follower : MonoBehaviour
{
    INode turtle_tflt_node;
    TransformListener turtle_tflt_;

    void Start()
    {
        RCLdotnet.Init();
        turtle_tflt_node = RCLdotnet.CreateNode("tf_demo_turtle_tflt");
        turtle_tflt_ = new TransformListener(ref turtle_tflt_node);
    }

    void Update()
    {
        if (RCLdotnet.Ok())
        {
            System.Tuple<int, uint> r_ts2 = RCLdotnet.Now();
            System.Tuple<int, uint> r_ts2_past = new System.Tuple<int, uint>(r_ts2.Item1 - 1, r_ts2.Item2);
            //ROS2.TF2.Transform tf = capsule_tflt_.LookUpLastTransform("world", "cube");
            ROS2.TF2.Transform tf = turtle_tflt_.LookUpTransform("world", "turtle_1", r_ts2_past);
            this.transform.position = new Vector3((float)tf.Translation_x + 0.3f, (float)tf.Translation_y, (float)tf.Translation_z);
            RCLdotnet.SpinOnce(turtle_tflt_node, 1000);
        }
    }
}