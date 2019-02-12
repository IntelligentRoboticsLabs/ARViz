using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScalarVisualizer : MonoBehaviour {

    LineRenderer lr;
    public int[] values;
    Vector3[] positions;
    
    void Start () {
        lr = gameObject.AddComponent<LineRenderer>();
        lr.material = new Material(Shader.Find("Sprites/Default"));
        lr.startWidth = 0.2f;
        //lr = this.GetComponent<LineRenderer>();
        positions = new Vector3[values.Length];
    }
	
	void Update () {
        for (int i = 0; i < values.Length; i++)
        {
            positions[i] = new Vector3(i, values[i], 0);
        }
        lr.positionCount = positions.Length;
        lr.SetPositions(positions);
    }
}
