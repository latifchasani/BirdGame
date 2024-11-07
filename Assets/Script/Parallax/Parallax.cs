using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    /*option 3*/
    [SerializeField]
    private float moveSpeed = 1f;

    [SerializeField]
    private float offset;

    private Vector2 startPosition;

    private float newPosition;

    private void Start()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        newPosition = Mathf.Repeat(Time.time * -moveSpeed, offset);

        transform.position = startPosition + Vector2.right * newPosition;


    }

    /*option 1*/

    /*private float length, startpos;
    public float parallaxEffect;
    public GameObject cam;
    float iteration = 0;

    float elapsed;
         int milliseconds { get { return Mathf.FloorToInt(elapsed * 1000); } }

    // Start is called before the first frame update
    void Start()
    {
        startpos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        *//*float temp = (cam.transform.position.x * (1 - parallaxEffect));
        float dist = (cam.transform.position.x * parallaxEffect);

        transform.position = new Vector3(startpos + dist, transform.position.y, transform.position.z);

        if (temp > startpos + length)
        {
            startpos += length;
        }
        else if (temp < startpos - length)
        {
            startpos -= length;
        }*/

    /*float iteration = elapsed += Time.deltaTime;

    float temp = (iteration += Time.deltaTime * (1 - parallaxEffect));
    float dist = (iteration += Time.deltaTime * parallaxEffect);

    transform.position = new Vector3(startpos + dist, transform.position.y, transform.position.z);

    if (temp > startpos + length)
    {
        startpos += length;
    }
    else if (temp < startpos - length)
    {
        startpos -= length;
    }*//*


     Debug.Log("para temp : " + temp);
    // Debug.Log("para cam trans : "+ cam.transform.position.x);
      Debug.Log("para dist : " + dist);
}*/

    /*option 2*/

    /*private float length, startpos;
    public GameObject cam;
    public float parallaxFactor;
    public float PixelsPerUnit;
 
    void Start()
    {
        startpos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }
 
    void Update()
    {
        float temp     = cam.transform.position.x * (1 - parallaxFactor);
        float distance = cam.transform.position.x * parallaxFactor;
 
        Vector3 newPosition = new Vector3(startpos + distance, transform.position.y, transform.position.z);
 
        transform.position = PixelPerfectClamp(newPosition, PixelsPerUnit);
 
        if (temp > startpos + (length / 2))      startpos += length;
        else if (temp < startpos - (length / 2)) startpos -= length;
   }
 
    private Vector3 PixelPerfectClamp(Vector3 locationVector, float pixelsPerUnit)
    {
        Vector3 vectorInPixels = new Vector3(Mathf.CeilToInt(locationVector.x * pixelsPerUnit), Mathf.CeilToInt(locationVector.y * pixelsPerUnit),Mathf.CeilToInt(locationVector.z * pixelsPerUnit));
        return vectorInPixels / pixelsPerUnit;
    }*/
}
