using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandManager : MonoBehaviour
{
    //Variables about timer
     [SerializeField] private int timer;
     private float leftTime;

     //Hand objects
     [SerializeField] private GameObject[] Hands;

     //Timer display
     [SerializeField] GameObject CountDisplayer;
     [SerializeField] Image Circle;
     Image CircleImage;

    //sprite for countdown
    private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite[] countDownSprite;

    private void count(int _activeHand)
    {
        //hide hands other than active one
        for (int i = 0; i < Hands.Length; i++)
        {
            if(i != _activeHand)
            {
                Hands[i].GetComponent<Hand>().hideMyself();
            }
        }

        //show TimerDisplayer
        CountDisplayer.SetActive(true);
        int intLeftTime = (int)leftTime;

        if(leftTime > 0)
        {
            leftTime -= Time.deltaTime;
            CircleImage.fillAmount = leftTime / timer;
            //Debug.Log((int)leftTime);
        }else
        {
            //change scene
            // Debug.Log("Count finishrd");
            SuperSceneManager.nextStage();
        }

        //Debug.Log("counting");

        //reflect time to sprite
        changeCountDownSprite(leftTime);
    }

    private void showAll()
    {
        for (int i = 0; i < Hands.Length; i++)
        {
            //Reset counter
            Hands[i].GetComponent<Hand>().showMyself();
        }

        leftTime = timer;
        CircleImage.fillAmount = 1;
        spriteRenderer.sprite = countDownSprite[0];

        CountDisplayer.SetActive(false);
    }

    void Awake()
    {
        leftTime = timer;
        CountDisplayer.SetActive(false);
        CircleImage = Circle.GetComponent<Image>();

        spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
    }

    void Update()
    {        
        //check
        int activeHandNum = 0;
        for(int i = 0; i < Hands.Length; i++)
        {
            if(Hands[i].GetComponent<Hand>().getIsActive())
            {
                activeHandNum++;
            }
        }

        if(activeHandNum == 0)
        {
           showAll();
        }
        else
        {
            for (int i = 0; i < Hands.Length; i++)
            {
                if(Hands[i].GetComponent<Hand>().getIsActive())
                {
                    count(i);
                }
            }
        }
    }
 
    //change sprite for countdown by using lefttime
    private void changeCountDownSprite(double t)
    {
        //Debug.Log(t);

        if(4.0 <= t && t < 5.0)
        {
            spriteRenderer.sprite = countDownSprite[5];
        }
        else if (3.0 < t && t <= 4.0)
        {
            spriteRenderer.sprite = countDownSprite[4];
        }
        else if (2.0 < t && t <= 3.0)
        {
            spriteRenderer.sprite = countDownSprite[3];
        }
        else if (1.0 < t && t <= 2.0)
        {
            spriteRenderer.sprite = countDownSprite[2];
        }
        else if (t <= 1.0)
        {
            spriteRenderer.sprite = countDownSprite[1];
        }
        else
        {
            spriteRenderer.sprite = countDownSprite[0];
        }
    }
}
