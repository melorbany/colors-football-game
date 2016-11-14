using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class RevMobAds : MonoBehaviour {

	private int j = 0;
	private RevMob revmob;
	private static RevMobFullscreen fullscreen;
	private static RevMobFullscreen video;



	private static readonly Dictionary<String, String> REVMOB_APP_IDS = new Dictionary<String, String>() {
		{ "Android", "582633de03ba46545c6c3edb"},
		{ "IOS", "5826dce203ba46545c6c3f31" }
	};
	void Awake() {
		revmob = RevMob.Start(REVMOB_APP_IDS, this.gameObject.name);
	}
		

	// Use this for initialization
	void Start () {
		j = 0;
		if (revmob != null) {
			fullscreen = revmob.CreateFullscreen();
			video = revmob.CreateRewardedVideo ();
		}	
	}
	
	// Update is called once per frame
	void Update () {
		
		if (j == 0 &&  GameManager.instance.isGameOver == true
			&& GameManager.instance.canShowAds)
		{
			j++;

			if (GameManager.instance.currentScore > 100 || (GameManager.instance.currentScore > 70 &&
				GameManager.instance.currentScore >= GameManager.instance.hiScore)) {

				if (video != null) {
					video.ShowRewardedVideo ();
				}else if (fullscreen != null) {
					fullscreen.Show();
				}
				
			}else if (GameManager.instance.currentScore >= 3)
			{
				if (fullscreen != null) {
					fullscreen.Show();
				}
			}
				

		}

	}



}
