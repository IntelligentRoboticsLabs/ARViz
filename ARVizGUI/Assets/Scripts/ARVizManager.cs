using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARVizManager : MonoBehaviour {

    public GameObject image;
    public GameObject map;
    public GameObject laser;

    public Material material;
    private bool IsCreated = false;
    //private GameObject LaserScan;
    private GameObject[] LaserScan;
    private Mesh mesh;
    private Vector3[] meshVerticies;
    private int[] meshTriangles;
    private Color[] meshVertexColors;

    private Vector3[] directions = new Vector3[61];

    int samples = 61;
    public int update_rate = 1800;
    public float angle_min = -2.095f;
    public float angle_max = 2.095f;
    public float angle_increment = 0.0686688497663f;
    public float time_increment = 0;
    public float scan_time = 0;
    public float range_min = 0.1f;
    public float range_max = 3;
    public double[] ranges = {
        2.75, 0.44, 3.0, 3.0, 3.0, 3.0, 3.0, 3.0, 3.0, 3.0,
        3.0, 3.0, 3.0, 3.0, 3.0, 3.0, 3.0, 3.0, 3.0, 3.0,
        3.0, 3.0, 3.0, 1.38, 1.37, 1.35, 3.0, 3.0, 3.0, 3.0,
        3.0, 3.0, 3.0, 3.0, 3.0, 3.0, 1.74, 1.82, 3.0, 3.0,
        3.0, 3.0, 3.0, 3.0, 3.0, 3.0, 3.0, 3.0, 3.0, 3.0,
        3.0, 3.0, 3.0, 3.0, 3.0, 3.0, 3.0, 3.0, 3.0, 3.0, 3.0 };
    public float[] intensities;

    void Start () {
    }
	
  	void Update () {
  	}

    public void canvasClick(string gameobject)
    {
        GameObject go = GameObject.Find(gameobject);
        go.GetComponent<MeshRenderer>().enabled = !(go.GetComponent<MeshRenderer>().enabled);
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

    public void Visualize()
    {
        if (laser.transform.childCount > 0)
        {
            foreach (Transform child in laser.transform)
            {
                GameObject.Destroy(child.gameObject);
            }
        }
        else
        {
            LaserScan = new GameObject[samples];
            for (int i = 0; i < samples; i++)
            {
                LaserScan[i] = new GameObject("LaserScanLines");
                LaserScan[i].transform.position = laser.transform.position;
                LaserScan[i].transform.parent = laser.transform;
                LaserScan[i].AddComponent<LineRenderer>();
                LaserScan[i].GetComponent<LineRenderer>().material = material;

                directions[i] = new Vector3(Mathf.Cos(angle_min + angle_increment * i), 0, Mathf.Sin(angle_min + angle_increment * i));

                LineRenderer lr = LaserScan[i].GetComponent<LineRenderer>();
                lr.startColor = GetColor((float)ranges[i]);
                lr.endColor = GetColor((float)ranges[i]);
                lr.startWidth = 0.1f;
                lr.endWidth = 0.1f;
                lr.SetPosition(0, laser.transform.position);
                lr.SetPosition(1, laser.transform.position + (float)ranges[i] * directions[i] * 0.8f);
            }
        }
    }
}
