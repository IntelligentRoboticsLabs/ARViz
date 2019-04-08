using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSphereTopics : MonoBehaviour, IManipulationHandler {
    public void OnManipulationCanceled(ManipulationEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnManipulationCompleted(ManipulationEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnManipulationStarted(ManipulationEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnManipulationUpdated(ManipulationEventData eventData)
    {
        float multiplier = 1.0f;
        float Speed = 10;

        var rotation = new Vector3(eventData.CumulativeDelta.y * -multiplier, eventData.CumulativeDelta.x * multiplier);
        transform.Rotate(rotation * Speed, Space.World);
    }


    void Start () {
		
	}
	
	void Update () {

    }

    
}
