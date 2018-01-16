using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour {
	public List<Transform> wayPoints;
	int currentWayPoint = 0;
	public float moveSpeed = 1f;
	float maxAnimationDelayedStartTime = 3f;
	// Use this for initialization
	void Start () {
		this.GetComponent<Animator> ().enabled = false;
		RotateToTarget ();
	}
	// Update is called once per frame
	void Update () {
		if (Time.time > Random.Range (0f, maxAnimationDelayedStartTime))
			this.GetComponent<Animator> ().enabled = true;
		Vector3 pos = this.transform.position;
		pos += this.transform.forward * moveSpeed * Time.deltaTime;
		this.transform.position = pos;

	}

	void OnTriggerEnter(Collider coll){
		if(coll.gameObject.tag == "WayPoint"){
			if(coll.transform == wayPoints[currentWayPoint].transform){
				currentWayPoint++;
				if (currentWayPoint >= wayPoints.Count) {
					currentWayPoint = 0;
				}
				RotateToTarget ();
			}
		}
	}
	void RotateToTarget(){
		Vector3 anyVector = transform.InverseTransformPoint(wayPoints [currentWayPoint].transform.position);
		float angle = Mathf.Atan2 (anyVector.x, anyVector.z) * Mathf.Rad2Deg;
		this.transform.Rotate (Vector3.up, angle);
	}
}
