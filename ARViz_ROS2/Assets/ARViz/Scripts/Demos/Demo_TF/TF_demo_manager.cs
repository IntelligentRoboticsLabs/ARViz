using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TF_demo_manager : MonoBehaviour
{
    public GameObject turtle_leader;
    public GameObject turtle_follower;

    void Start()
    {
        Instantiate(turtle_leader, new Vector3(), new Quaternion());
        Instantiate(turtle_follower, new Vector3(0.1f, 0.1f, 0.1f), new Quaternion());
    }
}
