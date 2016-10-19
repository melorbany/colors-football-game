using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using ArabicSupport;

public class AccountUI : MonoBehaviour {

	public Button playBtn,homeBtn;
    public InputField nameInputField, teamInputField;
	public string gameScene;
	public string mainMenu;


	// Use this for initialization
	void Start () {
		playBtn.GetComponent<Button>().onClick.AddListener(() => { PlayBtn(); });    //play
		homeBtn.GetComponent<Button>().onClick.AddListener(() => { HomeBtn(); });    //rate

    }
	
	// Update is called once per frame
	void Update () {
	
	}


	void PlayBtn()
	{
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
        Debug.Log(fixedArabic);
    }


    public void FixTeamsInputFieldText()
    {
        string fixedArabic = ArabicFixer.Fix(teamInputField.GetComponent<InputField>().text, true, true);
        teamInputField.GetComponent<InputField>().text = fixedArabic;
    }
}
