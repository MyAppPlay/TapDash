using UnityEngine;

public class DestroyFlag : SoundBase
{
    // Use this for initialization
    public bool isRotationFlag;

    public static DestroyFlag _instance;
    public AudioClip Ring;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
    }

    private void Update()
    {
        //if (isRotationFlag)
        //{
        //if (transform.rotation.eulerAngles.z != RotationCamera._instance.mainCamera.transform.rotation.eulerAngles.z)
        //{
        //    transform.rotation = Quaternion.Lerp(transform.rotation, RotationCamera._instance.mainCamera.transform.rotation
        //        , 10 * Time.deltaTime);

        //    if (transform.rotation.eulerAngles.z >= RotationCamera._instance.mainCamera.transform.rotation.eulerAngles.z - 5f)
        //    {
        //        isRotationFlag = false;
        //        //Debug.Log(isRotationFlag);
        //    }
        //    //  }
        //}
    }

    // check if trigger and then set can be instantiale next flag
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (UIManager._instance.posSaveDiamond != null)
            {
                UIManager._instance.isCheckGetPositionFlag = true;
                UIManager._instance.isInstancePosFlag = true;
            }

            LevelManager._instance.Saving = true;
            UIManager._instance.CountFlag = 0;
            PlayerPrefs.SetInt("CheckFlag", 1);
            PlayerPrefs.Save();
            CharacterManager._instance._audioSource.clip = Ring;
            CharacterManager._instance._audioSource.Play();
            Destroy(gameObject);
        }
    }
}