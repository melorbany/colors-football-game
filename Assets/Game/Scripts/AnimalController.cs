using UnityEngine;
using System.Collections;

public class AnimalController : MonoBehaviour {

    public static AnimalController instance;

    [SerializeField]
    private int maxUpForce;             //max force with which the animal obj moves up
    [SerializeField]
    private int minUpForce;             //min force with which the animal obj moves up
    private Rigidbody2D myBody;         //ref to rigidbody component on the game object
    private Collider2D objectCollider;  //ref to collider component on the game object
    [HideInInspector]
    public bool applyForce = false;     //decide when to apply up force

    [SerializeField]
    private Sprite[] img; //0 animal,1 football,2 poolball, 3 pokeball

    void Awake()
    {
        if (instance = null)
        {
            instance = this;
        }
    }

	// Use this for initialization
	void Start ()
    {
        //we get the rigidbody attach to the object
        myBody = GetComponent<Rigidbody2D>();
        //we get the collider attach to the object
        objectCollider = GetComponent<Collider2D>();
        //we get the sprite renderer attach to the childe object of this object
        SpriteRenderer image = transform.GetChild(0).GetComponent<SpriteRenderer>();
        //we change tile depending on the selection
        image.sprite = img[GameManager.instance.textureStyle];

    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        //when game is over we dont apply any forces on the object
        if (GameManager.instance.isGameOver)
        {
            myBody.isKinematic = true;
            return;
        }

        //when applyforce is true we apply forces
        if (applyForce)
        {
            //we take the random value between the force limits
            int upForce = Random.Range(minUpForce, maxUpForce);
            //add force to the rigidbody
            myBody.AddForce(Vector3.up * upForce );
            //and make apply force false so that the force is applied only once 
            applyForce = false;
        }
        //here we check the object velocity 
        //and if its greater than zero we make collider off(> 0 means going up)
        if (myBody.velocity.y > 0)
        {
            objectCollider.enabled = false;
        }
        else // if its less than zero we make it on(< 0 means going down)
        {
            objectCollider.enabled = true;
        }
        //if the object goes below the limited y values we make it deactive
        if (transform.position.y < -9f)
        {
            gameObject.SetActive(false);
        }
	}
}
