using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Control : MonoBehaviour {
    const float VTH = 0.4f;
    const float trapRange = 0.25f;

    LineRenderer lineRenderer;
    GameObject UI;
    GameObject hole;
    Rigidbody2D rb;

    bool canFire = false;
    Vector3 force = new Vector3(0f, 0f, 0f);
    float maxForce = 3f;
    float scale = 200f;
    int counter = 1;
    float preVelocity = 0f;

    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        // get number of remaining chances
        UI = GameObject.Find("UI");
        hole = GameObject.Find("hole");
        counter = int.Parse(UI.GetComponentInChildren<Text>().text);
        rb = GetComponent<Rigidbody2D>();
    }

    void NextRound()
    {
        if (counter == 1)
        {
            // Lose
        }

        counter--;
        UI.GetComponentInChildren<Text>().text = counter.ToString();
    }

    bool IsReady()
    {
        return rb.velocity.magnitude <= VTH;
    }

    void Pull()
    {
        if (!IsReady())
        {
            return;
        }
        
        Camera c = Camera.main;
        Vector3 mousePos = c.ScreenToWorldPoint(Input.mousePosition);
        force = transform.position - mousePos;
        force.z = 0f;
        if (force.magnitude > maxForce)
        {
            force = force * maxForce / force.magnitude;
        }
        lineRenderer.SetPosition(1, force);

        canFire = true;
    }

    void Fire()
    {
        if (!IsReady() || !canFire)
        {
            return;
        }
        rb.AddForce(force * scale);
        canFire = false;
        Debug.LogFormat("fource {0}", force);
        lineRenderer.SetPosition(1, new Vector3(0f, 0f, 0f));
    }

    void IfStop()
    {
        if (preVelocity > 0f && rb.velocity.magnitude <= VTH)
        {
            rb.velocity = new Vector2(0f, 0f);
            NextRound();
        }

        preVelocity = rb.velocity.magnitude;
    }

    void IfWin()
    {
        Transform holeTrans = hole.GetComponent<Transform>();
        float dist = Vector3.Distance(holeTrans.position, transform.position);
        if (dist <= trapRange && rb.velocity.magnitude <= 2f)
        {
            rb.velocity = new Vector3(0, 0);
            rb.position = holeTrans.position;
            //Win
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else if (dist <= trapRange)
        {
            rb.velocity -= rb.velocity.normalized * 0.5f;
        }
    }

    void HandleInput()
    {
        if (Input.GetMouseButton(0))
        {
            Pull();
        }

        if (Input.GetMouseButtonUp(0))
        {
            Fire();
        }
    }

    // Update is called once per frame
    void Update () {        
        HandleInput();        
    }

    private void FixedUpdate()
    {
        IfWin();
        IfStop();
    }
}
