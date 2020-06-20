using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager _instance;

    [HideInInspector]
    public bool isAddScore = false;

    [SerializeField]
    private Text _scoreText;

    public Text diamondText;
    public Text totalDiamondText;
    public int _score;
    public int _diamond;

    public bool isPlus;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
    }

    private void Start()
    {
        GetTextDiamond();
    }

    public void GetTextDiamond()
    {
        totalDiamondText.text = "" + PlayerPrefs.GetInt("DiamondTotal");
    }

    public void AddScore(int amount)
    {
        _score += amount;
        _scoreText.text = "" + _score;
        isAddScore = false;
    }

    public int ResetScore()
    {
        int total = _score - _score;
        _scoreText.text = "" + total;
        _score = total;
        return total;
    }

    // Use this for initialization
}