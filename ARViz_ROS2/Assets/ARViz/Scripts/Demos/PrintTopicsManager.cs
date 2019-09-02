using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ROS2;
using ROS2.Utils;

public class PrintTopicsManager : MonoBehaviour {

    private IPublisher<std_msgs.msg.String> chatter_pub;
    private INode node;

    // Use this for initialization
    void Start () {
        RCLdotnet.Init();
        node = RCLdotnet.CreateNode("printer");
        chatter_pub = node.CreatePublisher<std_msgs.msg.String>("chatter");
    }
	
	// Update is called once per frame
	void Update () {
        if (RCLdotnet.Ok())
        {

            std_msgs.msg.String msg = new std_msgs.msg.String();
            msg.Data = "Hello World: ";
            chatter_pub.Publish(msg);

            //RCLdotnet.PrintTopicNamesAndTypes(node.Handle);
        }
    }
}
