using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidsController : MonoBehaviour
{
    [SerializeField] private float maxPush;
    [SerializeField] private float maxSpin;
    [SerializeField] private Rigidbody rb;

    [SerializeField] private GameObject blueAsteroid;
    [SerializeField] private GameObject violetAsteroid;
    [SerializeField] private GameObject redAsteroid;

    //[SerializeField] private float screenTopBorder;
    //[SerializeField] private float screenBottomBorder;
    //[SerializeField] private float screenLeftBorder;
    //[SerializeField] private float screenRightBorder;
    [SerializeField] private AsteroidType asteroidColor;

    private GameController gameControer;
    
    void Start () {
        //Rendom movement of asteroids
        Vector3 push = new Vector3(Random.Range(-maxPush, maxPush), 0.0f, Random.Range(-maxPush, maxPush));
        float torque = Random.Range(-maxSpin, maxSpin);

        rb.AddForce(push);
        rb.AddTorque(new Vector3(torque, 0.0f, torque));

        gameControer = GameObject.FindObjectOfType<GameController>();
	}
	
	// Update is called once per frame
	void Update () {

        //Screen wraping. If asteroid goes out of the screen border it will show up on the other side
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
                //Spaw blue asteroid
                Instantiate(blueAsteroid, transform.position, transform.rotation);
                Instantiate(blueAsteroid, transform.position, transform.rotation);

                gameControer.IncreaseNumberOfAsteroids();
            }
            else if (asteroidColor == AsteroidType.BLUE)
            {
                //Spaw violet asteroid
                Instantiate(violetAsteroid, transform.position, transform.rotation);
                Instantiate(violetAsteroid, transform.position, transform.rotation);

                gameControer.IncreaseNumberOfAsteroids();
            }
            else if(asteroidColor == AsteroidType.VIOLET)
            {
                //Spaw red asteroid
                Instantiate(redAsteroid, transform.position, transform.rotation);
                Instantiate(redAsteroid, transform.position, transform.rotation);

                gameControer.IncreaseNumberOfAsteroids();
            }
            else if(asteroidColor == AsteroidType.RED)
            {
                //Get asteroids count - 1
                gameControer.UpdateNumberOfAsteroids();
            }

            Destroy(gameObject);
        }
        
    }
}
