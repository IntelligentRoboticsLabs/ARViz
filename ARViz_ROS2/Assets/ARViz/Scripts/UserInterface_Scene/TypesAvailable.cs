using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypesAvailable : MonoBehaviour
{
    private static TypesAvailable instance;

    public List<string> availableTypesList = new List<string>{
        "sensor_msgs::msg::dds_::LaserScan_",
        "sensor_msgs::msg::dds_::CompressedImage_",
        "nav_msgs::msg::dds_::OccupancyGrid_",
        "geometry_msgs::msg::dds_::PoseArray_"
    };

    private void Start()
    {
        instance = this;
    }

    public static TypesAvailable GetInstance()
    {
        return instance;
    }

    public bool IsTypeRenderingAvailable(string datatype)
    {
        return availableTypesList.Contains(datatype);
    }
}
