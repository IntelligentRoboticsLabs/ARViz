using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ROS2;
using ROS2.TF2;

using HoloToolkit.Unity.InputModule;

public class TF_test_touch : MonoBehaviour, IInputClickHandler, IInputHandler
{
    //INode tfbr_node;
    INode tflt_node;
    //TransformBroadcaster tfbr_;
    TransformListener tflt_;

    void Start()
    {
        RCLdotnet.Init();
        //tfbr_node = RCLdotnet.CreateNode("tfbr_test");
        tflt_node = RCLdotnet.CreateNode("tflt_test");
        //tfbr_ = new TransformBroadcaster(ref tfbr_node);
        tflt_ = new TransformListener(ref tflt_node);
    }

    void Update()
    {
        if (RCLdotnet.Ok())
        {
            RCLdotnet.SpinOnce(tflt_node, 500);
            ROS2.TF2.Transform tf = tflt_.LookUpLastTransform("world", "robot");
            this.transform.position = new Vector3((float)tf.Translation_x, (float)tf.Translation_y, (float)tf.Translation_z);
        }
    }

    public void OnInputClicked(InputClickedEventData eventData)
    {
        /*
        Debug.Log("############### TOCADO !!!!!!!!!");
        if (RCLdotnet.Ok())
        {
            RCLdotnet.SpinOnce(tflt_node, 500);
            ROS2.TF2.Transform tf = tflt_.LookUpLastTransform("world", "robot");

            geometry_msgs.msg.TransformStamped w2r = new geometry_msgs.msg.TransformStamped();
            w2r.Header.Frame_id = "world";
            System.Tuple<int, uint> r_ts = RCLdotnet.Now();
            w2r.Header.Stamp.Sec = r_ts.Item1;
            w2r.Header.Stamp.Nanosec = r_ts.Item2;
            w2r.Child_frame_id = "robot";
            w2r.Transform.Translation.X = tf.Translation_x + 0.2;
            w2r.Transform.Translation.Y = tf.Translation_y + 0.2;
            w2r.Transform.Translation.Z = tf.Translation_z + 0.2;
            w2r.Transform.Rotation.X = 1;
            w2r.Transform.Rotation.Y = 1;
            w2r.Transform.Rotation.Z = 1;
            w2r.Transform.Rotation.W = 0;
            tfbr_.SendTransform(ref w2r);

            this.transform.position = new Vector3((float)tf.Translation_x + 0.2f, (float)tf.Translation_y + 0.2f, (float)tf.Translation_z + 0.2f);
        }
        */
    }

    public void OnInputDown(InputEventData eventData){ }

    public void OnInputUp(InputEventData eventData){ }
}
