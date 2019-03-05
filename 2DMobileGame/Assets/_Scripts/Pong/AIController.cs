using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour {

    public Rigidbody rb;

    private float speed;

    private void Update()
    {
        speed = rb.velocity.x / 4;//speed is based on how fast the ball is going
        if(speed < 0)//used so when the ball has negative velocity
        {
            speed = -speed;
        }
        float target = rb.position.y;//finds the y position of the ball
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position,new Vector3(4.5f, target, 0), 0.02f * speed);//go towards the y position of the ball
    }
}