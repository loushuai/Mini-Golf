using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAdjustor : MonoBehaviour {
    // the design size
    public float standard_width = 576f;
    public float standard_height = 1024f;

    // the screen size
    float device_width = 0f;
    float device_height = 0f;

    private void Awake()
    {
        device_width = Screen.width;
        device_height = Screen.height;

        Debug.LogFormat("width {0}, height{1}", device_width, device_height);

        SetCameraSize();
    }

    void SetCameraSize()
    {
        float adjustor = 0f;
        float standard_aspect = standard_width / standard_height;
        float device_aspect = device_width / device_height;

        float scaleW = standard_width/ device_width;
        float scaleH = standard_height / device_height;

        if (device_aspect < standard_aspect)
        {
            GetComponent<Camera>().orthographicSize *= Mathf.Max(scaleW, scaleH);
        }
    }
}
