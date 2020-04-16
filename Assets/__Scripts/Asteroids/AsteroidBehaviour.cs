using UnityEngine;

public class AsteroidBehaviour : MonoBehaviour
{
    //private variables
    private Rigidbody2D rb;
    private float startPoint;
    private int xDirection;
    private int yDirection;
    private float speed = 1.0f;

    // private methods
    private void Start()
    {
        // get rigidbody from object
        rb = GetComponent<Rigidbody2D>(); 
        // acquire starting spawn point
        startPoint = rb.transform.position.x;
        // get asteroid speed
        speed = GetComponent<Asteroid>().getAsteroidSpeed();
        // move object down vertically
        yDirection = -1;

        //check starting position
        if(startPoint < -4.5f)
        {
            // if starting on left, move right
            xDirection = 1;
            
        }
        else if(startPoint > 4.5f)
        {
            // if starting on left, move right
            xDirection = -1;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        //move asteroid
        rb.velocity = new Vector2(xDirection * (speed / 2), yDirection * speed);
    }
}
