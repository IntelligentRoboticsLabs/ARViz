using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using HoloToolkit.Unity.InputModule;

public class TopicsGroupBehaviour : MonoBehaviour, IManipulationHandler, IInputClickHandler
{
    static Transform this_transform;
    static bool isMinimized;
    static float t;
    static bool goToScaling;

    public void OnInputClicked(InputClickedEventData eventData)
    {
        if (isMinimized)
        {
            Debug.Log("TopicsGroupBall Touched!!!");
            Minimize();
            RendererImageSensor.stopRendering = true;
            RendererLaserSensor.stopRendering = true;
            RendererMap.stopRendering = true;
            RendererPoseArray.stopRendering = true;
        }
    }

    public void OnManipulationCanceled(ManipulationEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnManipulationCompleted(ManipulationEventData eventData)
    {
        //throw new System.NotImplementedException();
    }

    public void OnManipulationStarted(ManipulationEventData eventData)
    {
        //throw new System.NotImplementedException();
    }

    public void OnManipulationUpdated(ManipulationEventData eventData)
    {
        float multiplier = 1.0f;
        float Speed = 10;

        //var rotation = new Vector3(eventData.CumulativeDelta.y * -multiplier, eventData.CumulativeDelta.x * multiplier);
        var rotation = new Vector3(eventData.CumulativeDelta.y * multiplier, 0, 0);
        transform.Rotate(rotation * Speed, Space.World);
    }

    public static void ToggleHideTopics()
    {
        foreach (Transform child in this_transform)
        {
            //Debug.Log(child.name);
            child.gameObject.SetActive(!child.gameObject.activeSelf);
        }
    }

    public static void Minimize()
    {
        ToggleHideTopics();
        t = 0.0f;
        goToScaling = true;
    }
    
    void Start () {
        this_transform = this.transform;
        isMinimized = false;
        goToScaling = false;
    }
	
	void Update () {
        if (goToScaling) {
            float step = 0.5f * Time.deltaTime;
            Vector3 init_pos = this_transform.localPosition;
            Vector3 goal_pos_to_min = new Vector3(init_pos.x - 0.1f, init_pos.y - 0.1f, init_pos.z);
            Vector3 goal_pos_from_min = new Vector3(init_pos.x + 0.1f, init_pos.y + 0.1f, init_pos.z);
            if (!isMinimized)
            {
                //Debug.Log("############### Minimize - position " + this_transform.position);
                //Debug.Log("############### Minimize - localPosition " + this_transform.localPosition);
                this_transform.localScale = new Vector3(1, 1, 1) * Mathf.Lerp(0.5f, 0.25f, t);
                transform.position = Vector3.MoveTowards(init_pos, goal_pos_to_min, step);
            }
            else
            {
                this_transform.localScale = new Vector3(1, 1, 1) * Mathf.Lerp(0.25f, 0.5f, t);
                transform.position = Vector3.MoveTowards(init_pos, goal_pos_from_min, step);
            }
            t += step;
            if (t > 1.0f)
            {
                goToScaling = false;
                isMinimized = !isMinimized;
            }
        }
    }
}
