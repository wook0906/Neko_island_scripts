using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileSpawner : MonoBehaviour {
	public GameObject missilePrefab;
	public float missileRespawnTime = 3f;
	GameObject tMissile;
	// Use this for initialization
	void Start () {
		InvokeRepeating ("missileSpawn", 0, missileRespawnTime);
	}
	
	// Update is called once per frame
	void Update () {
	}
	void missileSpawn(){
		Vector3 pos = this.transform.position;
		tMissile = Instantiate (missilePrefab);
		pos.x = Random.Range (-(this.transform.localScale.x * 5), this.transform.localScale.x * 5);
		pos.z = Random.Range (-(this.transform.localScale.z * 5), this.transform.localScale.z * 5);
		tMissile.transform.position = pos;
	}
}
