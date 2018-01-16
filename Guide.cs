using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Diagnostics;
using UnityStandardAssets.Characters.FirstPerson;

public class Guide : MonoBehaviour {
	public static Guide S;
	public Text guideText;
	public int curStep = 0;
	bool isPassed = false;
	public GameObject TargetPrefab;
	public float termBetweenNextMessage = 4f;
	Stopwatch sw;
	Weapon weaponControl;
	GameObject HeroGO;
	FirstPersonController fpControl;
	CharacterController moveControl;
	MissileSpawner enemyMissaileControl;
	// Use this for initialization
	void Awake(){
		S = this;
	}
	void Start () {
		
		enemyMissaileControl = GameObject.Find ("missileSpawnArea").GetComponent<MissileSpawner> ();
		HeroGO = GameObject.Find("Hero");
		weaponControl =HeroGO.GetComponentInChildren<Weapon>();
		fpControl = HeroGO.GetComponentInChildren<FirstPersonController>();
		moveControl = HeroGO.GetComponent<CharacterController> ();
		sw = new Stopwatch ();
		sw.Start ();
		guideText.text = "'게임명' 에 온 것을 환영함.";
		InvokeRepeating ("StepProcess", termBetweenNextMessage, termBetweenNextMessage+0.1f);
	}
	
	// Update is called once per frame
	void Update () {
	}
	void StopWatchReset(){
		sw.Stop ();
		sw.Reset ();
	}
	bool TimeCheck(){
		if (sw.ElapsedMilliseconds / 1000f > termBetweenNextMessage)
			return true;
		return false;
	}
	void TextClear(){
		guideText.text = "";
	}
	void StepProcess(){
		print ("StepProcess!");
		switch (curStep) {
		case 0:
			if (TimeCheck ()) {
				curStep++;
				StopWatchReset ();
			}
			break;
		case 1:
			guideText.text = "우선 WASD키로 움직여서 이리로 오세여";
			moveControl.enabled = true;
			if (TimeCheck ())
				TextClear ();
			break;
		case 2:
			sw.Start ();
			Instantiate (TargetPrefab);
			guideText.text = "이제 눈앞에 나타난 오브젝트를 마우스로 조준해서 쏴봅시다.  1번과 2번을 눌러서 무기를 선택 할 수 있어요!";
			moveControl.enabled = false;
			if (TimeCheck ()) {
				weaponControl.enabled = true;
				guideText.text = "총알이 다 떨어졌다면, R키를 눌러 재장전 할 수 있어요!";
				StopWatchReset ();
			}
			break;
		case 3:
			guideText.text = "F키를 누르면 총알의 색을 바꿀 수 있어요! 색을 한번 바꿔 본 후,  바닥을 향해 쏴볼까요!?";
			sw.Start ();
			if (TimeCheck ()) {
				guideText.text = "이제 움직이면서 마을을 색칠해 볼까요!?";
				StopWatchReset ();
				curStep++;
				moveControl.enabled = true;
			}
			break;
		case 4:
			guideText.text = "";
			curStep++;
			break;
		case 8:
			guideText.text = "이게 무슨소리죠...?";
			moveControl.enabled = false;
			weaponControl.enabled = false;
			enemyMissaileControl.enabled = true;
			sw.Start ();
			if (TimeCheck ()) {
				guideText.text = "개박사의 공격이에요! 미사일을 맞게되면 왼쪽하단의 생명력이 깎이게 될거에요!";
				moveControl.enabled = true;
				StopWatchReset ();
			}
			break;
		case 9:
			weaponControl.enabled = true;
			guideText.text = "30초 동안 개박사의 공격을 피하면서 마을을 색칠해주세요!";
			sw.Start ();
			if (TimeCheck ()) {
				StopWatchReset ();
				guideText.text = "";
				GameController.S.SendMessage ("TimerStart");
				curStep++;
			}
			break;
		case 10:
			curStep++;
			break;
		case 11:
			
			this.enabled = false;
			CancelInvoke ();
			break;

		default:
			curStep++;
			break;
		}
	}
	void CollidedTrigger(){
		switch (curStep) {
		case 1:
			curStep++;
			guideText.text = "잘했습니다!";
			StopWatchReset ();
			break;
		case 2:
			curStep++;
			guideText.text = "굳!";
			StopWatchReset ();
			break;
		case 8:
			curStep++;
			break;
		default:
		break;
		}
	}
}
