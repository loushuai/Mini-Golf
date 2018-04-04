using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resolution : MonoBehaviour {
    public Camera mainCamera;
    // the design size
    public float target_width = 768f;
    public float target_height = 1024f;
    // the screen size
    float device_width = 0f;
    float device_height = 0f;

    private void Awake() {
        device_width = Screen.width;
        device_height = Screen.height;

        Debug.LogFormat("width {0}, height{1}", device_width, device_height);
    }

    // Use this for initialization
    void Start () {
        float scale_w = device_width / target_width;
        float scale_h = device_height / target_height;
        float scale = Mathf.Max(scale_w, scale_h);
        GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize *= scale;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
