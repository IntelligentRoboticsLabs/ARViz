using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ROS2;
using ROS2.TF2;

public class HololensPositionPublisher : MonoBehaviour
{
    INode node;
    TransformBroadcaster tfbr_;
    TransformListener tflt_;
    std_msgs.msg.String msg;

    public static Vector3 init_pos;
    public static Quaternion init_rot;
    public static bool init_info;
    bool corrected = false;

    void Start()
    {
        RCLdotnet.Init();
        node = RCLdotnet.CreateNode("hololens_pos_publisher");
        tfbr_ = new TransformBroadcaster(ref node);
        tflt_ = new TransformListener(ref node);

        // Invokes the method PublishHololensInit2Marker every second
        InvokeRepeating("PublishHololensInit2Marker", 0, 1f);
    }

    void PublishHololensInit2Marker()
    // void Update()
    {
        if (RCLdotnet.Ok())
        {
            if (init_info == true)
            {
                //Debug.Log("########## HololensPositionPublisher init_info == true");
                geometry_msgs.msg.TransformStamped hi2m = new geometry_msgs.msg.TransformStamped();
                //Debug.Log("########## HololensPositionPublisher 111");
                hi2m.Header.Frame_id = "marker";
                //Debug.Log("########## HololensPositionPublisher 222");
                System.Tuple<int, uint> ts = RCLdotnet.Now();
                //Debug.Log("########## HololensPositionPublisher 333");
                hi2m.Header.Stamp.Sec = ts.Item1;
                hi2m.Header.Stamp.Nanosec = ts.Item2;
                hi2m.Child_frame_id = "hololens_camera_init";
                //Debug.Log("########## HololensPositionPublisher 444");
                hi2m.Transform.Translation.X = -init_pos.z;
                hi2m.Transform.Translation.Y = -init_pos.x;
                hi2m.Transform.Translation.Z = -init_pos.y;
                hi2m.Transform.Rotation.X = 0;
                hi2m.Transform.Rotation.Y = 0;
                hi2m.Transform.Rotation.Z = 0;
                hi2m.Transform.Rotation.W = 1;
                //Debug.Log("########## HololensPositionPublisher 444");
                tfbr_.SendTransform(ref hi2m);
                //Debug.Log("########## HololensPositionPublisher SendTransform");
            }
        }
        Debug.Log("########## HololensPositionPublisher update");
    }

    /*
    // Aplica matriz de rotación para corregir posicion
    Vector3 correctedPose(Vector3 pose, int correct)
    {
        float correct_rad = Mathf.PI * correct / 180.0f;;
        Vector3 pose_corrected = new Vector3();
        float[,] rotationMatrix = new float[3, 3] { { Mathf.Cos(correct_rad), 0, Mathf.Sin(correct_rad) }, { 0, 1, 0 }, { (-1)*Mathf.Sin(correct_rad), 0 , Mathf.Cos(correct_rad) }};
        
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                Debug.Log("pose_corrected[" + i + "]+=pose[" + j + "]*rotationMatrix[" + i + "," + j + "] --- " + pose_corrected[i] + " += " + pose[j] + " * " + rotationMatrix[i, j]);
                pose_corrected[i] += pose[j] * rotationMatrix[j,i];
            }
        }

        Debug.Log("#*#*#*#*#*#*#*#* correctedPose - pose " + pose);
        Debug.Log("#*#*#*#*#*#*#*#* correctedPose - pose_corrected " + pose_corrected);

        return pose_corrected;
    }
    */
}
