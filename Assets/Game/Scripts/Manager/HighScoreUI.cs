﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using ArabicSupport;
using SmartLocalization;


public class HighScoreUI : MonoBehaviour {

	public Button playBtn,homeBtn;
	public string gameScene;
	public string mainMenu;
	public Text titleText;


	// Use this for initialization
	void Start () {
		
		if (LanguageManager.Instance.GetDeviceCultureIfSupported () == null) {
			LanguageManager.Instance.ChangeLanguage ("en");
		} else {
			LanguageManager.Instance.ChangeLanguage (LanguageManager.Instance.GetDeviceCultureIfSupported ());
		}


		//LanguageManager.Instance.ChangeLanguage ("ja");

		if (LanguageManager.Instance.GetDeviceCultureIfSupported ().languageCode.Equals ("ar")) {
			titleText.text = ArabicFixer.Fix (LanguageManager.Instance.GetTextValue ("BestPlayers"));
		} else {
			titleText.text = LanguageManager.Instance.GetTextValue ("BestPlayers");
		}


		playBtn.GetComponent<Button>().onClick.AddListener(() => { PlayBtn(); });    //play
		homeBtn.GetComponent<Button>().onClick.AddListener(() => { HomeBtn(); });    //rate
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void PlayBtn()
	{
        GameManager.instance.isGameOver = false;
        #if UNITY_5_3 || UNITY_5_3_OR_NEWER
        SceneManager.LoadScene(gameScene);
		#else
		Application.LoadLevel(gameScene);
		#endif
	}


	void HomeBtn()
	{
        GameManager.instance.isGameOver = false;
        SceneManager.LoadScene(mainMenu);
	}
}
