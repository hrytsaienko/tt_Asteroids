using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidsController : MonoBehaviour
{
    [SerializeField] private float maxPush;
    [SerializeField] private float maxSpin;
    [SerializeField] private GameObject blueAsteroid;
    [SerializeField] private GameObject violetAsteroid;
    [SerializeField] private GameObject redAsteroid;
    [SerializeField] private AsteroidType asteroidColor;

    private Rigidbody rigidbody;
    private GameController gameController;
    
    void Start () {
        //Rendom movement of asteroids
        Vector3 push = new Vector3(Random.Range(-maxPush, maxPush), 0.0f, Random.Range(-maxPush, maxPush));
        float torque = Random.Range(-maxSpin, maxSpin);

        rigidbody = gameObject.GetComponent<Rigidbody>();
        rigidbody.AddForce(push);
        rigidbody.AddTorque(new Vector3(torque, 0.0f, torque));

        gameController = GameObject.FindObjectOfType<GameController>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        Vector3 newPosition = transform.position;
        ScreenWrapingCheck(newPosition);   //Screen wraping. If asteroid goes out of the screen border it will show up on the other side
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Shot"))
        {
            //Destroy shot
            Destroy(other.gameObject);
            //Check state of asteroid
            if (asteroidColor == AsteroidType.GREEN)
            {
                ChangeAsteroid(blueAsteroid);                
            }
            else if (asteroidColor == AsteroidType.BLUE)
            {
                ChangeAsteroid(violetAsteroid);
            }
            else if(asteroidColor == AsteroidType.VIOLET)
            {
                ChangeAsteroid(redAsteroid);
            }
            else if(asteroidColor == AsteroidType.RED)
            {
                gameController.UpdateNumberOfAsteroids();
            }

            Destroy(gameObject);
        }
        
    }

    void ChangeAsteroid(GameObject newAsteroid)
    {
        Instantiate(newAsteroid, transform.position, transform.rotation);
    }

    void ScreenWrapingCheck(Vector3 newPosition)
    {
        
        //Check Top of the screen
        if (transform.position.z > gameController.GetScreenTopBorder())
        {
            newPosition.z = gameController.GetScreenBottomBorder();
        }
        //Check bottom of the screen
        if (transform.position.z < gameController.GetScreenBottomBorder())
        {
            newPosition.z = gameController.GetScreenTopBorder();
        }
        //Check right site of the screen
        if (transform.position.x > gameController.GetScreenRightBorder())
        {
            newPosition.x = gameController.GetScreenLeftBorder();
        }
        //Check left side of the screen
        if (transform.position.x < gameController.GetScreenLeftBorder())
        {
            newPosition.x = gameController.GetScreenRightBorder();
        }
        if (transform.position.y != 0)
            newPosition.y = 0;
        transform.position = newPosition;
    }
}
