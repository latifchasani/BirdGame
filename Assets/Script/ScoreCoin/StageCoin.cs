using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageCoin : MonoBehaviour
{

    private void Update()
    {
        float step = 6 * Time.deltaTime;
        Vector2 targetPosition = new Vector2(-2000, 10);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);
        if (transform.position.x < -10)
        {
            Destroy(this.gameObject);
        }
    }

    // private void OnCollisionEnter2D(Collision2D collision)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "bird")
        {
            Controller.core.currentCoin();

            Destroy(this.gameObject);
        }
    }

}
