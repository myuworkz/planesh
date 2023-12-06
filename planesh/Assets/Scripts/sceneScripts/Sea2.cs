using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Sea2 : Scene
{
    private string sceneName;
    AudioSource audioData;
    [SerializeField] AudioClip sound;
    [SerializeField] private Vector2 iniForce;

    private Animator springAnimator; //アニメーター

    void Start()
    {
        sceneName = SceneManager.GetActiveScene().name;
        SuperSceneManager.setCurrentSceneName(sceneName);
        Debug.Log("currentScene is " + sceneName);

        //アニメーター
        springAnimator = this.GetComponent<Animator>();

        //audio
        audioData = GetComponent<AudioSource>();
    }

   public override void nextStage()
   {
       SuperSceneManager.nextStage();
        Destroy(GameObject.FindWithTag("Ball"));
   }

   void OnTriggerEnter2D(Collider2D collision)
   {
        //if (collision.gameObject.tag == "Ball")
        //{
        //    Invoke("nextStage", 1.5f);
        //    audioData.PlayOneShot(sound);
        //}
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            rb.AddForce(iniForce, ForceMode2D.Impulse);   // 力を加える

            springAnimationTrigger();//ばねアニメーション

            Invoke("nextStage", 1.0f);
            audioData.PlayOneShot(sound);
        }
    }

    private void springAnimationTrigger()
    {
        springAnimator.SetTrigger("springJumpTrigger");
    }
}
