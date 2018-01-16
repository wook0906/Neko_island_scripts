using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifePanel : MonoBehaviour {
	public List<GameObject> icons;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void UpdateLife(int Life)
	{
		for (int i = 0; i < icons.Count; i++) {
			if (i < Life)
				icons [i].SetActive (true);
			else
				icons [i].SetActive (false);
		}
	}
}
