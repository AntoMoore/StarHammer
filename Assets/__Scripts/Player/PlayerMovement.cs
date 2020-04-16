using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // public variables

    // private variables
    private float hMovement;
    private float vMovement;
    private Rigidbody2D rb;
    [SerializeField][Range(1.0f, 10.0f)]private float speed = 5.0f;
    private Vector2 touchPosition;
    private Vector2 currentPosition;

    //public methods

    // private methods


    // Start is called before the first frame update
    void Start()
    {
        //define rigid body
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // touchScreen controls (Used for Android)
        if(Input.touchCount > 0)
        {
            for(int i = 0; i < Input.touchCount; i++)
            {
                touchPosition = Camera.main.ScreenToWorldPoint(Input.touches[i].position);
                currentPosition = transform.position;
                Debug.DrawLine(currentPosition, touchPosition, Color.red);
                transform.position = Vector2.MoveTowards(currentPosition, touchPosition, speed * Time.deltaTime); 
            }
        }
        
        // Keyboard/Mouse controls (Windows/Linux PC)
        if(Input.anyKey == true)
        {
            vMovement = Input.GetAxis("Vertical");
            hMovement = Input.GetAxis("Horizontal"); 
        }

        // No Inputs - slow down ship
        if(Input.anyKey == false && Input.touchCount == 0)
        {
            reduceSpeed();
        }
        
        // apply velocity/force
        rb.velocity = new Vector2(hMovement * speed ,vMovement * speed); 

        //keep player on the screen
        float yValue = Mathf.Clamp(rb.position.y, -6.5f, 6.5f);
        float xValue = Mathf.Clamp(rb.position.x, -4.1f, 4.1f); 
        rb.position = new Vector2(xValue, yValue);
        
    }//update

    void reduceSpeed()
    {
        // gradually reduce ve by 5%
        if(vMovement > 0 || vMovement < 0 )
        {
            vMovement = (vMovement * 0.95f);
        }
        
        if(hMovement > 0 || hMovement < 0 )
        {
            hMovement = (hMovement * 0.95f);
        }
    }//slowShipDown
}//class
