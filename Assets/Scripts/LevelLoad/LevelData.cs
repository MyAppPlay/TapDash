using System.Collections.Generic;
using UnityEngine;


public class LevelData
{
    public string Name;
    public List<SpawnType> ObjectsToSpawn = new List<SpawnType>();

    public void AddNewPrefab(GameObject newObj, GameEnum.PrefabEnum prefabEnum)
    {
        Vector3 pos = newObj.transform.position;
        ObjectsToSpawn.Add(new SpawnType(prefabEnum, pos));
    }
}

//get infomation map........
public class SpawnType
{
    public GameEnum.PrefabEnum PrefabType;

    public float PosX;
    public float PosY;
    public float PosZ;

    public SpawnType()
    {
    }

    public SpawnType(GameEnum.PrefabEnum prefabEnum, Vector3 transformVector)
    {
        PrefabType = prefabEnum;
        PosX = transformVector.x;
        PosY = transformVector.y;
        PosZ = transformVector.z;
    }

    public Vector3 GetVector3()
    {
        return new Vector3(PosX, PosY, PosZ);
    }
}