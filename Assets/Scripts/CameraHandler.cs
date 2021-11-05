using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    public GameObject firstPersonCamera;
    public GameObject overheadCamera;
    public bool isFirstToggled = true;

    // Call this function to disable FPS camera,
    // and enable overhead camera.

    public void Toggle()
    {
        if(isFirstToggled)
        {
            ShowOverheadView();
        }
        else
        {
            ShowFirstPersonView();
        }
        isFirstToggled = !isFirstToggled;

    }
    public void ShowOverheadView()
    {
        overheadCamera.SetActive(true);
        firstPersonCamera.SetActive(false);
    }

    // Call this function to enable FPS camera,
    // and disable overhead camera.
    public void ShowFirstPersonView()
    {
        firstPersonCamera.SetActive(true);
        overheadCamera.SetActive(false);
    }
}
