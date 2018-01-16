using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Diagnostics;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
	public LifePanel lifePanel;
	public List<Mesh> meshList;
	public Text ColorText;
	public Text TimerText;
	public Text RemainBulletText;
	Stopwatch LimitTime;
	public static GameController S;
	// Use this for initialization
	void Awake(){
		S = this;
	}

	void Start () {
		LimitTime = new Stopwatch ();
		TimerText.text = "";
	}
	
	// Update is called once per frame
	void Update () {
		TimerText.text = "" + LimitTime.ElapsedMilliseconds / 1000f;
		if (Weapon.S.curSelectWeapon == WeaponType.pistol) {
			RemainBulletText.text = "" + Weapon.S.pistol.remainBullet + " / 50";
		}
		if (Weapon.S.curSelectWeapon == WeaponType.rocketLauncher) {
			RemainBulletText.text = "" + Weapon.S.rocket.remainBullet + " / 1";
		}
		lifePanel.UpdateLife (Hero.S.life);
		switch (Weapon.S.curColor) {
		case ColorType.red:
			ColorText.text = "Red";
			ColorText.color = Color.red;
			break;
		case ColorType.green:
			ColorText.text = "Green";
			ColorText.color = Color.green;
			break;
		case ColorType.blue:
			ColorText.text = "Blue";
			ColorText.color = Color.blue;
			break;
		} 
		if (LimitTime.ElapsedMilliseconds / 1000f > 30f) {
			SceneManager.LoadScene ("necoisland");
		}
	}
	void TimerStart(){
		LimitTime.Start ();
	}
}
