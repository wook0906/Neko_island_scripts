using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public enum ColorType{
	red,green,blue
}
public enum WeaponType{
	none,pistol,rocketLauncher,weapon3
}
//todo : weapon3 미정

public class Weapon : MonoBehaviour {
	public static Weapon S;
	public bool isFiring;
	public float timeBetweenShots;
	public Transform LaunchPoint;
	GameObject curWeapon;
	public List<Bullet> bulletPrefab;
	public List<GameObject> weaponPrefabList;
	public List<GameObject> weaponList;
	public WeaponType curSelectWeapon;
	public float shootingTempo = 0.3f;
	public float r = 1f;
	public float g = 0f;
	public float b = 0f;
	public Pistol pistol;
	public RocketLauncher rocket;
	Stopwatch reloadSW;
	public ColorType curColor;
	// Use this for initialization
	void Awake(){
		S = this;
	}
	void Start () {
		curColor = ColorType.red;
		curSelectWeapon = WeaponType.none;
		reloadSW = new Stopwatch ();
		for(int i = 0;i<=weaponPrefabList.Count-1;i++){
			weaponList.Add(Instantiate (weaponPrefabList [i]));
			weaponList[i].transform.position = transform.position;
			weaponList[i].transform.SetParent (this.transform);
			weaponList[i].SetActive (false);
		}
		pistol = weaponList [0].GetComponent<Pistol> ();
		rocket = weaponList [1].GetComponent<RocketLauncher> ();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.F)){
			switch (curColor) {
			case ColorType.red:
				{
					curColor = ColorType.green;
					r = 0f;
					g = 1f;
					b = 0f;
					break;
				}
			case ColorType.blue:
				{
					curColor = ColorType.red;
					r = 1f;
					g = 0f;
					b = 0f;
					break;
				}
			case ColorType.green:
				{
					curColor = ColorType.blue;
					r = 0f;
					g = 0f;
					b = 1f;
					break;
				}
			}
		}
		if (Input.GetKeyDown (KeyCode.R)) {
			if (curSelectWeapon == WeaponType.pistol) {
				pistol.isReady = false;
				reloadSW.Start ();
				if (reloadSW.ElapsedMilliseconds / 1000f > pistol.reloadingTime) {
					reloadSW.Stop ();
					reloadSW.Reset ();
					pistol.remainBullet = 50f;
					pistol.isReady = true;
				}
			}
			if (curSelectWeapon == WeaponType.rocketLauncher) {
				rocket.isReady = false;	
				reloadSW.Start ();
				if (reloadSW.ElapsedMilliseconds / 1000f > rocket.reloadingTime) {
					reloadSW.Stop ();
					reloadSW.Reset ();
					rocket.remainBullet = 1f;
					rocket.isReady = true;
				}
			}
		}
		if (curSelectWeapon != WeaponType.pistol && Input.GetKeyDown (KeyCode.Alpha1)) {
			curSelectWeapon = WeaponType.pistol;
			changeWeapon ();
		}
		if(curSelectWeapon!=WeaponType.rocketLauncher&&Input.GetKeyDown(KeyCode.Alpha2)){
			curSelectWeapon = WeaponType.rocketLauncher;
			changeWeapon ();//todo
		}
		if(Input.GetMouseButtonDown(0)){
			isFiring = true;
			Fire();
			if (curSelectWeapon == WeaponType.pistol) {
				Vector3 pos = LaunchPoint.transform.localPosition;
				pos.x -= 0.535f;
				LaunchPoint.transform.localPosition = pos;
				Fire ();
				pos.x += 0.535f;
				LaunchPoint.transform.localPosition = pos;
			}
		}
		if (Input.GetMouseButtonUp (0)) {
			isFiring = false;
		}
	}
	void changeWeapon(){//
		switch (curSelectWeapon) {
		case WeaponType.none:
			break;
		case WeaponType.pistol:
			foreach (var weapon in weaponList) {
				if (weapon.name == "Pistol(Clone)") {
					Vector3 pos = new Vector3 (0.026f, -0.24f, 0.238f);
					weapon.transform.localPosition = pos;
					weapon.transform.localRotation = Quaternion.Euler (0f, -90f, 0f);
					weapon.SetActive (true);
				}else
					weapon.SetActive (false);
			}
			break;
		case WeaponType.rocketLauncher:
			foreach (var weapon in weaponList) {
				if (weapon.name == "rocketLauncher(Clone)") {
					Vector3 pos = new Vector3 (0.12f, -0.047f, 0.246f);
					weapon.transform.localPosition = pos;
					weapon.SetActive (true);
				} else
					weapon.SetActive (false);
			}
			break;
		default:
			break;
		}
	}

	void Fire(){
		if (curSelectWeapon == WeaponType.none) {
			return;
		}
		switch (curSelectWeapon) {
		case WeaponType.pistol:
			foreach (var weapon in weaponList) {
				if (weapon.name == "Pistol(Clone)") {
					Bullet bullet;
					if (pistol.isReady) {
						bullet = Instantiate (bulletPrefab [0], LaunchPoint.position, LaunchPoint.rotation) as Bullet;
						pistol.remainBullet--;
						if (pistol.remainBullet == 0)
							pistol.isReady = false;
					}
				}
			}
			break;
		case WeaponType.rocketLauncher:
			foreach (var weapon in weaponList) {
				if (weapon.name == "rocketLauncher(Clone)") {
					Bullet bullet;
					if (rocket.isReady) {
						bullet = Instantiate (bulletPrefab [1], LaunchPoint.position, LaunchPoint.rotation) as Bullet;
						rocket.remainBullet--;
						if (rocket.remainBullet == 0)
							rocket.isReady = false;
					}
				}
			}
			break;
		default:
			break;
		}
	}
}
