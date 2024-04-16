using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backgroundScroller : MonoBehaviour
{
    public float scrollSpeed;
    public Vector3 startPos;
    private float repeatWidth;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = startPos;
        repeatWidth = GetComponent<BoxCollider>().size.x / 2;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * scrollSpeed * Time.deltaTime, Space.World);

        if (transform.position[0] <= startPos.x - repeatWidth)
        {
            transform.position = startPos;
        }
    }
}
