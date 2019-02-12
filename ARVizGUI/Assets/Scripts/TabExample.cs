using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabExample : MonoBehaviour {

    public string stringVar1;
    public string stringVar2;
    public string stringVar3;
    public string stringVar4;
    public string stringVar5;

    public int scalar_vertex;

    public int intVar1;
    public int intVar2;
    public int intVar3;
    public int intVar4;
    public int intVar5;

    public GameObject scalar_prefab;
    public GameObject image_prefab;
    public GameObject pointcloud_prefab;
    public GameObject laser_prefab;
    public GameObject tf_prefab;
    public GameObject map_prefab;
    public GameObject posearray_prefab;
    public GameObject path_prefab;

    [HideInInspector]
    public int tabsbar1;
    public int tabsbar2;
    public string currentTab;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
