using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ROS2;

public class LaserVisualizer : MonoBehaviour
{
    private Node node;
    ISubscription<sensor_msgs.msg.LaserScan> laser_sub;

    private GameObject[] LaserScan;
    private bool IsCreated = false;

    int samples;
    float angle_min;
    float angle_max;
    float angle_increment;
    float scan_time;
    float range_min;
    float range_max;
    float[] ranges;
    float[] intensities;
    // origin set from robot TF
    public static Vector3 origin;
    float objectWidth = 0.01f;

    System.Tuple<int, uint> msg_ts = RCLdotnet.Now();

    public static bool init_visualization;

    void Start()
    {
        RCLdotnet.Init();
        node = RCLdotnet.CreateNode("laser_listener");
        laser_sub = node.CreateSubscription<sensor_msgs.msg.LaserScan>(
            "scan", msg =>
            {
                float time_diff = (RCLdotnet.Now().Item1 - msg_ts.Item1) + Mathf.Pow((RCLdotnet.Now().Item2 - msg_ts.Item2), -9);
                if (time_diff > 1.5)
                {
                    Debug.Log("############## Time from last msg: " + (RCLdotnet.Now().Item1 - msg_ts.Item1) + "." + (RCLdotnet.Now().Item2 - msg_ts.Item2));
                    msg_ts = RCLdotnet.Now();
                    //Debug.Log("############## Laser frame_ID " + msg.Header.Frame_id + " Angle_min " + msg.Angle_min + " Angle_max " + msg.Angle_max + " Angle_increment " + msg.Angle_increment);
                    //Debug.Log("############## Time_increment " + msg.Time_increment + " Scan_time " + msg.Scan_time + " Range_min " + msg.Range_min + " Range_max " + msg.Range_max + " Ranges_count " + msg.Ranges.Count + " Intensities_count " + msg.Intensities.Count);
                    
                    // init_visualization=TRUE from MarkerDetectionHololens
                    if (init_visualization)
                    {
                        init_visualization = false;
                        angle_min = msg.Angle_min;
                        angle_max = msg.Angle_max;
                        angle_increment = msg.Angle_increment;
                        samples = msg.Ranges.Count;
                        scan_time = msg.Scan_time;
                        range_min = msg.Range_min;
                        range_max = msg.Range_max;
                        ranges = msg.Ranges.ToArray();
                        Visualize();
                    }
                }
            }
        );
    }
    
    void Update()
    {
        RCLdotnet.SpinOnce(node, 500);
    }

    private void Create(int numOfLines)
    {
        Debug.Log("############## Creating INIT");
        LaserScan = new GameObject[numOfLines];
        for (int i = 0; i < numOfLines; i++)
        {
            LaserScan[i] = new GameObject("LaserScanLines");
            LaserScan[i].transform.position = origin;
            LaserScan[i].transform.parent = gameObject.transform;
            LaserScan[i].AddComponent<LineRenderer>();
            LaserScan[i].GetComponent<LineRenderer>().material = new Material(Shader.Find("Mobile/Particles/Additive"));
            ranges[i] = 1.0f;
        }
        IsCreated = true;
        Debug.Log("############## Creating END");
    }

    protected void Visualize()
    {
        if (!IsCreated)
            Create(samples);

        for (int i = 0; i < samples; i++)
        {
            try
            {
                // Check that range value is inbounds
                if (range_min < ranges[i] && ranges[i] < range_max)
                {
                    LaserScan[i].transform.position = origin;
                    LineRenderer lr = LaserScan[i].GetComponent<LineRenderer>();
                    lr.startColor = GetColor(ranges[i]);
                    lr.endColor = GetColor(ranges[i]);
                    lr.startWidth = objectWidth;
                    lr.endWidth = objectWidth;
                    Vector3 direction = new Vector3(Mathf.Cos(angle_min + angle_increment * i), 0, Mathf.Sin(angle_min + angle_increment * i));
                    lr.SetPosition(0, origin);
                    lr.SetPosition(1, origin + ranges[i] * direction * 0.5f);
                    //lr.SetPosition(1, origin + direction);
                }
            }
            catch (System.IndexOutOfRangeException e)
            {
                //System.Console.WriteLine(e.Message);
                Debug.LogError("EEERROOOOR " + e.Message);
                // Set IndexOutOfRangeException to the new exception's InnerException.
                throw new System.ArgumentOutOfRangeException("index parameter is out of range.", e);
            }
        }
        init_visualization = true;
    }

    protected Color GetColor(float distance)
    {
        float h_min = (float)0;
        float h_max = (float)0.5;

        float h = (float)(h_min + (distance - range_min) / (range_max - range_min) * (h_max - h_min));
        float s = (float)1.0;
        float v = (float)1.0;

        return Color.HSVToRGB(h, s, v);
    }
}
