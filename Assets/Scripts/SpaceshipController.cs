using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipController : MonoBehaviour {

    [SerializeField] private Rigidbody rb;
    [SerializeField] private float speed;       //Speed of moving forward
    [SerializeField] private float turnPush;    //Force of turning ship

    //[SerializeField] private float screenTopBorder;
    //[SerializeField] private float screenBottomBorder;
    //[SerializeField] private float screenLeftBorder;
    //[SerializeField] private float screenRightBorder;

    [SerializeField] private float shotForce;
    [SerializeField] private GameObject shot;

    public Animator spinAnimation;
    private GameController gameControer;

    // Use this for initialization
    void Start () {
        spinAnimation.enabled = false;
        gameControer = GameObject.FindObjectOfType<GameController>();
    }
	
	// Update is called once per frame
	void Update () {

        //Check for input making shots
        if (Input.GetButtonDown("Fire1"))
        {
            GameObject newShot = Instantiate(shot, transform.position, transform.rotation);
            newShot.GetComponent<Rigidbody>().AddRelativeForce(Vector3.up * shotForce);
            Destroy(newShot, 5.0f);
        }

        //Screen wraping. If spaceship goes out of the screen border it will show up on the other side
        Vector3 newPosition = transform.position;
        //Check Top of the screen
        if (transform.position.z > gameControer.GetScreenTopBorder())
        {
            newPosition.z = gameControer.GetScreenBottomBorder();
        }
        //Check bottom of the screen
        if (transform.position.z < gameControer.GetScreenBottomBorder())
        {
            newPosition.z = gameControer.GetScreenTopBorder();
        }
        //Check right site of the screen
        if (transform.position.x > gameControer.GetScreenRightBorder())
        {
            newPosition.x = gameControer.GetScreenLeftBorder();
        }
        //Check left side of the screen
        if (transform.position.x < gameControer.GetScreenLeftBorder())
        {
            newPosition.x = gameControer.GetScreenRightBorder();
        }
        transform.position = newPosition;

        //Get spin animation
        //Chech for key is up to stop animation
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.W))
        {
            spinAnimation.SetInteger("SortOfSpin", 0);            
        }
        //Check for key input input to start spin animation
        if (Input.GetKeyDown(KeyCode.A))
        {
            spinAnimation.enabled = true;
            spinAnimation.SetInteger("SortOfSpin", 1);    // 1 equels negative spin on Y animation
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            spinAnimation.enabled = true;
            spinAnimation.SetInteger("SortOfSpin", 3);    // 3 equels positive spin on Y animation
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            spinAnimation.enabled = true;
            spinAnimation.SetInteger("SortOfSpin", 4);    // 4 equels positive spin on X animation
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            spinAnimation.enabled = true;
            spinAnimation.SetInteger("SortOfSpin", 2);    // 2 equels negative spin on X animation 
        }

    }

    void FixedUpdate()
    {
        float moveVertical = Input.GetAxis("Vertical");
        float moveHorizontal = Input.GetAxis("Horizontal");

        if (Input.GetButton("Horizontal"))
        {
            spinAnimation.enabled = false;                                  // disable animator in order to be able to turn the spaceship
            rb.transform.Rotate(new Vector3(0.0f, 0.0f, moveHorizontal * turnPush));   // turn the spaceship
        }

        rb.AddRelativeForce(Vector3.up * moveVertical * speed);             // move forward the spaceship      
    }
}
