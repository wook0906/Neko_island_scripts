using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Diagnostics;

public class PaintCompleteScene : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		Invoke ("DelayedLoadScene",5f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void DelayedLoadScene(){
		SceneManager.LoadScene ("GameOverScene");
	}
}
