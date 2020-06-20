using UnityEngine;
using UnityEngine.UI;

public class ChangeColorCamera : MonoBehaviour
{
    public static ChangeColorCamera _instance;
    public GameObject panelColorOver;
    public float Zoom1;

    public float Zoom2;

    public GameObject scaleObj;
    public GameObject scaleSnail;

    [HideInInspector]
    public float ZoomIn1, ZoomIn2;

    [HideInInspector]
    public float duration = 1.0f;

    private float elapsed = 0.0f;
    private float timeZoomIn = 0f;

    [HideInInspector]
    public bool transition = false;

    [HideInInspector]
    public bool GiamToc;

    [HideInInspector]
    public bool zoomIn = false;

    public Color[] color;
    private Camera _camera;
    private SpriteRenderer[] sprites;

    [HideInInspector]
    public bool isChangeColor;

    [HideInInspector]
    public bool isZoomOut, isZoomIn;

    public Sprite[] daGiac;

    [HideInInspector]
    public float timeMapout = 0f, timeMapIn = 0f;

    public GameObject[] BG;
    private Color alphal;
    private GameObject Flag;

    private void Awake()

    {
        if (_instance == null)
        {
            _instance = this;
        }
        SnailColor = false;
    }

    private void Start()
    {
        // PlayerPrefs.SetInt("ValuePlayLevel", 50);
        //  PlayerPrefs.SetInt("MaxValueLevel", 51);
        alphal.a = 198;
        BG[0].SetActive(true);
        BG[1].SetActive(false);
        sprites = GetComponentsInChildren<SpriteRenderer>();

        _camera = GetComponent<Camera>();

        Camera.main.orthographic = true;
        //  setup();
    }

    private void Update()
    {
        // when dead
        if (transition)
        {
            Zoom1 = Camera.main.orthographicSize;
            elapsed += Time.deltaTime / duration;
            Camera.main.orthographicSize = Mathf.Lerp(Zoom1, Zoom2, elapsed);
            zoomIn = false;
            timeZoomIn = 0f;
        }

        if (zoomIn)
        {
            transition = false;
            elapsed = 0.01f;
            timeZoomIn += Time.deltaTime;
            ZoomIn1 = Camera.main.orthographicSize;
            ZoomIn2 = 4.32f;
            Camera.main.orthographicSize = Mathf.Lerp(ZoomIn1, ZoomIn2, timeZoomIn);
        }

        // zoom out map
        if (isZoomOut)
        {
            timeMapout += Time.deltaTime / 2f;

            float zoom1 = Camera.main.orthographicSize;
            float zoom2 = 18f;
            Camera.main.orthographicSize = Mathf.Lerp(zoom1, zoom2, timeMapout);
            scaleObj.transform.localScale = Vector3.Lerp(scaleObj.transform.localScale, new Vector3(4, 4, 1), elapsed);
            scaleSnail.transform.localScale = Vector3.Lerp(scaleObj.transform.localScale, new Vector3(4, 4, 1), elapsed);
            Flag = GameObject.FindGameObjectWithTag("Flag");
            Flag.transform.localScale = Vector3.Lerp(Flag.transform.localScale, new Vector3(4, 4, 1), elapsed);
        }
        //zoom in map
        if (isZoomIn)
        {
            timeMapIn += Time.deltaTime / 0.3f;

            float zoomIn1 = 18f;
            float zoomIn2 = 10f;
            Camera.main.orthographicSize = Mathf.Lerp(zoomIn1, zoomIn2, timeMapIn);
            scaleObj.transform.localScale = Vector3.Lerp(scaleObj.transform.localScale, new Vector3(1, 1, 1), elapsed);
            Flag = GameObject.FindGameObjectWithTag("Flag");
            Flag.transform.localScale = Vector3.Lerp(Flag.transform.localScale, new Vector3(1, 1, 1), elapsed);
        }
        // mau tim
        if (mauTimSpeed)
        {
            if (PlayerController._instance.Anim != null)
            {
                _camera.orthographicSize = Mathf.Lerp(_camera.orthographicSize, 6f, 0.22f * Time.deltaTime);
                RotationCamera._instance._animCam.speed = Mathf.Lerp(1, 3f, 2f * Time.deltaTime);

                PlayerController._instance.Anim.speed = Mathf.Lerp(PlayerController._instance.Anim.speed, 2.1f, 1.2f * Time.deltaTime);
                PlayerController._instance.Anim.SetBool("Change", true);
                PlayerController._instance.SpeedUp = Mathf.Lerp(PlayerController._instance.SpeedUp, 4.8f, 0.5f * Time.deltaTime);
            }
        }
        if (SnailColor)
        {
            mauTimSpeed = false;
            _camera.orthographicSize = Mathf.Lerp(_camera.orthographicSize, 5.5f, 0.8f * Time.deltaTime);
            scaleSnail.transform.localScale = Vector3.Lerp(scaleObj.transform.localScale, new Vector3(4, 4, 1), 0.4f * Time.deltaTime);
        }
        if (GiamToc)
        {
            PlayerController._instance.SpeedUp = Mathf.Lerp(PlayerController._instance.SpeedUp, 2.6f, 6f * Time.deltaTime);
        }
        // mau tim
    }

