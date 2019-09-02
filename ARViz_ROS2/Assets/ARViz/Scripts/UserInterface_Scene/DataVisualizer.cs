using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ROS2;

public class DataVisualizer : MonoBehaviour
{
    static Transform this_transform;

    void Start()
    {
        this_transform = this.transform;
    }

    // Activa las variables de los scripts que inician la renderizacion de los datos de los sensores
    public static void RenderData(string topicType)
    {
        switch (topicType)
        {
            case "sensor_msgs::msg::dds_::LaserScan_":
                Debug.Log("DataVisualizer - RendererLaserSensor.startRendering = true");
                RendererLaserSensor.startRendering = true;
                break;
            case "sensor_msgs::msg::dds_::CompressedImage_":
                Debug.Log("DataVisualizer - RendererImageSensor.startRendering = true");
                RendererImageSensor.startRendering = true;
                break;
            default:
                Debug.Log("DataVisualizer - NADA DETECTADO");
                break;
        }
    }

    void Update()
    {

    }
}
