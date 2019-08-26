using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public int numberOfAsteroids;  //current number of asteroids on the scene
    public int level;
    public  GameObject asteroid;  //on new level will make new green asteroids

    public void UpdateNumberOfAsteroids()
    {
        numberOfAsteroids--;

        //Check if scene have any asteroids left
        if (numberOfAsteroids <= 0)
        {
            //Go to next level
            level++;

            //Spawn new asteroids
            for (int i = 0; i < level*2; i++)
            {
                Vector3 spawnPos = new Vector3(Random.Range(-45.0f, 45.0f), 0.0f, Random.Range(-58.0f, 58.0f));
                Instantiate(asteroid, spawnPos, Quaternion.identity);
                numberOfAsteroids++;
            }
        }
    }
}
