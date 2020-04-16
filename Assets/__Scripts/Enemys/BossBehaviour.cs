using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehaviour : MonoBehaviour
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
        yDirection = -1;

        // parent object for bullets (container)
        bulletParent = GameObject.Find("BulletParent");
        if (!bulletParent)
        {
            bulletParent = new GameObject("BulletParent");
        }

        // fire guns a set intervals
        InvokeRepeating("FireGun", fireDelay, rateOfFire);

        // move going forward
        Invoke("StopAdvancing", 9f);
        
        // move left and right
        InvokeRepeating("MoveRight", 10f, 2f);
        InvokeRepeating("MoveLeft", 11f, 2f);
    }

    private void Update()
    {
        // move object
        rb.velocity = new Vector2(xDirection * speed, yDirection * speed);
    }

    private void StopAdvancing()
    {
        this.yDirection = 0;
    }

    private void MoveLeft()
    {
        xDirection = -1;
    }

    private void MoveRight()
    {
        xDirection = 1;
    }
}
