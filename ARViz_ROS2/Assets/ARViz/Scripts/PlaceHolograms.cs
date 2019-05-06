using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HoloToolkit.Unity.Preview.SpectatorView
{
  public class PlaceHolograms : MonoBehaviour
  {

    [Tooltip("Component for sending hololens webcam feed to the marker detection code")]
    [SerializeField]
    private MarkerDetectionHololens hololensMarkerDetector;

    public MarkerDetectionHololens HololensMarkerDetector
    {
        get { return hololensMarkerDetector; }
        set { hololensMarkerDetector = value; }
    }

    void OnDestroy()
    {
        HololensMarkerDetector.OnMarkerDetected -= HologramOnARuco;
    }

    private void HologramOnARuco(int markerId, Vector3 pos, Quaternion rot)
    {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = new Vector3(0, 0, 0);
    }
  }
}
