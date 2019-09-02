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

    Vector3 W2O;

    void Start()
    {
        RCLdotnet.Init();
        tfbr_node = RCLdotnet.CreateNode("tfbr_test");
        tflt_node = RCLdotnet.CreateNode("tflt_test");
        tfbr_ = new TransformBroadcaster(ref tfbr_node);
        tflt_ = new TransformListener(ref tflt_node);
        updating = false;
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.localScale = new Vector3(0.025f, 0.025f, 0.025f);
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
                PublishW2O();
            }
        }
    }

    void InitializeTF(Vector3 pos, Quaternion rot)
    {
        Instantiate(cube_test, pos, new Quaternion());
        /*
        Debug.Log("############## InitializeTF - H2O: " + pos.x + " " + pos.y + " " + pos.z + " " + rot.x + " " + rot.y + " " + rot.z + " " + rot.w);
        RCLdotnet.SpinOnce(tfbr_node, 500);
        ROS2.TF2.Transform tf = new ROS2.TF2.Transform();
        while (tf.Translation_x < 0.0001 && tf.Translation_x > -0.0001)
        {
            tf = tflt_.LookUpLastTransform("world", "hololens_camera");
            Debug.Log("############## Into while - W2H: " + tf.Translation_x);
            RCLdotnet.SpinOnce(tflt_node, 500);
        }
        Debug.Log("############## InitializeTF - W2H: " + tf.Translation_x + " " + tf.Translation_y + " " + tf.Translation_z);
        W2O = CrossProduct(pos.x, pos.y, pos.z, (float)tf.Translation_x, (float)tf.Translation_y, (float)tf.Translation_z);
        */
        W2O = new Vector3(pos.x, pos.y, pos.z);
        updating = true;
    }
    
    void PublishW2O()
    {
        if (RCLdotnet.Ok())
        {
            geometry_msgs.msg.TransformStamped w2o = new geometry_msgs.msg.TransformStamped();
            w2o.Header.Frame_id = "world";
            System.Tuple<int, uint> r_ts2 = RCLdotnet.Now();
            w2o.Header.Stamp.Sec = r_ts2.Item1;
            w2o.Header.Stamp.Nanosec = r_ts2.Item2;
            w2o.Child_frame_id = "odom";
            w2o.Transform.Translation.X = W2O.x;
            w2o.Transform.Translation.Y = W2O.y;
            w2o.Transform.Translation.Z = W2O.z;
            w2o.Transform.Rotation.X = 0;
            w2o.Transform.Rotation.Y = 0;
            w2o.Transform.Rotation.Z = 0;
            w2o.Transform.Rotation.W = 1;
            tfbr_.SendTransform(ref w2o);
            RCLdotnet.SpinOnce(tfbr_node, 200);
            Debug.Log("############## TF_Test - PublishW2O: " + W2O + " at " + r_ts2.Item1 + "." + r_ts2.Item2);
            //RCLdotnet.Spin(tfbr_node);
        }
    }

    /*
    Vector3 CrossProduct(float u1, float u2, float u3, float v1, float v2, float v3)
    {
        float uvi, uvj, uvk;
        uvi = u2 * v3 - v2 * u3;
        uvj = v1 * u3 - u1 * v3;
        uvk = u1 * v2 - v1 * u2;
        Vector3 result = new Vector3(uvi, uvj, uvk);
        Debug.Log("############## CrossProduct " + uvi + " " + uvj + " " + uvk);
        return result;
    }
    */
}
