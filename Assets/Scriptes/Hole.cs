using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : MonoBehaviour {
    float trapRange = 0.25f;
	
	// Update is called once per frame
	void Update () {
        GameObject ball = GameObject.Find("ball");
        Transform ballTrans = ball.GetComponent<Transform>();
        Rigidbody2D ballRigid = ball.GetComponent<Rigidbody2D>();
        float dist = Vector3.Distance(ballTrans.position, transform.position);
        if (dist <= trapRange && ballRigid.velocity.magnitude <= 2f)
        {
            ballRigid.velocity = new Vector3(0, 0, 0);
            ballTrans.position = transform.position;
        }
        else if (dist <= trapRange)
        {
            ballRigid.velocity -= ballRigid.velocity.normalized * 0.5f;
        }
    }
}
