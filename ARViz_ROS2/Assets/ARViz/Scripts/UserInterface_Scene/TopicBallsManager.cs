using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TopicBallsManager : MonoBehaviour {

    Quaternion orientation;
    string topictype;
    public Material panelMaterialForDataAvailable;

    void Start () {
        orientation = this.transform.rotation;
        topictype = this.transform.Find("PanelType").transform.Find("Text").GetComponent<Text>().text;
        Debug.Log("############ TopicBallManager - " + topictype + " is available? : " + TypesAvailable.GetInstance().IsTypeRenderingAvailable(topictype));
        //Debug.Log("############ TopicBallManager - " + topictype + " is available? : " + TypesAvailable.IsTypeRenderingAvailable(topictype));
        /* 
         * If topic/service data visualization is available, 
         * show its 'type-panel' in other color/material 
         * and make it clickable -set onClick() callback-
         */
        //if (TypesAvailable.IsTypeRenderingAvailable(topictype))
        if (TypesAvailable.GetInstance().IsTypeRenderingAvailable(topictype))
        {
            this.transform.Find("PanelType").GetComponent<Image>().material = panelMaterialForDataAvailable;
            this.transform.Find("PanelType").transform.Find("Button").GetComponent<Button>().onClick.AddListener(DataVisualization);
        }
    }
	
	void Update () {
        this.transform.rotation = orientation;
	}

    /* 
     * OnClick function for 'topic-panel' button.
     * Show/hide the 'type-panel' button.
     */
    public void ShowTopicType()
    {
        Debug.Log("############ ShowTopicType");
        GameObject panel_type_go = this.transform.Find("PanelType").gameObject;
        panel_type_go.SetActive(!panel_type_go.activeSelf);
    }

    /*
     * OnClick function for 'type-panel' button.
     * Start rendering the visualization of the selected data 
     */
    public void DataVisualization()
    {
        Debug.Log("############ TopicDataVisualization");
        TopicsGroupBehaviour.Minimize();
        switch (topictype)
        {
            case "sensor_msgs::msg::dds_::LaserScan_":
                Debug.Log("DataVisualizer - RendererLaserSensor.startRendering = true");
                RendererLaserSensor.startRendering = true;
                break;
            case "sensor_msgs::msg::dds_::CompressedImage_":
                Debug.Log("DataVisualizer - RendererImageSensor.startRendering = true");
                RendererImageSensor.startRendering = true;
                break;
            case "nav_msgs::msg::dds_::OccupancyGrid_":
                Debug.Log("DataVisualizer - RendererMap.startRendering = true");
                RendererMap.startRendering = true;
                break;
            case "geometry_msgs::msg::dds_::PoseArray_":
                Debug.Log("DataVisualizer - RendererPoseArray.startRendering = true");
                RendererPoseArray.startRendering = true;
                break;
            default:
                Debug.Log("DataVisualizer - NADA DETECTADO");
                break;
        }
    }
}
