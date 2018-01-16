using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class FollowCam : MonoBehaviour {
	Vector3 diff;
	public GameObject target;
	public float followSpeed;
	Stopwatch sw;
	void Start(){
		sw = new Stopwatch ();
		sw.Start ();
		//target = GameObject.Find ("SP");
		diff = target.transform.position - this.transform.position;
	}
	void LateUpdate(){
		if(sw.ElapsedMilliseconds/1000 >1.3f)
			//target = GameObject.Find ("SP (1)");
		if (sw.ElapsedMilliseconds / 1000 > 4f) {
			followSpeed = 8f;
			//target = GameObject.Find ("SP (2)");
		}
		print ("Hello");
		this.transform.position = Vector3.Lerp (this.transform.position,
			target.transform.position - diff, Time.deltaTime * followSpeed);
	}
}
