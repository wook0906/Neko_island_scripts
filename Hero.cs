using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Diagnostics;
using UnityStandardAssets.Characters.FirstPerson;

public enum HeroState{
	pause,normal
}
public class Hero : MonoBehaviour {
	public static Hero S;
	public int life = 5;
	public float hitRecovery = 0.5f;
	Stopwatch sw;
	public HeroState heroState;
	float prevWS;
	float prevRS;
	public List<GameObject> catStateImages;
	void Start () {
		heroState = HeroState.normal;
		sw = new Stopwatch ();
		prevRS = this.GetComponent<FirstPersonController> ().m_RunSpeed;
		prevWS = this.GetComponent<FirstPersonController> ().m_WalkSpeed;
		catStateImages [1].SetActive (false);
	}
	void Awake(){
		S = this;
	}

	// Update is called once per frame
	void Update () {
		if (life == 0) {
			SceneManager.LoadScene ("GameOverScene");
		}
	}
	void OnCollisionEnter(Collision coll){
		if (coll.gameObject.tag == "EnemyMissile") {
			Destroy (coll.gameObject);
			life--;
			InvokeRepeating ("DamagedPause", 0f, 1f);
			GetComponentInChildren<Camera> ().SendMessage("Clash");
		}
	}
	void OnTriggerEnter(Collider coll){
		if (coll.gameObject.name == "GuideTrigger") {
			Destroy (coll.gameObject);
			Guide.S.SendMessage("CollidedTrigger");
		}
	}
	void DamagedPause(){
		heroState = HeroState.pause;
		sw.Start ();
		this.GetComponentInChildren<Weapon> ().enabled = false;
		catStateImages [1].SetActive (true);
		this.GetComponent<FirstPersonController>().m_RunSpeed = 0f;		
		this.GetComponent<FirstPersonController>().m_WalkSpeed = 0f;

		if (sw.ElapsedMilliseconds / 1000f > hitRecovery) {
			sw.Stop ();
			sw.Reset ();
			this.GetComponent<FirstPersonController>().m_RunSpeed = prevRS;
			this.GetComponent<FirstPersonController>().m_WalkSpeed = prevWS;
			heroState = HeroState.normal;
			this.GetComponentInChildren<Weapon> ().enabled = true;
			catStateImages [1].SetActive (false);
			CancelInvoke ();
		}
	}
}
