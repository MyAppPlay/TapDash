using UnityEngine;

public class CharacterSnail : MonoBehaviour
{
    public static CharacterSnail _instance;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        FollowCamera._instance.isFollow = true;
        FollowCamera._instance.Setup();
    }


    private void OnTriggerEnter2D(Component col)
    {
        if (col.gameObject.name == "VeDich")
        {
            if (PlayerPrefs.GetInt("Check") == 1)
            {
                UIManager._instance.Checked = true;
            }
            if (PlayerPrefs.GetInt("Check") > 1)
            {
                CharacterManager._instance.CheckRandom = true;
            }

            UIManager._instance.pos = transform.position;

            // instance nhan vat
            GameManager._instance.isInstance = true;
            Destroy(gameObject);
        }
        switch (col.gameObject.tag)
        {
            case "LevelUp":
                PlayerController._instance.CheckColor = false;
                break;
            case "Green":
                PlayerController._instance.CheckColor = false;
                break;
            case "Orange":
                PlayerController._instance.CheckColor = false;
                break;
            case "Pink":
                PlayerController._instance.CheckColor = false;
                break;
            case "Violet":
                PlayerController._instance.CheckColor = false;
                break;
            case "BlueBlur":
                PlayerController._instance.CheckColor = false;
                break;
            case "CamSam":
                PlayerController._instance.CheckColor = false;
                break;
            case "Blue":
                PlayerController._instance.CheckColor = false;
                break;
            case "PinkBlur":
                PlayerController._instance.CheckColor = false;
                break;
        }
    }
}