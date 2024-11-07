using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    private Rigidbody2D rb;
    public float width;
    private float speed = -3f;
    public GameObject parentPipe;
    public float batasAtas, batasBawah;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.velocity = new Vector2(speed, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < -width)
        {
            Destroy(this.gameObject);
        }
    }

    /*private void Reposition()
    {
        Vector2 vector = new Vector2(width * 1f, 0);
        transform.position = (Vector2)transform.position + vector;
    }*/
}
