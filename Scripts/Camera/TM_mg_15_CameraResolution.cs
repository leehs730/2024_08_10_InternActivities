using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TM_mg_15_CameraResolution : MonoBehaviour
{
    // 원하는 고정된 화면 비율 (예: 9:16 비율)
    private float targetAspect = 9.0f / 16.0f;

    private void Awake()
    {
        Screen.SetResolution(1080, 1920, true);
    }

    // Start is called before the first frame update
    void Start()
    {
        AdjustAspectRatio();
    }

    void OnPreCull() => GL.Clear(true, true, Color.black);

    private void AdjustAspectRatio()
    {
        float windowAspect = (float)Screen.width / (float)Screen.height;
        float scaleHeight = windowAspect / targetAspect;

        Camera camera = Camera.main;

        if (scaleHeight < 1.0f)
        {
            Rect rect = camera.rect;

            rect.width = 1.0f;
            rect.height = scaleHeight;
            rect.x = 0;
            rect.y = (1.0f - scaleHeight) / 2.0f;

            camera.rect = rect;
        }
        else
        {
            float scaleWidth = 1.0f / scaleHeight;

            Rect rect = camera.rect;

            rect.width = scaleWidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scaleWidth) / 2.0f;
            rect.y = 0;

            camera.rect = rect;
        }
    }
    
}
