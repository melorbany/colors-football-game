using UnityEngine;
using System.Collections;
//using Facebook.Unity;              //..............................uncomment this when you import facebook sdk
using System.Collections.Generic;
using System.Linq;

public class FacebookShare : MonoBehaviour {

    public static FacebookShare instance;

    //..............................................................for facebook
    [Header("For Facebook")]
    // Custom Share Link
    [SerializeField]
    private string shareLinkAndroid = "";
    [SerializeField]
    private string shareLinkiOS = "";
    [SerializeField]
    private string shareTitle = "Link Title";
    [SerializeField]
    private string shareDescription = "Link Description";

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        //FacebookInit();      //uncomment after importing facebook sdk
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    //.........................................................................Facebook
    /*    //...............................remove this line when you import facebook sdk
    void FacebookInit()
    {
        if (!FB.IsInitialized)
        {
            // Initialize the Facebook SDK
            FB.Init(InitCallback, OnHideUnity);
        }
        else
        {
            // Already initialized, signal an app activation App Event
            FB.ActivateApp();
        }
    }

    private void InitCallback()
    {
        if (FB.IsInitialized)
        {
            // Signal an app activation App Event
            FB.ActivateApp();
            // Continue with Facebook SDK
            // ...
        }
        else
        {
            Debug.Log("Failed to Initialize the Facebook SDK");
        }
    }

    private void OnHideUnity(bool isGameShown)
    {
        if (!isGameShown)
        {
            // Pause the game - we will need to hide
            Time.timeScale = 0;
        }
        else
        {
            // Resume the game - we're getting focus again
            Time.timeScale = 1;
        }
    }

    private void AuthCallback(ILoginResult result)
    {
        if (FB.IsLoggedIn)
        {
            // AccessToken class will have session details
            var aToken = Facebook.Unity.AccessToken.CurrentAccessToken;
            // Print current access token's User ID
            Debug.Log(aToken.UserId);
            // Print current access token's granted permissions
            foreach (string perm in aToken.Permissions)
            {
                Debug.Log(perm);
            }
        }
        else
        {
            Debug.Log("User cancelled login");
        }
    }

    private void ShareCallback(IShareResult result)
    {
        if (result.Cancelled || !String.IsNullOrEmpty(result.Error))
        {
            Debug.Log("ShareLink Error: " + result.Error);
        }
        else if (!String.IsNullOrEmpty(result.PostId))
        {
            // Print post identifier of the shared content
            Debug.Log(result.PostId);
        }
        else
        {
            // Share succeeded without postID
            Debug.Log("ShareLink success!");
        }
    }

    //use for facebook login
    public void FacebookLogin()
    {
        var perms = new List<string>() { "public_profile", "email", "user_friends" };
        FB.LogInWithReadPermissions(perms, AuthCallback);
    }

    //use to share link , we use this in out fb button
    public void FBShareLink()
    {
        if (FB.IsLoggedIn)
        {
#if UNITY_ANDROID
            FB.ShareLink(new Uri(shareLinkAndroid), shareTitle, shareDescription, callback: ShareCallback);
#elif UNITY_IOS
            FB.ShareLink(new Uri(shareLinkiOS), shareTitle, shareDescription, callback: ShareCallback);
#endif
        }
        else
        {
            FacebookLogin();
#if UNITY_ANDROID
            FB.ShareLink(new Uri(shareLinkAndroid), shareTitle, shareDescription, callback: ShareCallback);
#elif UNITY_IOS
            FB.ShareLink(new Uri(shareLinkiOS), shareTitle, shareDescription, callback: ShareCallback);
#endif
        }
    }
    */  //...............................remove this line when you import facebook sdk
        //.........................................................................Facebook


}
