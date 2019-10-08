using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ROS2;
using ROS2.Utils;
using UnityEngine.UI;

public class TopicsVisualizer : MonoBehaviour {

    public GameObject topicBall;
    public GameObject interactionZone;
    float circle_radius;
    public static float ntopics;
    float circle_rads = 2*Mathf.PI;
    float ball_radius = 0.2f;
    
    public static Vector3 init_pos;
    public static Quaternion init_rot;
    public static bool init_info;

    SortedDictionary<string, List<string>> topics;

    private Node node;
    private IGraph graph;

    public Vector3 robotInitPosition;

    void Start () {
        init_info = false;
        init_pos = robotInitPosition;
        init_rot = new Quaternion();
        RCLdotnet.Init();
        node = RCLdotnet.CreateNode("drawer");
        graph = new Graph(node);
    }
	
	void Update () {
        if (init_info == true)
        {
            topics = graph.GetTopicNamesAndTypes(true);
            ntopics = topics.Count;
            Debug.Log("############## num topics: " + ntopics);
            if (ntopics > 0)
            {
                init_info = false;

                // TODO: Redibujar cuando se añadan/eliminen topics
                
                Debug.Log("############## init_pos: " + init_pos);
                Debug.Log("############## init_rot: " + init_rot);
                BallsCircle(init_pos, init_rot);
            }
        }
	}

    void BallsCircle(Vector3 pos, Quaternion rot)
    {
        circle_radius = ntopics / 6 * ball_radius;
        float arc = circle_rads / ntopics;
        /*
         *Place interaction zone with respect to the marker/robot
         * Zero-quaternion so the topics group is exactly vertical
         */
        GameObject interact_go = Instantiate(interactionZone, pos, new Quaternion());

        /* Place balls in a wheel */
        for (float a = 0; a < circle_rads; a += arc)
        {
            float y = circle_radius * Mathf.Sin(a);
            float z = circle_radius * Mathf.Sin((Mathf.PI / 2) - a);
            GameObject go = Instantiate(topicBall);
            go.transform.parent = interact_go.transform;
            go.transform.localPosition = new Vector3(0, y, z);
            go.transform.rotation = Quaternion.identity;
        }
        int cnt = 0;
        foreach (KeyValuePair<string, List<string>> pair in topics)
        {

            Debug.Log("############## \t topic: [" + pair.Key + "]");
            foreach (string type in pair.Value)
            {
                Debug.Log("############## \t\t type: [" + type + "]");
            }
            interact_go.transform.GetChild(cnt).GetChild(0).transform.Find("PanelTopic").transform.Find("Text").GetComponent<Text>().text = pair.Key;
            interact_go.transform.GetChild(cnt).GetChild(0).transform.Find("PanelType").transform.Find("Text").GetComponent<Text>().text = pair.Value[0];
            cnt++;
        }
        interact_go.GetComponent<SphereCollider>().radius = circle_radius + ball_radius;
        interact_go.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
    }
}
