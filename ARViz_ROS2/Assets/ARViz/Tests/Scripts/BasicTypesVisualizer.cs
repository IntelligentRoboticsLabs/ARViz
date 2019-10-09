using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasicTypesVisualizer : MonoBehaviour {

    [System.Serializable]
    public class BasicType
    {
        public GameObject prefab;
        public bool instantiate;
    }

    public BasicType scalar;
    public BasicType image;
    public BasicType pointcloud;
    public BasicType laser;
    public BasicType tf;
    public BasicType map;
    public BasicType posearray;
    public BasicType path;
    private BasicType[] basictypes = new BasicType[8];
    public Dropdown m_Dropdown;
    public Transform m_Transform;

    // Use this for initialization
    void Start () {
        basictypes[0] = scalar;
        basictypes[1] = image;
        basictypes[2] = pointcloud;
        basictypes[3] = laser;
        basictypes[4] = tf;
        basictypes[5] = map;
        basictypes[6] = posearray;
        basictypes[7] = path;
        /*
        for(int i = 0; i < basictypes.Length; i++)
        {
            if (basictypes[i].instantiate)
            {
                Instantiate(basictypes[i].prefab, new Vector3(i,0,0), Quaternion.identity);
            }
        }
        */
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void instantiateBasicType(int i)
    {
        Debug.Log(m_Dropdown.options[i].text);
        GameObject basicTypeObject = GameObject.FindWithTag("BasicType");
        if (basicTypeObject != null)
            Destroy(basicTypeObject);
        Instantiate(basictypes[i].prefab, m_Transform.position, Quaternion.identity);
        /*
        switch (m_Dropdown.options[i].text)
        {
            case "Scalar":
                Instantiate(basictypes[i].prefab, transform.position, Quaternion.identity);
                break;
            case "Image":
                break;
            case "Pointcloud":
                break;
            case "Laser":
                break;
            case "Tf":
                break;
            case "Map":
                break;
            case "PoseArray":
                break;
            case "Path":
                break;
            default:
                break;
        }
        */
    }
}
