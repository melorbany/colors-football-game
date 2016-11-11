using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using ArabicSupport;
using SmartLocalization;
using System.Text.RegularExpressions;


public class AccountUI : MonoBehaviour {

	public Button registerBtn,playBtn,homeBtn;
    public InputField nameInputField, teamInputField;
	public string gameScene,mainMenu,leaderScene;

	public Text titleP1,titleP2 ,registerButtonText;

	// Use this for initialization
	void Start () {

		if (LanguageManager.Instance.GetDeviceCultureIfSupported () == null) {
			LanguageManager.Instance.ChangeLanguage ("en");
		} else {
			LanguageManager.Instance.ChangeLanguage (LanguageManager.Instance.GetDeviceCultureIfSupported ());
		}


		//LanguageManager.Instance.ChangeLanguage ("ja");

		if (LanguageManager.Instance.GetDeviceCultureIfSupported ().languageCode.Equals ("ar")) {


			titleP1.text = ArabicFixer.Fix (LanguageManager.Instance.GetTextValue ("Register1"));
			titleP2.text = ArabicFixer.Fix (LanguageManager.Instance.GetTextValue ("Register2"));
			nameInputField.GetComponent<InputField>().text = ArabicFixer.Fix (LanguageManager.Instance.GetTextValue ("YourName"));
			teamInputField.GetComponent<InputField>().text = ArabicFixer.Fix (LanguageManager.Instance.GetTextValue ("YourTeam"));
			registerButtonText.text = ArabicFixer.Fix (LanguageManager.Instance.GetTextValue ("Register"));

		} else {

			titleP1.text = LanguageManager.Instance.GetTextValue ("Register1");
			titleP2.text = LanguageManager.Instance.GetTextValue ("Register2");
			nameInputField.GetComponent<InputField>().text = LanguageManager.Instance.GetTextValue ("YourName");
			teamInputField.GetComponent<InputField>().text = LanguageManager.Instance.GetTextValue ("YourTeam");
			registerButtonText.text = LanguageManager.Instance.GetTextValue ("Register");

		}

		registerBtn.GetComponent<Button>().onClick.AddListener(() => { RegisterBtn(); });    //play
		playBtn.GetComponent<Button>().onClick.AddListener(() => { PlayBtn(); });    //play
		homeBtn.GetComponent<Button>().onClick.AddListener(() => { HomeBtn(); });    //rate

    }
	
	// Update is called once per frame
	void Update () {
	
	}


	void RegisterBtn()
	{

		string name = nameInputField.text;
		string team = teamInputField.text;
		string userName = "";


		//Debug.Log (name);

		if (name.Length > 1) {

            userName = Highscores.instance.FormatUserName (name, team);
            Highscores.instance.AddNewHighscore (userName , GameManager.instance.hiScore);
            GameManager.instance.isUserRegistered = true;
            GameManager.instance.regUserName = userName;
            GameManager.instance.Save();
            SceneManager.LoadScene (leaderScene);
		}
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
		SceneManager.LoadScene(mainMenu);
	}


	public void FixNameInputFieldText()
	{
		string text = nameInputField.GetComponent<InputField> ().text;
	
		var isArabic = Regex.IsMatch(text, @"\p{IsArabic}");
		if (isArabic) {
			text = ArabicFixer.Fix(text, true, true);
		}

        nameInputField.GetComponent<InputField>().text = text;
    }


    public void FixTeamsInputFieldText()
    {
		string text = teamInputField.GetComponent<InputField> ().text;

		var isArabic = Regex.IsMatch(text, @"\p{IsArabic}");
		if (isArabic) {
			text = ArabicFixer.Fix(text, true, true);
		}
        
		teamInputField.GetComponent<InputField>().text = text;
    }
}
