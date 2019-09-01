using UnityEngine;

public class GameController : MonoBehaviour {

    [SerializeField] private int numberOfAsteroids;  //current number of asteroids on the scene
    [SerializeField] private int level;
    [SerializeField] private GameObject asteroid;  //on new level will make new green asteroids

    [SerializeField] private float screenTopBorder;
    [SerializeField] private float screenBottomBorder;
    [SerializeField] private float screenLeftBorder;
    [SerializeField] private float screenRightBorder;

    public float GetScreenTopBorder()
    {
        return screenTopBorder;
    }

    public float GetScreenBottomBorder()
    {
        return screenBottomBorder;
    }

    public float GetScreenLeftBorder()
    {
        return screenLeftBorder;
    }

    public float GetScreenRightBorder()
    {
        return screenRightBorder;
    }

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
                Vector3 spawnPos = new Vector3(Random.Range(screenBottomBorder, screenTopBorder), 0.0f, Random.Range(screenLeftBorder, screenRightBorder));
                Instantiate(asteroid, spawnPos, Quaternion.identity);
                numberOfAsteroids++;
            }
        }
    }

    public void IncreaseNumberOfAsteroids()
    {
        numberOfAsteroids++;
    }
}
