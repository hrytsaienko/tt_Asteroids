using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidsController : MonoBehaviour
{
    public float maxPush;
    public float maxSpin;
    public Rigidbody rb;

    public GameObject blueAsteroid;
    public GameObject violetAsteroid;
    public GameObject redAsteroid;

    public float screenTopBorder;
    public float screenBottomBorder;
    public float screenLeftBorder;
    public float screenRightBorder;
    public int asteroidColor; // 3 - green,  2 - blue,  1 - violet,  0 - red

    public GameController gc;
    
    void Start () {
        //Rendom movement of asteroids
        Vector3 push = new Vector3(Random.Range(-maxPush, maxPush), 0.0f, Random.Range(-maxPush, maxPush));
        float torque = Random.Range(-maxSpin, maxSpin);

        rb.AddForce(push);
        rb.AddTorque(new Vector3(torque, 0.0f, torque));

        gc = GameObject.FindObjectOfType<GameController>();
	}
	
	// Update is called once per frame
	void Update () {

        //Screen wraping. If asteroid goes out of the screen border it will show up on the other side
        Vector3 newPosition = transform.position;
        //Check Top of the screen
        if (transform.position.z > screenTopBorder)
        {
            newPosition.z = screenBottomBorder;
        }
        //Check bottom of the screen
        if (transform.position.z < screenBottomBorder)
        {
            newPosition.z = screenTopBorder;
        }
        //Check right site of the screen
        if (transform.position.x > screenRightBorder)
        {
            newPosition.x = screenLeftBorder;
        }
        //Check left side of the screen
        if (transform.position.x < screenLeftBorder)
        {
            newPosition.x = screenRightBorder;
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
            if (asteroidColor == 3)
            {
                //Spaw blue asteroid
                Instantiate(blueAsteroid, transform.position, transform.rotation);
            }
            if (asteroidColor == 2)
            {
                //Spaw violet asteroid
                Instantiate(violetAsteroid, transform.position, transform.rotation);
            }
            if (asteroidColor == 1)
            {
                //Spaw red asteroid
                Instantiate(redAsteroid, transform.position, transform.rotation);
            }
            if (asteroidColor == 0)
            {
                //Get asteroids count - 1
                gc.UpdateNumberOfAsteroids();
            }

            Destroy(gameObject);
        }
        
    }
}
