using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Track_viveTrigger : MonoBehaviour {
	public List<Transform> trackPoint;
	public int totalPoints;
	public float width = 2.5f;
	public float depth = 2.5f;
	public float eclipseFactor = .5f; //if = 1, it will become a circle
	public float radi = 4f;
	public float speed = 5;
	private int currentPoint;
	private GameObject points;
    private GameObject sphere;

	// Use this for initialization
	void Start () {
		points = GameObject.Find("Points");

        sphere = GameObject.Find("Sphere");
        sphere.transform.position = new Vector3((width + radi * Mathf.Cos(0)), 1.5f, (depth - eclipseFactor * radi * Mathf.Sin(0)));

		for(float i = 0; i < Mathf.PI*2;i += ((Mathf.PI)*2)/totalPoints){

			float x = width + radi * Mathf.Cos(i);
			float y = depth - eclipseFactor * radi * Mathf.Sin(i);
			Vector3 pos = new Vector3(x,1.5f,y);

			GameObject target = new GameObject("Target");
			target.transform.position = pos;
			trackPoint.Add(target.transform);
			
			GameObject test = GameObject.CreatePrimitive(PrimitiveType.Sphere);
			test.GetComponent<SphereCollider>().enabled = false;
			test.transform.position = pos;
			test.transform.localScale = new Vector3(.1f,.1f,.1f);
			
		}
	}
	
	void OnTriggerStay (Collider other){
        //Change name from "Cube" to "Camera (eye)" and attach a Spherecollider to the Camera (eye)
		if(transform.position != trackPoint[currentPoint].position && other.name == "Cube"){
			Vector3 point = Vector3.MoveTowards(transform.position, trackPoint[currentPoint].position, speed * Time.deltaTime);
			GetComponent<Rigidbody>().MovePosition(point);
		}
		else
		{
			currentPoint = (currentPoint + 1) % trackPoint.Count;
		}
	}
}
