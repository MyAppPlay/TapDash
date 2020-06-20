using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : SoundBase
{
    public static PlayerController _instance;

    public float SpeedUp = 2.9f;

    private int _randomFly;
    public AudioClip[] DeadSound;
    public Transform[] DiamondFound;
    //public GameObject ScoreManagerObj;

    [HideInInspector]
    public bool IsUp;

    [HideInInspector]
    public bool IsLeft;

    [HideInInspector]
    public bool IsRight;

    [HideInInspector]
    public bool IsJump;

    public bool CheckPlayerSound;
    private AudioSource _audioSource;

    public AudioClip FinishWin;
    public Transform startTransform;
    public Transform startTransform1;

    public Transform endCast;
    // public Transform jumpTransform;

    public Sprite GreenArrow;
    public GameObject XDead;
    private Collider2D _box;

    public LayerMask LayerMask;
    //  public LayerMask JumpLayerMask;

    [HideInInspector]
    public RaycastHit2D MoveMent;



    private RaycastHit2D _coinRay;

    public Rigidbody2D _rb;
    private Collider2D _boxMoveMid;

    [SerializeField]
    public Animator Anim;

    [HideInInspector]
    public bool isMoveCenter;


    [HideInInspector]
    public Vector3 posSaveLevel;


    [HideInInspector]
    public int ClickAddScore;

    private Color _alpha;
    private Color _bpha;

    [HideInInspector]
    public int ValuePlayLevel;

    public AudioClip[] jump;



    [HideInInspector]
    public bool isClick;

    // check color from changecolorCamera
    public bool CheckColor;

    //Diamond pos
    public Vector3 diamondPos;


    // public string[] AnimSate;
    public string animStateStr;
    public string _animState2;
    public enum Direct
    {
        Up,
        Left,
        Right,
        JumpUp,
    }

    public Direct _direct;
    public int CountItemsEated;
    public int CountItems;

    private void Awake()
    {
        _alpha.a = 0;

        _bpha.a = 0;
        ClickAddScore = 40;
        if (_instance == null)
            _instance = this;
    }

    // refercency

    public void PlaySnail()
    {
        soundManager.OcSenSound();
    }

    private void Start()
    {
        CheckColor = true;
        if (PlayerPrefs.GetInt("HelpPanel") == 1)
            isClick = true;
        _audioSource = GetComponent<AudioSource>();
        ValuePlayLevel = PlayerPrefs.GetInt("ValuePlayLevel", 0);

        _boxMoveMid = GetComponent<Collider2D>();

        FollowCamera._instance.Setup();

        //   _rb = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();
        _box = GetComponent<Collider2D>();
        IsUp = true;
    }

    private void Update()
    {
        CheckRayCastAndMovement();
        if (isMoveCenter)
            MoveCenter(_boxMoveMid);
    }

    // get character moving center when them get winner
    public void MoveCenter(Collider2D col)
    {
        _boxMoveMid = col;
        if (transform.position.x < col.bounds.center.x)
            transform.Translate(Vector3.right * Time.deltaTime * 0.7f);
        else
            transform.Translate(Vector3.left * Time.deltaTime * 0.7f);
        if ((transform.position.x > col.bounds.center.x - 0.01) && (transform.position.x < col.bounds.center.x + 0.01))
            isMoveCenter = false;
    }

    private void CheckRayCastAndMovement()
    {
        MoveMent = Physics2D.Linecast(startTransform.transform.position, endCast.transform.position, LayerMask);


        if (Anim.GetCurrentAnimatorStateInfo(0).IsTag("Fly" + _randomFly))
        {
            _coinRay = Physics2D.Linecast(startTransform.position, startTransform1.position);

            if (_coinRay)
                if (_coinRay.collider.gameObject.name == "Diamond")
                {
                    CountItemsEated++;
                    ScoreManager._instance.AddScore(10);
                    soundManager.DiamondSound();
                    _coinRay.collider.gameObject.SetActive(false);
                    ScoreManager._instance.diamondText.text = CountItemsEated + "/" + CountItems;
                }

            _box.isTrigger = false;
        }
        else if (!Anim.GetCurrentAnimatorStateInfo(0).IsTag("Fly" + _randomFly))
        {
            _box.isTrigger = true;
        }

        if (MoveMent.collider != null && MoveMent.collider.gameObject.layer == LayerMask.NameToLayer("Number"))
            if (MoveMent.collider != null)
                MoveMent.collider.transform.gameObject.GetComponent<SpriteRenderer>().sprite = GreenArrow;

        if (MoveMent.collider != null && MoveMent.collider.gameObject.layer == LayerMask.NameToLayer("Jump"))
            MoveMent.collider.transform.gameObject.GetComponent<SpriteRenderer>().sprite = GreenArrow;

        if (!Input.GetMouseButtonDown(0)) return;
        if (MoveMent.collider != null && MoveMent.collider.gameObject.layer == LayerMask.NameToLayer("Number"))
        {
            if (SpeedUp <= 4.0f)
                Anim.Play(animStateStr);
            if (SpeedUp > 4.0)
                Anim.Play(_animState2);

            _box.isTrigger = true;
            //cong thuc tinh do dai lay vi tri gan nhat giua 2 vector
            //   var distance = Vector3.Distance(startTransform.position, endCast.position);
            if (MoveMent.collider != null)
            {
                //   var diff = moveMent.collider.transform.position - startTransform.transform.position;
                //   var currentDiff = diff.sqrMagnitude;
                if (isClick)
                //   if (currentDiff < distance)
                {
                    //      distance = currentDiff;
                    _alpha.b = 255;
                    _alpha.g = 255;
                    _alpha.r = 255;
                    //alpha.a = 0f;
                    _alpha.a = _alpha.b = _alpha.g = _alpha.r = 0;
                    MoveMent.collider.transform.gameObject.GetComponent<SpriteRenderer>().color = _alpha;
                }
            }

            ScoreManager._instance.AddScore(ClickAddScore);

            soundManager.TouchClick(0.7f);
            // ReSharper disable once CompareOfFloatsByEqualityOperator
            if (MoveMent.transform.rotation.eulerAngles.z == 90)
            {
                _direct = Direct.Left;
                IsLeft = true;
                IsRight = false;
                IsUp = false;
                // isJump=true ;
            }
            // ReSharper disable once CompareOfFloatsByEqualityOperator
            else if (MoveMent.transform.rotation.eulerAngles.z == 0)
            {
                _direct = Direct.Up;
                IsLeft = false;
                IsRight = false;
                IsUp = true;// isJump = true;
            }
            // ReSharper disable once CompareOfFloatsByEqualityOperator
            if (MoveMent.transform.rotation.eulerAngles.z == 270)
            {
                _direct = Direct.Right;
                IsLeft = false;
                IsRight = true;
                IsUp = false; //isJump = true;
            }
        }
        //  Debug.Log(moveMent.collider);
        //check bay
        if (MoveMent.collider != null && MoveMent.collider.gameObject.layer == LayerMask.NameToLayer("Jump"))
        {
            _direct = Direct.JumpUp;
            RotationCamera._instance.rayShoot = false;
            RotationCamera._instance.rayShootNguoc = false;
            //cong thuc tinh do dai lay vi tri gan nhat giua 2 vector
            //float distance = Vector3.Distance(startTransform.position, jumpTransform.position);
            //Vector3 diff = fly.collider.transform.position - startTransform.transform.position;
            //float currentDiff = diff.sqrMagnitude;
            //if (currentDiff < distance)
            //{
            //    distance = currentDiff;
            MoveMent.collider.transform.gameObject.GetComponent<SpriteRenderer>().color = _bpha;
            _alpha.a = _alpha.b = _alpha.g = _alpha.r = 255;

            if (MoveMent.collider != null)
                MoveMent.collider.transform.gameObject.GetComponent<SpriteRenderer>().color = _alpha;
            else
                return;

            // fly.collider.transform.gameObject.SetActive(false);
            //  }

            IsRight = false;
            IsLeft = false;
            IsUp = false;
            IsJump = true;

            _randomFly = Random.Range(1, 5);
        }
        // check vong tron ban dau
        if (MoveMent.collider != null && MoveMent.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            RotationCamera._instance.rayShoot = false;
            RotationCamera._instance.rayShootNguoc = false;
            ClickAddScore = 0;
            //  Destroy(ScoreManagerObj);
            soundManager.TouchClick(0f);
            IsUp = false;
            IsLeft = false;
            IsRight = false;
        }
        else if (MoveMent.collider != null && MoveMent.collider.gameObject.layer != LayerMask.NameToLayer("Ground"))
        {
            ClickAddScore = 40;
        }
    }

    public void LoadScene()
    {
        SceneManager.LoadScene("Game");
    }

    // Ontrigger

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "Dead")
        {
            // check if many check is random
            if (PlayerPrefs.GetInt("Check") > 1)
            {
                CharacterManager._instance.CheckRandom = true;
                CharacterManager._instance.RandomChar();
            }

            // CharacterManager._instance.RandomChar();
            ChangeColorCamera._instance.SnailColor = false;
            // reset score is 0
            ScoreManager._instance._score = 0;
            // save pos when dead to instance xDead
            posSaveLevel = transform.position;
            // _anim.SetTrigger("Dead");
            _rb.isKinematic = true;
            //speedUp = 0f;
            CountItemsEated = 0;

            //disable everything from cameara
            RotationCamera._instance.isZoomin = false;
            RotationCamera._instance.isZoomOut = false;
            RotationCamera._instance.rayShoot = false;
            RotationCamera._instance.rayShootNguoc = false;
            RotationCamera._instance.state = 0;
            GameManager._instance.isDead = true;
            ChangeColorCamera._instance.zoomIn = false;
            //gan ten tu game manager equar this name level;

            // position instance Xdead

            IsUp = false;
            IsLeft = false;
            IsRight = false;

            //   gameObject.SetActive(false);

            UIManager._instance.GameOverShow();
            //  CharacterManager._instance.RandomChar();
            soundManager.playPlayerSound(DeadSound[Random.Range(0, DeadSound.Length)], 1f);
            //   Dead();

            // check flag avata
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            if (UIManager._instance.posSaveDiamond == null)
                // ReSharper disable once HeuristicUnreachableCode
                UIManager._instance.isCheckGetPositionFlag = true;

            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            if (UIManager._instance.posSaveDiamond != null)
            {
                UIManager._instance.isCheckGetPositionFlag = false;

                UIManager._instance.isInstancePosFlag = true;
            }

            //
            // Get the first in stance flag and then never instantiale any more
            // set firs 0 and load map set is 10 and over
            if (!PlayerPrefs.HasKey("First"))
                PlayerPrefs.SetInt("First", 0);
            if (PlayerPrefs.GetInt("First") == 0)
                PlayerPrefsX.SetVector3("PosFlag", UIManager._instance.posSaveDiamond);
            Destroy(gameObject);
        }
        else if (col.gameObject.name == "Diamond")
        {
            // check flag avata
            if (UIManager._instance.isCheckGetPositionFlag)
                UIManager._instance.posSaveDiamond = col.bounds.center;

            // getPosDiamond = false;

            // getPosDiamond = false;
            //
            CountItemsEated++;

            soundManager.DiamondSound();
            ScoreManager._instance.AddScore(10);
            //  ScoreManager._instance.AddDiamond();
            col.gameObject.SetActive(false);
            //if (CountItemsEated > PlayerPrefs.GetInt("ItemsCountLevel" + ValuePlayLevel))
            //{
            ScoreManager._instance.diamondText.text = CountItemsEated + "/" + CountItems;
        }
        else
        {
            if (col.gameObject.tag == "LevelUp")
            {
                //  Debug.Log(PlayerPrefs.GetInt("ValuePlayLevel"));
                if (RotationCamera._instance._animCam.GetCurrentAnimatorStateInfo(0).IsName("Rotation"))
                    RotationCamera._instance._animCam.SetBool("Right", false);
                if (RotationCamera._instance._animCam.GetCurrentAnimatorStateInfo(0).IsName("Left"))
                    RotationCamera._instance._animCam.SetBool("Left", false);
                //
                if (PlayerPrefs.GetInt("ValueMaxLevel") < ValuePlayLevel + 1)
                {
                    //  source.PlayOneShot(levelFinish, 1f);
                    PlayerPrefs.SetInt("ValueMaxLevel", ValuePlayLevel + 1);
                    PlayerPrefs.Save();
                }
                //Update Text Facebook img
                //
                GameManager._instance.UpdateText();

                //
                ScoreManager._instance.diamondText.text = PlayerPrefs.GetInt("ItemsCountLevel" + ValuePlayLevel) + "/" +
                                                          CountItems;
                //
                //checkRay = true;
                GameManager._instance.levelToLoadAgain = col.transform.parent.parent.name;
                //check if level max less than value level+1 and save valueplay......

                RotationCamera._instance.mainCamera.transform.rotation = Quaternion.Euler(0f, 0f, 0f);

                //Kiểm tra số item ăn được có bằng item đếm được trong màn

                CountItems = GameObject.Find(col.transform.parent.parent.name).transform.Find("Diamond").childCount;
                CharacterManager._instance.unlock = true;
                CharacterManager._instance.LockCharacter();

                //   Debug.Log(CountItems);
            }
            if (col.gameObject.tag == "Hard")
            {
                col.gameObject.SetActive(false);
                if (UIManager._instance.isCheckGetPositionFlag)
                    UIManager._instance.posSaveDiamond = col.bounds.center;
            }
            else if (col.gameObject.name == "VeDich")
            {
                LevelManager._instance.Level[PlayerPrefs.GetInt("ValueMaxLevel") - 1].transform.Find("Avata")
                    .gameObject.SetActive(false);
                //defauc camera state
                UIManager._instance.UpdateText(PlayerPrefs.GetInt("ValuePlayLevel") + 2);
                ChangeColorCamera._instance.SwitchColor(7);
                //speed violet
                ChangeColorCamera._instance.mauTimSpeed = false;
                //  speedUp = 2.6f;
                // _anim.speed = 1;
                //    ChangeColorCamera._instance.zoomIn = true;
                RotationCamera._instance.isZoomOut = true;
                ChangeColorCamera._instance.SnailColor = false;
                RotationCamera._instance._animCam.speed = 1;
                Anim.SetBool("Change", false);
                Anim.speed = 1;
                //speedUp = 2.6f;
                //  Debug.Log(gameObject.name);
                CheckPlayerSound = true;
                isMoveCenter = true;
                FollowCamera._instance.Setup();
                //get level text
                // int a = PlayerPrefs.GetInt("ValuePlayLevel" + 1);
                // UIManager._instance.UpdateText(a);
                //
                CharacterManager._instance.unlock = true;

                // check false default
                RotationCamera._instance.rayShoot = false;
                RotationCamera._instance.rayShootNguoc = false;
                if (RotationCamera._instance.isZoomin)
                {
                    RotationCamera._instance.isZoomin = false;
                    RotationCamera._instance.isZoomOut = true;
                }

                RotationCamera._instance.state = 0;
                //    RotationCamera._instance.vedich = true;
                // tang level moi khi cham dich
                ValuePlayLevel++;
                //gan so level
                //reset camera
                if (RotationCamera._instance._animCam.GetCurrentAnimatorStateInfo(0).IsName("Rotation"))
                    RotationCamera._instance._animCam.SetBool("Right", false);
                if (RotationCamera._instance._animCam.GetCurrentAnimatorStateInfo(0).IsName("Left"))
                    RotationCamera._instance._animCam.SetBool("Left", false);

                PlayerPrefs.SetInt("ValuePlayLevel", ValuePlayLevel);
                PlayerPrefs.Save();

                //   Kiểm tra số item ăn được có bằng item đếm được trong màn
                if (!PlayerPrefs.HasKey("ItemsLevel" + ValuePlayLevel))
                    PlayerPrefs.SetString("ItemsLevel" + ValuePlayLevel, "False");
                if (PlayerPrefs.GetString("ItemsLevel" + ValuePlayLevel) == "False")
                {
                    if ((CountItemsEated == CountItems) || (PlayerPrefs.GetInt("Diamond") > CountItems))
                        PlayerPrefs.SetString("ItemsLevel" + ValuePlayLevel, "True");
                    else
                        PlayerPrefs.SetString("ItemsLevel" + ValuePlayLevel, "False");

                    PlayerPrefs.Save();
                }
                if (!PlayerPrefs.HasKey("ItemsCountLevel" + ValuePlayLevel)) // max diamond value each level
                {
                    PlayerPrefs.SetInt("ItemsCountLevel" + ValuePlayLevel, CountItemsEated);
                    PlayerPrefs.SetInt("DiamondTotal", PlayerPrefs.GetInt("DiamondTotal") + CountItemsEated);
                    // total diamond
                }

                // check cup va sound
                if (ValuePlayLevel < PlayerPrefs.GetInt("ValueMaxLevel"))
                {
                    soundManager.LevelUp(1f);
                    //soundManager.LevelUp(1f);
                    LevelManager._instance.cupGot.SetActive(false);
                }
                else if (PlayerPrefs.GetInt("ValueMaxLevel") >= ValuePlayLevel)
                {
                    LevelManager._instance.cupGot.SetActive(true);
                    _audioSource.clip = FinishWin;
                    _audioSource.Play();
                }

                if (PlayerPrefs.GetInt("ItemsCountLevel" + ValuePlayLevel) < CountItemsEated)
                {
                    PlayerPrefs.SetInt("ItemsCountLevel" + ValuePlayLevel,
                        PlayerPrefs.GetInt("ItemsCountLevel" + ValuePlayLevel) +
                        (CountItemsEated - PlayerPrefs.GetInt("ItemsCountLevel" + ValuePlayLevel)));
                    PlayerPrefs.SetInt("DiamondTotal",
                            PlayerPrefs.GetInt("DiamondTotal") +
                            (CountItemsEated - PlayerPrefs.GetInt("ItemsCountLevel" + ValuePlayLevel)));
                    //Tổng số kc kiếm được
                }

                CountItemsEated = 0;

                _boxMoveMid = col;
            }
            else if (col.gameObject.tag == "Stop")
            {
                SpeedUp = 2.6f;
                Destroy(gameObject);
                LevelManager._instance.LevelComplete.SetActive(true);
            }
            else if (col.gameObject.tag == "Green")
            {
                if (CheckColor)
                {
                    //RotationCamera._instance.state = 3;
                    //ChangeColorCamera._instance.SwitchColor(0);
                    StartCoroutine(ChangeColor(3, 0));
                }
                else
                {
                    RotationCamera._instance.state = 3;
                    SpeedUp = 2.6f;
                    ChangeColorCamera._instance.SwitchColor(8);
                }
            }
            else if (col.gameObject.tag == "Orange")
            {
                if (CheckColor)
                {
                    //RotationCamera._instance.state = 2;
                    //ChangeColorCamera._instance.SwitchColor(1);
                    StartCoroutine(ChangeColor(2, 1));
                }
                else
                {
                    RotationCamera._instance.state = 2;
                    SpeedUp = 2.6f;
                    ChangeColorCamera._instance.SwitchColor(8);
                }
            }
            else if (col.gameObject.tag == "Pink")
            {
                if (CheckColor)
                {
                    //RotationCamera._instance.state = 1;
                    //ChangeColorCamera._instance.SwitchColor(7);
                    StartCoroutine(ChangeColor(1, 7));
                }
                else
                {
                    RotationCamera._instance.state = 1;
                    SpeedUp = 2.6f;
                    ChangeColorCamera._instance.SwitchColor(8);
                }
            }
            else if (col.gameObject.tag == "Violet")
            {
                if (CheckColor)
                {
                    //RotationCamera._instance.state = 1;
                    ////RotationCamera._instance.rayShoot = true;
                    //ChangeColorCamera._instance.SwitchColor(4);
                    StartCoroutine(ChangeColor(1, 4));
                }
                else
                {
                    RotationCamera._instance.state = 1;
                    SpeedUp = 2.6f;
                    ChangeColorCamera._instance.SwitchColor(8);
                }
            }
            else if (col.gameObject.tag == "BlueBlur")
            {
                if (CheckColor)
                {
                    //   isActiveCameraRotation = true;
                    //  camerahehe = 1;
                    //   Debug.Log(camerahehe);
                    //RotationCamera._instance.state = 1;
                    //ChangeColorCamera._instance.SwitchColor(5);
                    StartCoroutine(ChangeColor(2, 5));
                }
                else
                {
                    RotationCamera._instance.state = 1;
                    SpeedUp = 2.6f;
                    ChangeColorCamera._instance.SwitchColor(8);
                }
            }
            else if (col.gameObject.tag == "CamSam")
            {
                if (CheckColor)
                {
                    //RotationCamera._instance.state = 1;
                    //ChangeColorCamera._instance.SwitchColor(2);
                    StartCoroutine(ChangeColor(1, 2));
                }
                else
                {
                    RotationCamera._instance.state = 1;
                    SpeedUp = 2.6f;
                    ChangeColorCamera._instance.SwitchColor(8);
                }
            }
            else if (col.gameObject.tag == "Blue")
            {
                ChangeColorCamera._instance.SwitchColor(6);
                if (CheckColor && (ValuePlayLevel > 2))
                {
                    StartCoroutine(ChangeColor(2, 6));
                }
                else if (ValuePlayLevel > 2)
                {
                    RotationCamera._instance.state = 2;
                    SpeedUp = 2.6f;
                    ChangeColorCamera._instance.SwitchColor(8);
                }
            }
            else if (col.gameObject.tag == "PinkBlur")
            {
                if (CheckColor)
                {
                    StartCoroutine(ChangeColor(1, 3));
                }
                else
                {
                    RotationCamera._instance.state = 2;
                    SpeedUp = 2.6f;
                    ChangeColorCamera._instance.SwitchColor(8);
                }
            }
            else if (col.gameObject.tag == "Right")
            {
                if (UIManager._instance.isCheckGetPositionFlag)
                {
                    //    isJump = false;

                    UIManager._instance.posSaveDiamond = col.bounds.center;
                }
            }
            else if (col.gameObject.tag == "Left")
            {
                if (UIManager._instance.isCheckGetPositionFlag)
                {
                    //   isJump = false;
                    UIManager._instance.posSaveDiamond = col.bounds.center;
                }
            }
            else if (col.gameObject.tag == "Up")
            {
                if (UIManager._instance.isCheckGetPositionFlag)
                {
                    //    isJump = false;
                    UIManager._instance.posSaveDiamond = col.bounds.center;
                }
            }
            else if (col.gameObject.tag == "Jump")
            {
                if (UIManager._instance.isCheckGetPositionFlag)
                    UIManager._instance.posSaveDiamond = col.bounds.center;
            }
        }
    }

    private IEnumerator ChangeColor(int state, int swithcolor)
    {
        yield return new WaitForSeconds(0.01f);
        RotationCamera._instance.state = state;

        ChangeColorCamera._instance.SwitchColor(swithcolor);
    }

    private void OnTriggerExit2D(Component col)
    {
        if (col.gameObject.name != "VeDich")
            switch (col.gameObject.tag)
            {
                case "LevelUp":
                    {
                        var local = col.transform.localPosition;
                        //set pos equal local
                        UIManager._instance.pos = local;
                        UIManager._instance.pos = col.GetComponent<Collider2D>().bounds.center;
                        UIManager._instance.pos.y -= 0.5f;
                        RotationCamera._instance.isZoomOut = false;
                        break;
                    }
                case "Right":
                    {
                        col.gameObject.SetActive(false);

                        break;
                    }
                case "Left":
                    {
                        col.gameObject.SetActive(false);

                        break;
                    }
                case "Up":
                    {
                        col.gameObject.SetActive(false);

                        break;
                    }
                case "Jump":
                    {
                        col.gameObject.SetActive(false);
                        break;
                    }
            }
        else
        {
            //check unlock level choose and sound
            CharacterManager._instance.CheckSound();
            //
            ChangeColorCamera._instance.GiamToc = false;
            CharacterManager._instance.unlock = false;
            ChangeColorCamera._instance.zoomIn = false;
            //RotationCamera._instance.mainCamera.orthographicSize = 4.2f;
            RotationCamera._instance.isZoomOut = false;
            // ValuePlayLevel = 0;
            CheckPlayerSound = false;
            isMoveCenter = false;
            Destroy(col.transform.parent.parent.gameObject, 3f);
        }
    }

    private void FixedUpdate()
    {
        MoveUp();

        Jump();
    }

    private void Jump()
    {
        _direct = Direct.JumpUp;
        var flying = _randomFly;
        if (!IsJump) return;
        switch (flying)
        {
            case 1:

                soundManager.playPlayerSound(jump[0], 1);
                Anim.SetTrigger("Fly");
                IsJump = false;
                // _anim.Stop();

                break;

            case 2:

                soundManager.playPlayerSound(jump[1], 1);
                Anim.SetTrigger("Fly2");
                IsJump = false;
                // _anim.Stop();

                break;

            case 3:

                soundManager.playPlayerSound(jump[2], 1);
                Anim.SetTrigger("Fly3");
                IsJump = false;
                // _anim.Stop();

                break;

            case 4:

                soundManager.playPlayerSound(jump[3], 1);
                Anim.SetTrigger("Fly4");
                IsJump = false;

                break;

            default:

                soundManager.playPlayerSound(jump[0], 1);
                Anim.SetTrigger("Fly");
                IsJump = false;
                // _anim.Stop();

                break;
        }
    }

    private void MoveUp()
    {
        const float smoothing = 1f;
        //float speedRotation = 100f;
        if (IsUp)
        {
            _direct = Direct.Up;

            _rb.velocity = new Vector3(0f, SpeedUp, 0f);

            var rotation = Quaternion.identity;
            rotation.z += Time.deltaTime * smoothing;
            transform.rotation = rotation;
        }

        if (IsLeft)
        {
            _direct = Direct.Left;
            _rb.velocity = new Vector3(-SpeedUp, 0f, 0f);

            //   _anim.SetTrigger("Dead");
            var rotation = Quaternion.Euler(0, 0, 90f);
            rotation.z += Time.deltaTime * smoothing;
            transform.rotation = rotation;
        }
        if (!IsRight) return;
        {
            _direct = Direct.Right;
            _rb.velocity = new Vector3(SpeedUp, 0f, 0f);

            var rotation = Quaternion.Euler(0, 0, -90f);
            rotation.z += Time.deltaTime * smoothing;
            transform.rotation = rotation;
        }
    }
}