using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SerializeScene : ScriptableWizard
{
    public string Folder = "Levels";
    private string assetPath;

    [UnityEditor.MenuItem("Tools/CustomHelpers/Serialize Scene")]
    private static void SerializeOpenScene()
    {
        var ss = (SerializeScene)ScriptableWizard.DisplayWizard("Serialize Scene", typeof(SerializeScene));
    }

    private void OnWizardCreate()
    {
        //Find Levelinfo and extract info on which world the level belongs to
        var li = FindObjectOfType(typeof(LevelInfo)) as LevelInfo;
        var world = li.World;

        // Get the path we'll use to write our assets:
        assetPath = Application.dataPath + "/Resources/" + Folder + "/" + world.ToString() + "/";
        Debug.Log("Save at " + assetPath);

        // Create the folder that will hold our assets:
        Directory.CreateDirectory(assetPath);

        FindAssets();

        // Make sure the new assets are (re-)imported:
        AssetDatabase.Refresh();
    }

    private void FindAssets()
    {
        //List<GameObject> objList = new List<GameObject>();
        var currentScene = SceneManager.GetActiveScene().name;
        var newLevel = new LevelData { Name = trimStringToSceneName(currentScene) };

        //Trim string to leave out folder information

        //Walls
        var walls = GameObject.FindGameObjectsWithTag("Level");
        Debug.Log("We found " + walls.Length + " Level");

        addObjects(walls, GameEnum.PrefabEnum.Level145, ref newLevel);

        var newObject = JsonFx.Json.JsonWriter.Serialize(newLevel);

        var fs = new FileStream(assetPath + newLevel.Name + ".txt", FileMode.OpenOrCreate, FileAccess.Write);
        var sw = new StreamWriter(fs);
        sw.Write(newObject);
        sw.Close();
        fs.Close();
    }

    private void addObjects(GameObject[] objList, GameEnum.PrefabEnum prefabEnum, ref LevelData level)
    {
        for (var i = 0; i < objList.Length; ++i)
        {
            //Only look for instances of prefabs
            if (PrefabAssetType.Model == PrefabUtility.GetPrefabAssetType(objList[i]))
            {
                var root = PrefabUtility.GetOutermostPrefabInstanceRoot(objList[i]);

                //Only add to list if its the root to make sure the same object is not added several times
                if (root == objList[i])
                {
                    Debug.Log("It's the Root");
                    level.AddNewPrefab(objList[i], prefabEnum);
                }
                else
                {
                    Debug.Log("It's not the root, so we dont add it to list");
                }
            }
            else
            {
                Debug.Log("NOT A PREFAB");
            }
        }
    }

    private string trimStringToSceneName(string path)
    {
        var sceneName = string.Empty;

        //Removing all but levelname + ".unity"
        var tmpString = path.Remove(0, path.Length - 13);

        //removing ".unity"
        sceneName = tmpString.Substring(0, tmpString.Length - 6);

        return sceneName;
    }
}