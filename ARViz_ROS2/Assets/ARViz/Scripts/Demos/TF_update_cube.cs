using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using HoloToolkit.Unity.InputModule;

using ROS2;
using ROS2.TF2;

public class TF_update_cube : MonoBehaviour
{
    INode tflt_node;
    TransformListener tflt_;

    void Start()
    {
        RCLdotnet.Init();
        tflt_node = RCLdotnet.CreateNode("tflt_cube_update");
        tflt_ = new TransformListener(ref tflt_node);
    }
    
    void Update()
    {
        if (RCLdotnet.Ok())
        {
            ROS2.TF2.Transform tf = tflt_.LookUpLastTransform("world", "base_link");
            
            //ROS2.TF2.Transform tf = tflt_.LookUpTransform("world", "base_link", RCLdotnet.Now());
            float xpos = (float)Math.Round(tf.Translation_x, 3, MidpointRounding.ToEven);
            //float ypos = 0;
            //float zpos = 0;
            float ypos = (float)Math.Round(tf.Translation_y, 3, MidpointRounding.ToEven);
            float zpos = (float)Math.Round(tf.Translation_z, 3, MidpointRounding.ToEven);

            //this.transform.position = new Vector3(xpos, ypos, zpos);

            //Debug.Log("############## TF_update_cube - W2B valid: " + tf.Valid);
            Debug.Log("############## TF_update_cube - W2B pos: " + xpos + " " + ypos + " " + zpos);
            RCLdotnet.SpinOnce(tflt_node, 200);
            //RCLdotnet.Spin(tflt_node);
        }
    }
}
