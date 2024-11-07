using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bird : MonoBehaviour
{
    public Controller controller;
    public Transform birdImage;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "ground" || collision.transform.tag == "ground_2")
        {
            Debug.Log("ground test");
            controller.gameOver();
        }

        if (collision.transform.tag == "pipe")
        {
            Debug.Log("pipe test");
            controller.gameOver();
        }
    }

        private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.transform.tag == "pipe_mid")
        {
            controller.incrementPoint();
            controller.finalIncrementPoint();            
        }
    }
}
