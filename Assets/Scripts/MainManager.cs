using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    [System.Serializable]
    public class PlayerData
    {
        public string playerName;
        public int money;
        public float count;
        public Vector3 position;
    }

    [SerializeField] PlayerData playerData;

    public void SavePlayerData()
    {
        playerData.position = transform.position;
        DataManager.SaveToJson(playerData, "playerData");
    }
    public void LoadPlayerData()
    {
        playerData = DataManager.LoadFromJson<PlayerData>("playerData");
        transform.position = playerData.position;
    }

    public void OnOpenOption()
    {
        SceneManager.Instance.OpenOption();
    }

    public void OnLoadGameScene()
    {
        SceneManager.Instance.LoadNextScene(SceneManager.SCENE.Game);
    }

}
