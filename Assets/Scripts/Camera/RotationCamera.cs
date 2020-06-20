using UnityEngine;

public class RotationCamera : MonoBehaviour
{
    public static RotationCamera _instance;

    public float interpVelocity;

    [HideInInspector]
    public bool Lerp;

    // Use this for initialization
    [HideInInspector]
    public bool roration;

    public Camera mainCamera;

    // zoom in camera
    private float zoomTime = 5;

    [HideInInspector]
    public float zoom1, zoom2;

    public bool isZoomin, isZoomOut;

    public Animator _animCam;

    private void Awake()
    {
        zoom1 = 3.5f; zoom2 = 4.2f;
        mainCamera.orthographic = true;
        if (_instance == null)
        {
            _instance = this;
        }
    }

    public bool rayShoot, rayShootNguoc;
    public bool right, left, mid;
    public int state;

    public bool vedich;

    private void Update()
    {
        float zoomerTime = zoomTime * Time.deltaTime;

        if (isZoomin)
        {
            zoom1 = 3.3f;
            mainCamera.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, zoom1, zoomerTime);
            //zoomerTime = 0f;
            // Debug.Log(mainCamera.orthographicSize);
        }
        else if (isZoomOut)
        {
            zoom2 = 4.2f;
            mainCamera.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, zoom2, zoomerTime);
        }
        if (rayShootNguoc)
        {
            XoayNguoc();
        }
        if (rayShoot)
        {
            XoayXuoi();
        }
        if (!Input.GetMouseButtonDown(0)) return;
        ChangeColorCamera._instance.zoomIn = false;
        ChangeColorCamera._instance.transition = false;

        switch (state)
        {
            case 1:
                rayShoot = true;
                if (PlayerController._instance.MoveMent.collider != null)
                {
                    if (PlayerController._instance.MoveMent.collider.transform.rotation.eulerAngles.z == 270)
                    {
                        left = false;
                        right = true;
                        mid = false;
                    }
                    else if (PlayerController._instance.MoveMent.collider.transform.rotation.eulerAngles.z == 0)
                    {
                        left = false;
                        right = false;
                        mid = true;
                    }
                    else if (PlayerController._instance.MoveMent.collider.transform.rotation.eulerAngles.z == 90)
                    {
                        left = true;
                        right = false;
                        mid = false;
                    }
                }
                break;

            case 2:
                rayShootNguoc = true;
                if (PlayerController._instance.MoveMent.collider != null)
                {
                    if (PlayerController._instance.MoveMent.collider.transform.rotation.eulerAngles.z == 270)
                    {
                        left = true;
                        right = true;
                        mid = false;
                    }
                    else if (PlayerController._instance.MoveMent.collider.transform.rotation.eulerAngles.z == 0)
                    {
                        left = false;
                        right = false;
                        mid = true;
                    }
                    else if (PlayerController._instance.MoveMent.collider.transform.rotation.eulerAngles.z == 90)
                    {
                        left = true;
                        right = false;
                        mid = false;
                    }
                }
                break;

            case 3:
                if (mainCamera.orthographicSize >= 4.1)
                {
                    isZoomin = true;
                    isZoomOut = false;
                }
                if (mainCamera.orthographicSize >= 3.1 && mainCamera.orthographicSize < 4.1)
                {
                    isZoomOut = true;
                    isZoomin = false;
                }
                break;
        }
    }

    private void XoayXuoi()
    {
        if (!PlayerController._instance.MoveMent) return;
        if (PlayerController._instance.MoveMent.collider == null) return;
        if (PlayerController._instance.MoveMent.collider.transform.rotation.eulerAngles.z == 270 && right)
        {
            _animCam.SetBool("Right", true);
        }
        else if (PlayerController._instance.MoveMent.collider.transform.rotation.eulerAngles.z == 0 && mid)
        {
            if (_animCam.GetCurrentAnimatorStateInfo(0).IsName("Rotation"))
            {
                _animCam.SetBool("Right", false);
            }
            if (_animCam.GetCurrentAnimatorStateInfo(0).IsName("Left"))
            {
                _animCam.SetBool("Left", false);
            }
        }
        else if (PlayerController._instance.MoveMent.collider.transform.rotation.eulerAngles.z == 90 && left)
        {
            _animCam.SetBool("Left", true);
        }
    }

    private void XoayNguoc()
    {
        if (!PlayerController._instance.MoveMent) return;
        if (PlayerController._instance.MoveMent.collider == null) return;
        if (PlayerController._instance.MoveMent.collider.transform.rotation.eulerAngles.z == 270 && right)
        {
            _animCam.SetBool("Left", true);
        }
        else if (PlayerController._instance.MoveMent.collider.transform.rotation.eulerAngles.z == 0 && mid)
        {
            if (_animCam.GetCurrentAnimatorStateInfo(0).IsName("Rotation"))
            {
                _animCam.SetBool("Right", false);
            }
            if (_animCam.GetCurrentAnimatorStateInfo(0).IsName("Left"))
            {
                _animCam.SetBool("Left", false);
            }
        }
        else if (PlayerController._instance.MoveMent.collider.transform.rotation.eulerAngles.z == 90 && left)
        {
            _animCam.SetBool("Right", true);
        }
    }
}