using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plasma : MonoBehaviour
{
    // == member variables ==
    [SerializeField] public int plasmaDamage = 10;

    // == On Collision ==
    private void OnTriggerEnter2D(Collider2D whatHitMe)
    {
        var player = whatHitMe.GetComponent<Player>();

        if(player)
        {
            // destroy bullet
            Destroy(gameObject);
        }  
    }
}
