using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    public void OnLoadGameScene()
    {
        SceneManager.Instance.LoadNextScene("Game");
    }

}
