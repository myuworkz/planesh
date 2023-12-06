using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SuperSceneManager : MonoBehaviour
{

    private static List<string> sceneNames;
    private static string currentSceneName;
    private static int currentSceneNum;

    void Awake()
    {
        //initialize sceneNames
        sceneNames = new List<string>();

        //name all the scenes
        sceneNames.Add("Initial");
        sceneNames.Add("Opening");
        sceneNames.Add("Sea1");
        sceneNames.Add("Sea2");
        sceneNames.Add("Ice1");
        sceneNames.Add("Ice2");
        sceneNames.Add("Ending");
        Debug.Log("All scenes set");
    }

    public static void setCurrentSceneName(string sceneName)
    {
        currentSceneName = sceneName;
        for(int i = 0; i < sceneNames.Count; i++)
        {
            if(sceneNames[i] == currentSceneName)
            {
                currentSceneNum = i;
                break;
            }
        }
    }

    public static int getCurrentSceneNum()
    {
        return currentSceneNum;
    }

    public static int getScenesLength()
    {
        return sceneNames.Count;
    }

    public static void nextStage()
    {
        if(currentSceneNum + 1 < sceneNames.Count)
        {
            SceneManager.LoadScene(sceneNames[currentSceneNum + 1]);
        }
        else
        {
            SceneManager.LoadScene(sceneNames[0]);
        }
    }

}
