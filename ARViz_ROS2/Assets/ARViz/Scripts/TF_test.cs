using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ROS2;
using ROS2.TF2;

public class TF_test : MonoBehaviour
{
    public GameObject cube_test;

    public static Vector3 init_pos;
    public static Quaternion init_rot;
    public static bool init_info;

    bool updating;

    INode tfbr_node, tflt_node;
    TransformBroadcaster tfbr_;
    TransformListener tflt_;

    void Start()
    {
        RCLdotnet.Init();
        tfbr_node = RCLdotnet.CreateNode("tfbr_test");
        tflt_node = RCLdotnet.CreateNode("tflt_test");
        tfbr_ = new TransformBroadcaster(ref tfbr_node);
        tflt_ = new TransformListener(ref tflt_node);
        updating = false;
    }
    
    void Update()
    {
        if (RCLdotnet.Ok())
        {
            if (init_info == true)
            {
                init_info = false;
                Debug.Log("############## init_pos: " + init_pos);
                Debug.Log("############## init_rot: " + init_rot);
                InitializeTF(init_pos, init_rot);
            }
            if (updating == true)
            {
                UpdateTF();
            }
        }
    }

    void InitializeTF(Vector3 pos, Quaternion rot)
    {
        Instantiate(cube_test, pos, rot);
        geometry_msgs.msg.TransformStamped h2r = new geometry_msgs.msg.TransformStamped();
        h2r.Header.Frame_id = "hololens_camera";
        System.Tuple<int, uint> r_ts = RCLdotnet.Now();
        h2r.Header.Stamp.Sec = r_ts.Item1;
        h2r.Header.Stamp.Nanosec = r_ts.Item2;
        h2r.Child_frame_id = "robot";
        h2r.Transform.Translation.X = pos.x;
        h2r.Transform.Translation.Y = pos.y;
        h2r.Transform.Translation.Z = pos.z;
        h2r.Transform.Rotation.X = rot.x;
        h2r.Transform.Rotation.Y = rot.y;
        h2r.Transform.Rotation.Z = rot.z;
        h2r.Transform.Rotation.W = rot.w;
        tfbr_.SendTransform(ref h2r);
        updating = true;
    }

    void UpdateTF()
    {
        if (RCLdotnet.Ok())
        {
            RCLdotnet.SpinOnce(tflt_node, 500);
            ROS2.TF2.Transform tf = tflt_.LookUpLastTransform("world", "robot");

            geometry_msgs.msg.TransformStamped w2r = new geometry_msgs.msg.TransformStamped();
            w2r.Header.Frame_id = "world";
            System.Tuple<int, uint> r_ts2 = RCLdotnet.Now();
            w2r.Header.Stamp.Sec = r_ts2.Item1;
            w2r.Header.Stamp.Nanosec = r_ts2.Item2;
            w2r.Child_frame_id = "robot";
            w2r.Transform.Translation.X = tf.Translation_x + 0.01;
            w2r.Transform.Translation.Y = tf.Translation_y + 0.01;
            w2r.Transform.Translation.Z = tf.Translation_z + 0.01;
            w2r.Transform.Rotation.X = 1;
            w2r.Transform.Rotation.Y = 1;
            w2r.Transform.Rotation.Z = 1;
            w2r.Transform.Rotation.W = 0;
            tfbr_.SendTransform(ref w2r);
        }
        /*
        RCLdotnet.SpinOnce(tflt_node, 500);
        ROS2.TF2.Transform tf = tflt_.LookUpLastTransform("hololens_camera", "robot");
        Debug.Log("TF H2R: " + tf.Translation_x.ToString() + " " + tf.Translation_y.ToString() + " " + tf.Translation_z.ToString() + " " + tf.Rotation_x.ToString() + " " + tf.Rotation_y.ToString() + " " + tf.Rotation_z.ToString() + " " + tf.Rotation_w.ToString());
        ROS2.TF2.Transform tf2 = tflt_.LookUpLastTransform("world", "robot");
        Debug.Log("TF W2R: " + tf2.Translation_x.ToString() + " "+ tf2.Translation_y.ToString() + " " + tf2.Translation_z.ToString() + " " + tf2.Rotation_x.ToString() + " " + tf2.Rotation_y.ToString() + " " + tf2.Rotation_z.ToString() + " " + tf2.Rotation_w.ToString());
        
        Debug.Log("TF pos Y: " + tf.Translation_y);
        Debug.Log("TF pos Z: " + tf.Translation_z);
        Debug.Log("TF rot X: " + tf.Rotation_x);
        Debug.Log("TF rot Y: " + tf.Rotation_y);
        Debug.Log("TF rot Z: " + tf.Rotation_z);
        Debug.Log("TF rot W: " + tf.Rotation_w);
        */
    }
}
