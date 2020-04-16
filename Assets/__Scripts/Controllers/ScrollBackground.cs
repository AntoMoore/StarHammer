using UnityEngine;

public class ScrollBackground : MonoBehaviour
{
    // == private fields ==
    [SerializeField] private float scrollSpeed = 0.000001f;
    private Material myMaterial;
    private Vector2 offset;

    // Start is called before the first frame update
    void Start()
    {
        myMaterial = GetComponent<Renderer>().material;
        offset = new Vector2(0f, scrollSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        myMaterial.mainTextureOffset += offset * Time.deltaTime;
    }
}
