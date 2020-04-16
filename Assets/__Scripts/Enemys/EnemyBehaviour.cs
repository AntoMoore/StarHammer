using UnityEngine;

// make rigidBody a requirement
[RequireComponent(typeof(Rigidbody2D))]
public class EnemyBehaviour : MonoBehaviour
{
    //private variables
    [SerializeField] private Plasma bulletPrefab;
    [SerializeField] private float rateOfFire = 5f;
    [SerializeField] private float fireDelay = 5f;
    [SerializeField] private float bulletSpeed = 5f;
    [SerializeField] private float speed = 1f;

    private Rigidbody2D rb;
    private float startPoint;
    private int xDirection;
    private int yDirection;
    //private float speed;
    private GameObject bulletParent;
    //private GameObject gun;

    // private methods
    private void FireGun()
    {
        // create a bullet at the players gun position
        Plasma bullet = Instantiate(bulletPrefab, bulletParent.transform);
        bullet.transform.position = transform.position;

        // get rigidbody and apply speed to bullet
        Rigidbody2D rb1 = bullet.GetComponent<Rigidbody2D>();
        rb1.velocity = Vector2.down * bulletSpeed;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //speed = GetComponent<Enemy>().getEnemySpeed();
        startPoint = rb.transform.position.x;
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

        // parent object for bullets (container)
        bulletParent = GameObject.Find("BulletParent");
        if (!bulletParent)
        {
            bulletParent = new GameObject("BulletParent");
        }

        // fire guns a set intervals
        InvokeRepeating("FireGun", fireDelay, rateOfFire);
    }

    private void Update()
    {
        rb.velocity = new Vector2(xDirection * (speed / 2), yDirection * speed);
    }
}
