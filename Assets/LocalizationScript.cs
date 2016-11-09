using UnityEngine;
using System.Collections;
using SmartLocalization;
using System.Collections.Generic;

public class LocalizationScript : MonoBehaviour {

	Dictionary<string, string> dictionary = 
		new Dictionary<string, string> { { "English", "en" }, { "Arabic", "ar" },
		{ "Chinese", "zh-CHS" }, { "Japanese", "ja" },{ "Russian", "ru" }, { "German", "de" }
		,{ "French", "fr" }, { "Spanish", "es" }};


	// Use this for initialization
	void Start () {
	
		Debug.Log ("Localization Script Start");
		string language = LanguageManager.Instance.GetSystemLanguageEnglishName ();
		language = "Arabic";
		if (LanguageManager.Instance.IsLanguageSupportedEnglishName (language)) {
			LanguageManager.Instance.ChangeLanguage (dictionary[language]);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
