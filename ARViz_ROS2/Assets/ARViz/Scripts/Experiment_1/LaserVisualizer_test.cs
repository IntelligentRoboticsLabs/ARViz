using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

using ROS2;

public class LaserVisualizer_test : MonoBehaviour
{
    private INode node;
    ISubscription<sensor_msgs.msg.LaserScan> laser_sub;
    ISubscription<std_msgs.msg.String> string_sub;
    private bool IsCreated = false;
    // origin set from robot TF
    public static Vector3 origin;
    System.Tuple<int, uint> msg_ts = RCLdotnet.Now();
    public static bool init_visualization;
    GameObject robot;

    void Start()
    {
        RCLdotnet.Init();
        node = RCLdotnet.CreateNode("laser_listener");

        //laser_sub = node.CreateSubscription<sensor_msgs.msg.LaserScan>(
        string_sub = node.CreateSubscription<std_msgs.msg.String>(
            "chatter", msg =>
            {
                float time_diff = (RCLdotnet.Now().Item1 - msg_ts.Item1) + Mathf.Pow((RCLdotnet.Now().Item2 - msg_ts.Item2), -9);
                if (time_diff > 0.5)
                {
                    Debug.Log("############## Time from last msg: " + (RCLdotnet.Now().Item1 - msg_ts.Item1) + "." + (RCLdotnet.Now().Item2 - msg_ts.Item2));
                    msg_ts = RCLdotnet.Now();
                    //Debug.Log("############## Laser frame_ID " + msg.Header.Frame_id + " Angle_min " + msg.Angle_min + " Angle_max " + msg.Angle_max + " Angle_increment " + msg.Angle_increment);
                    //Debug.Log("############## Time_increment " + msg.Time_increment + " Scan_time " + msg.Scan_time + " Range_min " + msg.Range_min + " Range_max " + msg.Range_max + " Ranges_count " + msg.Ranges.Count + " Intensities_count " + msg.Intensities.Count);

                    // init_visualization=TRUE from MarkerDetectionHololens
                    if (init_visualization)
                    {
                        init_visualization = false;
                        Visualize();
                    }
                }
            }
        );

        // Invokes the method Spinning every 0.5 second
        InvokeRepeating("Spinning", 0, 0.5f);
    }
    
    void Update()
    {
        //RCLdotnet.SpinOnce(node, 500);
    }

    void Spinning()
    {
        RCLdotnet.SpinOnce(node, 100);
    }

    private void Create()
    {
        Debug.Log("############## Creating INIT");
        robot = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        robot.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        IsCreated = true;
        Debug.Log("############## Creating END");
    }

    protected void Visualize()
    {
        if (!IsCreated)
            Create();


        robot.transform.position = new Vector3(-origin.y, origin.z, origin.x);
        Debug.Log("############## robot.transform.position " + robot.transform.position.x + ", " + robot.transform.position.y + ", " + robot.transform.position.z);
        init_visualization = true;
    }
}
