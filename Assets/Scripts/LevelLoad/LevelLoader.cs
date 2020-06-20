using UnityEngine;


public class LevelLoader : MonoBehaviour
{
    public static LevelLoader _instance;

    private TextAsset LevelToLoad;
    private LevelData levelData;

    // public int levelIndex = 0;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
    }

    // Use this for initialization
    private void Start()
    {
        // load level from level manager
        string s = LevelManager._instance._levelIndex.ToString();
        //InitiateLevel("Level" + s);
    }

    public void InitiateLevel(string levelString)
    {
        LevelToLoad = Resources.Load("Levels/Level1/" + levelString + "") as TextAsset;
        levelData = JsonFx.Json.JsonReader.Deserialize<LevelData>(LevelToLoad.text);

        foreach (SpawnType go in levelData.ObjectsToSpawn)
        {
            //You need to have you Prefabs placed in a "Prefabs" folder
            string resourcePos = "Prefabs/" + go.PrefabType.ToString();
            Object objLoaded = Resources.Load(resourcePos);
            Vector3 pos = go.GetVector3();

            GameObject newObj = Instantiate(objLoaded, pos, Quaternion.identity) as GameObject;
            if (newObj != null) newObj.name = objLoaded.name;
        }
    }
}