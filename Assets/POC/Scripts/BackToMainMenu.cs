using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMainMenu : MonoBehaviour
{
   public void BacktoMainMenu()
    {
        StartCoroutine(LoadMainMenu());
    }

    IEnumerator LoadMainMenu()
    {
        AsyncOperation loadScene = SceneManager.LoadSceneAsync(GenericVariables.MainMenu_Scene);
        while (!loadScene.isDone) yield return null;
    }
}