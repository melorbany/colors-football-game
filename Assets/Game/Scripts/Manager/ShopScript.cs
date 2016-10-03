using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShopScript : MonoBehaviour {

    public static ShopScript instance;
    public Text totalPoint;
    public GameObject shopPanel;
    public Button shopCloseBtn, animalBtn, footballBtn, pokeballBtn, poolballBtn, shopBtn;

    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    // Use this for initialization
    void Start ()
    {
        totalPoint.text = "Point " + GameManager.instance.points;

        shopBtn.GetComponent<Button>().onClick.AddListener(() => { ShopBtn(); });    //shop
        shopCloseBtn.GetComponent<Button>().onClick.AddListener(() => { ShopCloseBtn(); });    //shopclose

        //shop
        animalBtn.GetComponent<Button>().onClick.AddListener(() => { Animal(); });    //shop animal btn
        footballBtn.GetComponent<Button>().onClick.AddListener(() => { Football(); });    //shop football btn
        poolballBtn.GetComponent<Button>().onClick.AddListener(() => { Poolball(); });    //shop poolball btn
        pokeballBtn.GetComponent<Button>().onClick.AddListener(() => { Pokeball(); });    //shop pokeball btn

        TextureTrack();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void TextureTrack()
    {
        if (GameManager.instance.textureUnlocked[1])
        {
            footballBtn.transform.GetChild(1).GetComponent<Text>().text = "";
        }

        if (GameManager.instance.textureUnlocked[2])
        {
            poolballBtn.transform.GetChild(1).GetComponent<Text>().text = "";
        }

        if (GameManager.instance.textureUnlocked[3])
        {
            pokeballBtn.transform.GetChild(1).GetComponent<Text>().text = "";
        }
    }

    void ShopBtn()
    {
        shopPanel.SetActive(true);
    }

    void ShopCloseBtn()
    {
        shopPanel.SetActive(false);
    }

    //buy buttons

    void Animal()
    {
        if (GameManager.instance.textureStyle != 0)
        {
            GameManager.instance.textureStyle = 0;
            GameManager.instance.Save();
            shopPanel.SetActive(false);
        }
    }

    void Football()
    {
        if (GameManager.instance.textureUnlocked[1] && GameManager.instance.textureStyle != 1)
        {
            GameManager.instance.textureStyle = 1;
            GameManager.instance.Save();
            shopPanel.SetActive(false);
        }
        else
        {
            if (GameManager.instance.points >= 50)
            {
                GameManager.instance.points -= 50;
                GameManager.instance.textureUnlocked[1] = true;
                GameManager.instance.textureStyle = 1;
                GameManager.instance.Save();
                footballBtn.transform.GetChild(1).GetComponent<Text>().text = "";
                shopPanel.SetActive(false);
            }
            else
            {
                Debug.Log("No Points");
                //pop iap menu
            }
        }
    }

    void Poolball()
    {
        if (GameManager.instance.textureUnlocked[2] && GameManager.instance.textureStyle != 2)
        {
            GameManager.instance.textureStyle = 2;
            GameManager.instance.Save();
            shopPanel.SetActive(false);
        }
        else
        {
            if (GameManager.instance.points >= 100)
            {
                GameManager.instance.points -= 100;
                GameManager.instance.textureUnlocked[2] = true;
                GameManager.instance.textureStyle = 2;
                GameManager.instance.Save();
                poolballBtn.transform.GetChild(1).GetComponent<Text>().text = "";
                shopPanel.SetActive(false);
            }
            else
            {
                Debug.Log("No Points");
                //pop iap menu
            }
        }
    }

    void Pokeball()
    {
        if (GameManager.instance.textureUnlocked[3] && GameManager.instance.textureStyle != 3)
        {
            GameManager.instance.textureStyle = 3;
            GameManager.instance.Save();
            shopPanel.SetActive(false);
        }
        else
        {
            if (GameManager.instance.points >= 150)
            {
                GameManager.instance.points -= 150;
                GameManager.instance.textureUnlocked[3] = true;
                GameManager.instance.textureStyle = 3;
                GameManager.instance.Save();
                pokeballBtn.transform.GetChild(1).GetComponent<Text>().text = "";
                shopPanel.SetActive(false);
            }
            else
            {
                Debug.Log("No Points");
                //pop iap menu
            }
        }
    }

}
