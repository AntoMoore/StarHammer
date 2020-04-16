using UnityEngine;

// make rigidBody a
[RequireComponent(typeof(Rigidbody2D))]
public class IconBehaviour : MonoBehaviour
{
    //private variables
    private Rigidbody2D rb;
    [SerializeField]private float speed = 1.5f;

    // private methods
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(0,-1 * speed);
    }
}

