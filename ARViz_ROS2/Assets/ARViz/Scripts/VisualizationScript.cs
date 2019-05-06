using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualizationScript : MonoBehaviour {

    public GameObject ball;
    public GameObject topicsGroup;
    float circle_radius;
    public static float ntopics;
    float circle_rads = 2*Mathf.PI;
    float ball_radius = 0.2f;

    void Start () {
        ntopics = 11;
        //circle_radius = 2;
        circle_radius = ntopics/6*ball_radius;
        BallsCircle();
    }
	
	void Update () {
		
	}

    void BallsCircle()
    {
        float arc = circle_rads / ntopics;
        // Place balls in a circle
        for (float a = 0; a < circle_rads; a += arc)
        {
            float y = circle_radius * Mathf.Sin(a);
            float z = circle_radius * Mathf.Sin((Mathf.PI / 2) - a);
            //Instantiate(ball, new Vector3(0, y, z), Quaternion.identity, topicsGroup.transform);
            GameObject go = Instantiate(ball);
            go.transform.parent = topicsGroup.transform;
            go.transform.localPosition = new Vector3(0, y, z);
            go.transform.rotation = Quaternion.identity;
        }
        topicsGroup.GetComponent<SphereCollider>().radius = circle_radius+ ball_radius;
    }
}
