using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backgroundScroller : MonoBehaviour
{
    public float scrollSpeed;
    public Vector3 startPos;
    public float endXPos;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = startPos;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * scrollSpeed * Time.deltaTime, Space.World);

        if (transform.position[0] <= endXPos)
        {
            transform.position = startPos;
        }
    }
}
