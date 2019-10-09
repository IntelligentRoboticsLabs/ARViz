using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ROS2;
using ROS2.Utils;

public class GraphScript : MonoBehaviour {

    private Node node;
    //private ISubscription<std_msgs.msg.String> chatter_sub;
    private IGraph graph;

    void Start () {
        RCLdotnet.Init();
        node = RCLdotnet.CreateNode("listener");
        //chatter_sub = node.CreateSubscription<std_msgs.msg.String>("chatter", msg => Debug.Log("I heard: [" + msg.Data + "]"));
        graph = new Graph(node);
    }

	void Update () {
        SortedDictionary<string, List<string>> topics = graph.GetTopicNamesAndTypes(true);
        Debug.Log("num topics: " + topics.Count.ToString());

        //VisualizationScript.ntopics = topics.Count;

        foreach (KeyValuePair<string, List<string>> kvp in topics)
        {
            string topictype = "";
            //Debug.Log("\ttopic: [" + kvp.Key + "]");
            foreach (string type in kvp.Value)
            {
                //Debug.Log("\t\ttype: [" + type + "]");
                topictype += type + " ";
            }
            Debug.Log(kvp.Key+": "+topictype);
            //Debug.Log("\t\tpublishers: [" + graph.CountPublishers(kvp.Key) + "]");
            //Debug.Log("\t\tsubscribers: [" + graph.CountSubscribers(kvp.Key) + "]");
        }

        SortedDictionary<string, List<string>> services = graph.GetServiceNamesAndTypes();

        Debug.Log("num services: " + services.Count.ToString());
        foreach (KeyValuePair<string, List<string>> kvp in services)
        {
            string servicetype = "";
            //Debug.Log("\tservice: [" + kvp.Key + "]");
            foreach (string type in kvp.Value)
            {
                //Debug.Log("\t\ttype: [" + type + "]");
                servicetype += type + " ";
            }
            Debug.Log(kvp.Key + ": " + servicetype);
        }

        List<string> nodes = graph.GetNodeNames();
        Debug.Log("num nodes: " + nodes.Count.ToString());
        foreach (string node_name in nodes)
        {
            Debug.Log("node: [" + node_name + "]");
        }
    }
}