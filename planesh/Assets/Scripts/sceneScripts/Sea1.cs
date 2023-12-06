using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Sea1 : Scene
{
    private string sceneName;
    AudioSource audioData;
    [SerializeField] AudioClip sound;

    void Start()
    {
        sceneName = SceneManager.GetActiveScene().name;
        SuperSceneManager.setCurrentSceneName(sceneName);
        Debug.Log("currentScene is " + sceneName);

        //audio
        audioData = GetComponent<AudioSource>();
        
    }

   public override void nextStage()
   {
       SuperSceneManager.nextStage();
   }

   void OnTriggerEnter2D(Collider2D collision)
   {
       Debug.Log("collide");
        if (collision.gameObject.tag == "Ball")
        {
            Invoke("nextStage", 0.7f);
            audioData.PlayOneShot(sound);
            //nextStage();
        }
    }
}
