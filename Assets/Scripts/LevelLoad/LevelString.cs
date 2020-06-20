using UnityEngine;

public class LevelString : MonoBehaviour
{
    public static LevelString _instance;

    public string levelName;
    public int number;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            LevelLoader._instance.InitiateLevel(levelName + number.ToString());
        }
    }
}