using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using ArabicSupport;


public class AccountUI : MonoBehaviour {

	public Button registerBtn,playBtn,homeBtn;
    public InputField nameInputField, teamInputField;
	public string gameScene,mainMenu,leaderScene;


	// Use this for initialization
	void Start () {

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


		Debug.Log (name);

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
        string fixedArabic = ArabicFixer.Fix(nameInputField.GetComponent<InputField>().text, true, true);
        nameInputField.GetComponent<InputField>().text = fixedArabic;
    }


    public void FixTeamsInputFieldText()
    {
        string fixedArabic = ArabicFixer.Fix(teamInputField.GetComponent<InputField>().text, true, true);
        teamInputField.GetComponent<InputField>().text = fixedArabic;
    }
}
