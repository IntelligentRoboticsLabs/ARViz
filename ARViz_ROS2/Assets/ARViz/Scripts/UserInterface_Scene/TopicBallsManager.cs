using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TopicBallsManager : MonoBehaviour {

    Quaternion orientation;

	void Start () {
        orientation = this.transform.rotation;
    }
	
	void Update () {
        this.transform.rotation = orientation;
	}

    public void ShowTopicType()
    {
        Debug.Log("############ ShowTopicType");
        GameObject panel_type_go = this.transform.Find("PanelType").gameObject;
        panel_type_go.SetActive(!panel_type_go.activeSelf);
    }

    public void DataVisualization()
    {
        Debug.Log("############ TopicDataVisualization");
        TopicsGroupBehaviour.Minimize();
        string topictype = this.transform.Find("PanelType").transform.Find("Text").GetComponent<Text>().text;
        DataVisualizer.RenderData(topictype);
    }
}