    public bool mauTimSpeed;
    public bool SnailColor;

    // change color
    public void SwitchColor(int colorStr)
    {
        if (colorStr == color.Length)
        {
            colorStr = 0;
        }

        foreach (SpriteRenderer child in sprites)
        {
            switch (colorStr)
            {
                case 0:
                    //green
                    if (!SnailColor)
                    {
                        panelColorOver.GetComponent<Image>().color = color[0];

                        BG[0].SetActive(true);
                        BG[1].SetActive(false);
                        SnailColor = false; mauTimSpeed = false;
                        if (PlayerController._instance.Anim != null)
                        {
                            if (PlayerPrefs.GetInt("ValuePlayLevel") == 2)
                            {
                                PlayerController._instance.SpeedUp = 3.5f;
                                PlayerController._instance.Anim.speed = 1.22f;
                                PlayerController._instance.Anim.SetBool("Change", false);
                            }
                            if (PlayerPrefs.GetInt("ValuePlayLevel") >= 9)
                            {
                                PlayerController._instance.SpeedUp = 3.7f;
                                PlayerController._instance.Anim.speed = 1.3f;
                                PlayerController._instance.Anim.SetBool("Change", false);
                            }
                            if (PlayerPrefs.GetInt("ValuePlayLevel") >= 16)
                            {
                                PlayerController._instance.SpeedUp = 4.1f;
                                PlayerController._instance.Anim.speed = 1.4f;
                                FollowCamera._instance.smoothing = 18f;
                                PlayerController._instance.Anim.SetBool("Change", true);
                            }
                            if (PlayerPrefs.GetInt("ValuePlayLevel") >= 23)
                            {
                                PlayerController._instance.SpeedUp = 4.35f;
                                PlayerController._instance.Anim.speed = 1.9f;
                                FollowCamera._instance.smoothing = 14f;
                                PlayerController._instance.Anim.SetBool("Change", true);
                            }
                        }
                        _camera.backgroundColor = color[0];

                        child.sprite = daGiac[0];
                    }
                    break;

                case 1:
                    //Orange
                    if (!SnailColor)
                    {
                        panelColorOver.GetComponent<Image>().color = color[1];

                        mauTimSpeed = false;
                        SnailColor = false;
                        BG[0].SetActive(true);
                        BG[1].SetActive(false);
                        if (PlayerController._instance.Anim != null)
                        {
                            if (PlayerPrefs.GetInt("ValuePlayLevel") == 3)
                            {
                                PlayerController._instance.SpeedUp = 3.2f;
                                PlayerController._instance.Anim.speed = 1.14f;
                                PlayerController._instance.Anim.SetBool("Change", false);
                            }
                            if (PlayerPrefs.GetInt("ValuePlayLevel") >= 10)
                            {
                                PlayerController._instance.SpeedUp = 3.7f;
                                PlayerController._instance.Anim.speed = 1.3f;
                                RotationCamera._instance._animCam.speed = 1f;
                                PlayerController._instance.Anim.SetBool("Change", false);
                            }
                            if (PlayerPrefs.GetInt("ValuePlayLevel") >= 17)
                            {
                                PlayerController._instance.SpeedUp = 4.1f;
                                PlayerController._instance.Anim.speed = 1.6f;
                                RotationCamera._instance._animCam.speed = 1.2f;
                                PlayerController._instance.Anim.SetBool("Change", true);
                            }
                            if (PlayerPrefs.GetInt("ValuePlayLevel") >= 39)
                            {
                                PlayerController._instance.SpeedUp = 4.5f;
                                PlayerController._instance.Anim.speed = 1.88f;
                                RotationCamera._instance._animCam.speed = 1.4f;
                                PlayerController._instance.Anim.SetBool("Change", true);
                            }
                        }
                        _camera.backgroundColor = color[1];

                        child.sprite = daGiac[1];
                    }
                    break;

                case 2:
                    if (!SnailColor)
                    {
                        panelColorOver.GetComponent<Image>().color = color[2];

                        mauTimSpeed = false;
                        SnailColor = false;
                        BG[0].SetActive(true);
                        BG[1].SetActive(false);
                        // cam dam
                        if (PlayerController._instance.Anim != null)
                        {
                            if (PlayerPrefs.GetInt("ValuePlayLevel") >= 11)
                            {
                                PlayerController._instance.SpeedUp = 3.6f;
                                PlayerController._instance.Anim.speed = 1.32f;
                                RotationCamera._instance._animCam.speed = 0.8f;
                            }
                            if (PlayerPrefs.GetInt("ValuePlayLevel") >= 18)
                            {
                                PlayerController._instance.SpeedUp = 3.99f;
                                PlayerController._instance.Anim.speed = 1.45f;
                                RotationCamera._instance._animCam.speed = 0.8f;
                            }
                            if (PlayerPrefs.GetInt("ValuePlayLevel") >= 16)
                            {
                                PlayerController._instance.SpeedUp = 4.1f;
                                PlayerController._instance.Anim.speed = 1.5f;
                                RotationCamera._instance._animCam.speed = 1.2f;
                                PlayerController._instance.Anim.SetBool("Change", true);
                            }
                            if (PlayerPrefs.GetInt("ValuePlayLevel") >= 26)
                            {
                                PlayerController._instance.SpeedUp = 4.4f;
                                PlayerController._instance.Anim.speed = 1.55f;
                                PlayerController._instance.Anim.SetBool("Change", true);
                                RotationCamera._instance._animCam.speed = 1.2f;
                            }
                            if (PlayerPrefs.GetInt("ValuePlayLevel") >= 40)
                            {
                                PlayerController._instance.SpeedUp = 4.5f;
                                PlayerController._instance.Anim.speed = 1.88f;
                                RotationCamera._instance._animCam.speed = 1.2f;
                                PlayerController._instance.Anim.SetBool("Change", true);
                            }
                            if (PlayerPrefs.GetInt("ValuePlayLevel") >= 55)
                            {
                                PlayerController._instance.SpeedUp = 5f;
                                PlayerController._instance.Anim.speed = 2.1f;
                                RotationCamera._instance._animCam.speed = 1.41f;
                                PlayerController._instance.Anim.SetBool("Change", true);
                            }
                            if (PlayerPrefs.GetInt("ValuePlayLevel") >= 100)
                            {
                                PlayerController._instance.SpeedUp = 5f;
                                PlayerController._instance.Anim.speed = 2.55f;
                                RotationCamera._instance._animCam.speed = 1.45f;
                                PlayerController._instance.Anim.SetBool("Change", true);
                            }
                        }

                        _camera.backgroundColor = color[2];
                        child.sprite = daGiac[2];
                    }
                    break;

                case 3:
                    //Pink Red
                    if (!SnailColor)
                    {
                        panelColorOver.GetComponent<Image>().color = color[3];

                        mauTimSpeed = false;
                        SnailColor = false;
                        BG[0].SetActive(true);
                        BG[1].SetActive(false);
                        if (PlayerController._instance.Anim != null)
                        {
                            if (PlayerPrefs.GetInt("ValuePlayLevel") == 4)
                            {
                                PlayerController._instance.SpeedUp = 3.5f;
                                PlayerController._instance.Anim.speed = 1.35f;
                                PlayerController._instance.Anim.SetBool("Change", false);
                            }
                            if (PlayerPrefs.GetInt("ValuePlayLevel") >= 12)
                            {
                                PlayerController._instance.SpeedUp = 4f;
                                PlayerController._instance.Anim.speed = 1.55f;
                                RotationCamera._instance._animCam.speed = 0.9f;
                                PlayerController._instance.Anim.SetBool("Change", false);
                            }
                            if (PlayerPrefs.GetInt("ValuePlayLevel") >= 19)
                            {
                                PlayerController._instance.SpeedUp = 4.5f;
                                PlayerController._instance.Anim.speed = 1.82f;
                                RotationCamera._instance._animCam.speed = 1.23f;
                                PlayerController._instance.Anim.SetBool("Change", true);
                            }
                            if (PlayerPrefs.GetInt("ValuePlayLevel") >= 40)
                            {
                                PlayerController._instance.SpeedUp = 5f;
                                PlayerController._instance.Anim.speed = 2f;
                                RotationCamera._instance._animCam.speed = 1.41f;
                                PlayerController._instance.Anim.SetBool("Change", true);
                            }
                            if (PlayerPrefs.GetInt("ValuePlayLevel") >= 65)
                            {
                                PlayerController._instance.SpeedUp = 5.3f;
                                PlayerController._instance.Anim.speed = 2.11f;
                                RotationCamera._instance._animCam.speed = 1.5f;
                                PlayerController._instance.Anim.SetBool("Change", true);
                            }
                        }
                        _camera.backgroundColor = color[3];

                        child.sprite = daGiac[3];
                    }
                    break;

                case 4:
                    //Tim
                    if (!SnailColor)
                    {
                        //  PlayerController._instance.speedUp = 4f;
                        panelColorOver.GetComponent<Image>().color = color[4];
                        PlayerController._instance.SpeedUp = 2.6f;
                        SnailColor = false;
                        BG[0].SetActive(true);
                        BG[1].SetActive(false);
                        mauTimSpeed = true;
                        _camera.backgroundColor = color[4];

                        child.sprite = daGiac[4];
                    }
                    break;

                case 5:
                    // xanh dam mat trang
                    if (!SnailColor)
                    {
                        panelColorOver.GetComponent<Image>().color = color[5];

                        mauTimSpeed = false;
                        SnailColor = false;
                        BG[0].SetActive(true);
                        BG[1].SetActive(false);
                        if (PlayerController._instance.Anim != null)
                        {
                            if (PlayerPrefs.GetInt("ValuePlayLevel") == 1)
                            {
                                PlayerController._instance.SpeedUp = 3f;
                                PlayerController._instance.Anim.speed = 1.1f;

                                PlayerController._instance.Anim.SetBool("Change", false);
                            }
                            if (PlayerPrefs.GetInt("ValuePlayLevel") == 7)
                            {
                                PlayerController._instance.SpeedUp = 3.2f;
                                PlayerController._instance.Anim.speed = 1f;
                                PlayerController._instance.Anim.SetBool("Change", false);
                            }
                            if (PlayerPrefs.GetInt("ValuePlayLevel") >= 14)
                            {
                                RotationCamera._instance._animCam.speed = 0.9f;
                                PlayerController._instance.SpeedUp = 3.5f;
                                PlayerController._instance.Anim.speed = 1.1f;
                                PlayerController._instance.Anim.SetBool("Change", false);
                            }
                            if (PlayerPrefs.GetInt("ValuePlayLevel") >= 21)
                            {
                                RotationCamera._instance._animCam.speed = 1.2f;
                                PlayerController._instance.SpeedUp = 4.2f;
                                PlayerController._instance.Anim.speed = 1.5f;
                                PlayerController._instance.Anim.SetBool("Change", true);
                            }
                            if (PlayerPrefs.GetInt("ValuePlayLevel") >= 40)
                            {
                                RotationCamera._instance._animCam.speed = 1.3f;
                                PlayerController._instance.SpeedUp = 4.5f;
                                PlayerController._instance.Anim.speed = 1.86f;
                                PlayerController._instance.Anim.SetBool("Change", true);
                            }
                            if (PlayerPrefs.GetInt("ValuePlayLevel") >= 65)
                            {
                                PlayerController._instance.SpeedUp = 5.3f;
                                PlayerController._instance.Anim.speed = 2.11f;
                                RotationCamera._instance._animCam.speed = 1.54f;
                                PlayerController._instance.Anim.SetBool("Change", true);
                            }
                        }
                        _camera.backgroundColor = color[5];
                        child.sprite = daGiac[5];
                    }
                    break;

                case 6:
                    //blue
                    if (!SnailColor)
                    {
                        panelColorOver.GetComponent<Image>().color = color[6];

                        mauTimSpeed = false;
                        SnailColor = false;
                        BG[0].SetActive(true);
                        BG[1].SetActive(false);
                        if (PlayerController._instance.Anim != null)
                        {
                            if (PlayerPrefs.GetInt("ValuePlayLevel") >= 8)
                            {
                                RotationCamera._instance._animCam.speed = 1f;
                                PlayerController._instance.Anim.speed = 1.2f;
                                PlayerController._instance.SpeedUp = 2.9f;
                                PlayerController._instance.Anim.SetBool("Change", false);
                            }
                            if (PlayerPrefs.GetInt("ValuePlayLevel") >= 22)
                            {
                                PlayerController._instance.SpeedUp = 4.5f;
                                PlayerController._instance.Anim.speed = 1.82f;
                                RotationCamera._instance._animCam.speed = 1.18f;
                                PlayerController._instance.Anim.SetBool("Change", true);
                            }
                            if (PlayerPrefs.GetInt("ValuePlayLevel") >= 40)
                            {
                                PlayerController._instance.SpeedUp = 5f;
                                PlayerController._instance.Anim.speed = 2f;
                                RotationCamera._instance._animCam.speed = 1.41f;
                                PlayerController._instance.Anim.SetBool("Change", true);
                            }
                            if (PlayerPrefs.GetInt("ValuePlayLevel") >= 65)
                            {
                                PlayerController._instance.SpeedUp = 5.15f;
                                PlayerController._instance.Anim.speed = 2.2f;
                                RotationCamera._instance._animCam.speed = 1.51f;
                                PlayerController._instance.Anim.SetBool("Change", true);
                            }
                        }
                        _camera.backgroundColor = color[6];

                        child.sprite = daGiac[6];
                    }
                    break;

                case 7:
                    // pink
                    if (!SnailColor)
                    {
                        GiamToc = true;

                        mauTimSpeed = false;
                    }
                    break;

                case 8:
                    // green
                    panelColorOver.GetComponent<Image>().color = color[8];
                    mauTimSpeed = false;
                    SnailColor = true;
                    _camera.backgroundColor = color[8];
                    BG[0].SetActive(false);
                    BG[1].SetActive(true);
                    RotationCamera._instance._animCam.speed = 1f;
                    //   child.sprite = null;
                    break;
                case 9:
                    _camera.backgroundColor = color[9];
                    panelColorOver.GetComponent<Image>().color = color[9];
                    child.sprite = daGiac[1];
                    BG[1].SetActive(false);
                    BG[0].SetActive(true);
                    break;
            }
        }
        colorStr++;
    }
}