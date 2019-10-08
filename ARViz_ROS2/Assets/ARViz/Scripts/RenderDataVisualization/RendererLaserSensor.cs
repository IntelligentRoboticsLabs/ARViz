using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ROS2;

public class RendererLaserSensor : MonoBehaviour
{
    private Node node;
    ISubscription<sensor_msgs.msg.LaserScan> laser_sub;
    
    private bool IsCreated = false;

    private GameObject LaserScan;
    private Mesh mesh;
    private Vector3[] meshVertices;
    private Color[] meshVertexColors;
    private int[] meshTriangles;
    public static Vector3 origin;
    protected Vector3[] directions;

    int samples;
    float angle_min;
    float angle_max;
    float angle_increment;
    float scan_time;
    float range_min;
    float range_max;
    float[] ranges;

    public static bool startRendering;
    public static bool stopRendering;

    public string topic = "scan";

    void Start()
    {
        RCLdotnet.Init();
        node = RCLdotnet.CreateNode("laser_listener");

        origin = new Vector3(-3, -1, 1);

        laser_sub = node.CreateSubscription<sensor_msgs.msg.LaserScan>(
            topic, msg =>
            {
                //Debug.Log("############## I saw a laser-frame!");
                angle_min = msg.Angle_min;
                angle_max = msg.Angle_max;
                angle_increment = msg.Angle_increment;
                samples = msg.Ranges.Count;
                scan_time = msg.Scan_time;
                range_min = msg.Range_min;
                range_max = msg.Range_max;
                ranges = msg.Ranges.ToArray();
                // Clean ranges values
                for (int i = 0; i < ranges.Length; i++)
                {
                    if ((ranges[i] < range_min) || (ranges[i] > range_max))
                        ranges[i] = 0;
                }
                directions = new Vector3[ranges.Length];
                
                for (int i = 0; i < ranges.Length; i++)
                {
                    directions[i] = new Vector3(Mathf.Cos(angle_min + angle_increment * i), 0, Mathf.Sin(angle_min + angle_increment * i));
                }
                Visualize();
            }
        );

        startRendering = false;
        stopRendering = false;
    }

    void Update()
    {
        if (startRendering)
        {
            InvokeRepeating("Spinning", 0, 1f);
            startRendering = false;
        }
        if (stopRendering)
        {
            CancelInvoke("Spinning");
            DestroyRender();
            stopRendering = false;
        }
    }

    void Spinning()
    {
        RCLdotnet.SpinOnce(node, 100);
    }

    private void Create()
    {
        LaserScan = new GameObject("LaserScanMesh");
        LaserScan.transform.position = origin;
        LaserScan.transform.rotation = Quaternion.Euler(0, -90, 0);
        LaserScan.transform.parent = gameObject.transform;
        LaserScan.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = LaserScan.AddComponent<MeshRenderer>();
        meshRenderer.material = new Material(Shader.Find("Mobile/Particles/Additive"));

        mesh = LaserScan.GetComponent<MeshFilter>().mesh;
        meshVertices = new Vector3[directions.Length + 1];
        meshTriangles = new int[3 * (directions.Length - 1)];
        meshVertexColors = new Color[meshVertices.Length];

        IsCreated = true;
    }

    protected void Visualize()
    {
        if (!IsCreated)
            Create();

        meshVertices[0] = Vector3.zero;
        meshVertexColors[0] = Color.green;
        for (int i = 0; i < meshVertices.Length - 1; i++)
        {
            meshVertices[i + 1] = ranges[i] * directions[i];
            meshVertexColors[i + 1] = GetColor(ranges[i]);
        }
        for (int i = 0; i < meshTriangles.Length / 3; i++)
        {
            meshTriangles[3 * i] = 0;
            meshTriangles[3 * i + 1] = i + 2;
            meshTriangles[3 * i + 2] = i + 1;
        }
        mesh.vertices = meshVertices;
        mesh.triangles = meshTriangles;
        mesh.colors = meshVertexColors;
        
    }

    protected void DestroyRender()
    {
        Destroy(LaserScan);
        IsCreated = false;
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