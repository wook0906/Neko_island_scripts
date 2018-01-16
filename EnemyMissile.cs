using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class EnemyMissile : MonoBehaviour {
	public float missileSpeed = 10f;
	public float aimingTime = 1f;
	GameObject target;
	Vector3 pos;
	Vector3 rot;
	bool isCollided = false;
	// Use this for initialization
	Stopwatch sw;
	void Start () {
		sw = new Stopwatch ();
		sw.Start ();
		target = GameObject.FindGameObjectWithTag ("Hero");
	}
	
	// Update is called once per frame
	void Update () {
		if (sw.ElapsedMilliseconds / 1000f > aimingTime) {
			if (!isCollided) {
				this.transform.Translate (Vector3.forward * missileSpeed * Time.deltaTime);
			}
		} else {
			Vector3 targetDir = target.transform.position - this.transform.position;
			Vector3 newDir = Vector3.RotateTowards(this.transform.forward,targetDir,missileSpeed*Time.deltaTime,0f);
			this.transform.rotation = Quaternion.LookRotation(newDir);
		}
	}
	void OnCollisionEnter(Collision coll){
		if(coll.gameObject.tag =="Hero" && Guide.S.enabled  == true){
			Guide.S.SendMessage("CollidedTrigger");
		}
		isCollided = true;
		this.GetComponent<Rigidbody> ().useGravity = true;
		this.GetComponent<MeshRenderer> ().enabled = false;
		Invoke ("DelayDestroy", 0.1f);
	}
	void DelayDestroy(){
		Destroy (this.gameObject);
	}
}