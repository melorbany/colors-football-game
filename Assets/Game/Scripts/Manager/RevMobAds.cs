using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class RevMobAds : MonoBehaviour {

	private int j = 0;
	private RevMob revmob;
	private static RevMobFullscreen fullscreen;

	private static readonly Dictionary<String, String> REVMOB_APP_IDS = new Dictionary<String, String>() {
		{ "Android", "582633de03ba46545c6c3edb"},
		{ "IOS", "582633de03ba46545c6c3edb" },
		{ "AMAZON", "5826dd104b6a58ae2a01dfe4" },

	};
	void Awake() {
		revmob = RevMob.Start(REVMOB_APP_IDS, this.gameObject.name);
	}

	// Use this for initialization
	void Start () {
		j = 0;

		Debug.Log (revmob);
		if (revmob != null) {
			fullscreen = revmob.CreateFullscreen();
		}
	}
	
	// Update is called once per frame
	void Update () {
		
		if (GameManager.instance.isGameOver == true
			&& GameManager.instance.canShowAds)
		{
			if (j == 0 && GameManager.instance.currentScore >= 3)
			{
				if (fullscreen != null) {
					fullscreen.Show();
				}
				j++;
			}
		}

	}
}
