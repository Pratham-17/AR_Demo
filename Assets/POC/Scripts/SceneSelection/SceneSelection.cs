using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SceneSelection : MonoBehaviour
{
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

        listofScenes = new List<string>() {GenericVariables.DP_DefaultName,
                                            GenericVariables.DP_ImageTargetName, 
                                            GenericVariables.DP_ModelTargetName, 
                                            GenericVariables.DP_MeasurementName};

#if UNITY_IOS
        listofScenes.Add(GenericVariables.DP_NavigationName);
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

        AsyncOperation loadScene;

        switch (sceneName)
        {
            case GenericVariables.DP_ImageTargetName:
                loadScene = SceneManager.LoadSceneAsync(GenericVariables.ImageTraget_Scene);
                break;
            case GenericVariables.DP_MeasurementName:
                loadScene = SceneManager.LoadSceneAsync(GenericVariables.Measurement_Scene);
                break;
            case GenericVariables.DP_NavigationName:
                loadScene = SceneManager.LoadSceneAsync(GenericVariables.Navigation_Scene);
                break;
            default:
                loadScene = SceneManager.LoadSceneAsync(GenericVariables.ModelTraget_Scene);
                break;
        }
         
        while(!loadScene.isDone) yield return null;

        loadScene.completed += (value) => { loadingText.enabled = false; };
    }
}
