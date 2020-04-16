using UnityEngine;

public class Sortie : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        // if all hostiles in sortie are destroyed, destroy parent gameobject
        if(gameObject.transform.childCount == 0)
        {
            Destroy(gameObject);
        }
    }
}
