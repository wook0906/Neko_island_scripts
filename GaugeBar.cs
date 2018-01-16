using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GaugeBar : MonoBehaviour {
	public float curPaintState{ get; set; }
	public float MaxPainState{ get; set; }
	public Slider paintStateBar;
	// Use this for initialization
	void Start () {
		MaxPainState = 100f;
		curPaintState = 0f;
		paintStateBar.value = CalculateGauge ();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.X)){
			InputPaintInfo (1f);
		}
	}
	void InputPaintInfo(float pInfo)
	{
		curPaintState += pInfo;
		paintStateBar.value = CalculateGauge ();
		if (curPaintState >= 95)
			WorkDone ();
	}
	float CalculateGauge(){
		return curPaintState / MaxPainState;
	}
	void WorkDone(){
		print ("다칠했다!");
	}
}
