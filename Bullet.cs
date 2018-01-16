using UnityEngine;

using System.Collections;

public class Bullet : MonoBehaviour{
	//GameObject weapon = GameObject.FindGameObjectWithTag("Weapon");
	public ParticleSystem collPC;
	public float shootingRange=10f;
	public AudioSource audioSrc;
	public Transform LaunchPoint;
	bool isGrounded = false;
	Vector3 pos;
	void Start (){
		this.GetComponent<Rigidbody> ().useGravity = false;
		Invoke ("DelayedDestroy2", 8f);
	}
	void Update(){
		pos = LaunchPoint.position;
		if (!isGrounded)
			this.GetComponent<Rigidbody> ().transform.Translate (pos*Time.deltaTime*shootingRange);
	}
	void OnCollisionEnter(Collision coll){
		if (coll.gameObject.name == "ForTestEnemyTarget(Clone)") {
			Guide.S.SendMessage ("CollidedTrigger");
			Destroy (coll.gameObject);
		}
		ParticleSystem pt = Instantiate (collPC, transform.position, Quaternion.identity);
		pt.transform.SetParent (transform.root);

		isGrounded = true;
		this.GetComponent<Rigidbody> ().useGravity = true;
		this.GetComponent<MeshRenderer> ().enabled = false;
		Invoke ("DelayedDestroy", 0.1f);
	}
	void DelayedDestroy(){
		Destroy (this.gameObject);
	}
	void DelayedDestroy2(){
		Destroy (this.gameObject);
	}
}