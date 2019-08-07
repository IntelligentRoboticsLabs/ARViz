using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

using ROS2;

public class RobotPositionVisualization : MonoBehaviour
{
    private INode node;
    ISubscription<geometry_msgs.msg.Twist> position_sub_r1;
    ISubscription<geometry_msgs.msg.Twist> position_sub_r2;
    GameObject robot_1, robot_2;
    public static Vector3 origin_1, origin_2;
    private bool AreRobotsCreated;
    public static bool init_visualization;

    void Start()
    {

    }

    void Update()
    {
        if (init_visualization)
        {
            Visualize();
        }
    }

    private void Create()
    {
        Debug.Log("############## Creating INIT");
        robot_1 = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        robot_1.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        robot_2 = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        robot_2.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        AreRobotsCreated = true;
        Debug.Log("############## Creating END");
    }

    protected void Visualize()
    {
        if (!AreRobotsCreated)
            Create();

        robot_1.transform.position = new Vector3(-origin_1.y, origin_1.z, origin_1.x);
        robot_2.transform.position = new Vector3(-origin_2.y, origin_2.z, origin_2.x);
        init_visualization = true;
    }
}
