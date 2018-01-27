using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AspectRatio : MonoBehaviour {

    public float aspectRatioWanted = 16f / 9f;

    private Vector2 last_resolution;
    private Vector2 current_resolution;

    private void Start()
    {
        UpdateAspectRatio();
        last_resolution = new Vector2(Screen.width, Screen.height);
    }

    private void Update()
    {
        current_resolution.x = Screen.width;
        current_resolution.y = Screen.height;
        if (current_resolution != last_resolution)
        {
            UpdateAspectRatio();
            last_resolution = current_resolution;
        }
        
    }

    private void UpdateAspectRatio()
    {
        float variance = ((float)Screen.width / (float)Screen.height) / aspectRatioWanted;
        if (variance < 1.0f)
        {
            Camera.main.rect = new Rect(0, 0.5f - variance / 2.0f, 1.0f, variance);
        }
        else
        {
            variance = 1.0f / variance;
            Camera.main.rect = new Rect(0.5f - variance / 2.0f, 0.0f, variance, 1.0f);
        }
    }
}
