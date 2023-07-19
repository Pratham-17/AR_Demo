using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SceneSelection : MonoBehaviour
{
    static string Navigation = "Navigation";
    static string ModelTraget = "Model Traget";
    static string ImageTraget = "Image Traget";
    static string Measurement = "Real world measurement";

    [SerializeField] List<string> listofScenes;

    public TMP_Dropdown sceneSelection;
    public TMP_Text loadingText;

    /// <summary>
    /// for initializing scene selection dropdown.
    /// </summary>
    private void Start()
    {
        // Empty all the options before starting it.
        sceneSelection.ClearOptions();

        listofScenes = new List<string>() { ModelTraget, ImageTraget, Measurement };

#if UNITY_IOS
        listofScenes.Add(Navigation);
#endif

        foreach (string scene in listofScenes)
        {
            Debug.Log($"added {scene} scene in dropdown");
            sceneSelection.options.Add(new TMP_Dropdown.OptionData(scene,null));
        }
    }

    public void OnValueChange()
    {
        Debug.Log($"Selected scene: {sceneSelection.options[sceneSelection.value].text}");
        StartCoroutine(LoadSelectedScene(sceneSelection.options[sceneSelection.value].text));
    }

    IEnumerator LoadSelectedScene(string sceneName)
    {
        yield return new WaitForSeconds(1f);
        //Show loading text/screen.
        loadingText.enabled = true;
        AsyncOperation loadScene = SceneManager.LoadSceneAsync(sceneName);
        while(!loadScene.isDone) yield return null;

        loadScene.completed += (value) => { loadingText.enabled = false; };
    }
}
