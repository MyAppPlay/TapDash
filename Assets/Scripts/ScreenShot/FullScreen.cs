using UnityEngine;

public class FullScreen : MonoBehaviour
{
    // Use this for initialization

    private int _defWidth;

    private int _defHeight;

    public void Awake()
    {
        _defWidth = Screen.width;
        _defHeight = Screen.height;
        if (!Application.isEditor)
        {
            Destroy(this);
        }
    }

    public void ChangeFullScreen()
    {
        if (!Screen.fullScreen)
        {
            Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, true);
        }
        else
        {
            Screen.SetResolution(_defWidth, _defHeight, false);
        }
    }
}

// Update is called once per frame