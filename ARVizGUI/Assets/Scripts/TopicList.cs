using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TopicList : MonoBehaviour, IPointerClickHandler {

    public int num_topics;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
    }

    public void changeColor()
    {
        this.GetComponent<Renderer>().material.color = Color.green;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // Toggle the topictype gameobject visibility
        GameObject gochild = gameObject.transform.GetChild(1).gameObject;
        gochild.SetActive(!gochild.activeSelf);
    }

}
