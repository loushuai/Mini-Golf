using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Friction : MonoBehaviour {
    public float deceleration = 0.05f;

    // Update is called once per frame
    void Update () {
		Rigidbody2D rb = GetComponent<Rigidbody2D>();
        //Debug.LogFormat("{0}", Time.deltaTime);
        if (rb.velocity.magnitude > 0.01)
            rb.velocity = Vector3.Lerp(new Vector3(0, 0, 0), rb.velocity, (rb.velocity.magnitude - deceleration) / rb.velocity.magnitude);
	}
}
