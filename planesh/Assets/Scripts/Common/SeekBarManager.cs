using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekBarManager : MonoBehaviour
{
    [SerializeField] private GameObject background;
    [SerializeField] private GameObject thumb;
    [SerializeField] private GameObject ball;
    [SerializeField] private Camera cam;
    private float widthToGo;
    private int screenWidth;
    private Vector2 thumbFirstPos;
    private Vector2 thumbEndPos;

    
    // Start is called before the first frame update
    void Start()
    {
        //init variables
        

        screenWidth = Screen.width;



    }

    // Update is called once per frame
    void Update()
    {

        //define thumFirstPos and thumEndPos

        float backgroundWidth = background.GetComponent<Renderer>().bounds.size.x;
        Vector2 backgroundLeftSidePos = new Vector2(background.transform.position.x - backgroundWidth / 2, background.transform.position.y);

        //put thumb on the first pos
        float currentSceneNum = (float)SuperSceneManager.getCurrentSceneNum() - 2;
        float scenesLength = 4; //sea1 sea2 ice1 ice2
        widthToGo = backgroundWidth / scenesLength;
        thumbFirstPos = new Vector2(backgroundLeftSidePos.x + currentSceneNum * widthToGo, backgroundLeftSidePos.y);
        thumbEndPos = new Vector2(backgroundLeftSidePos.x + (currentSceneNum + 1) * widthToGo, backgroundLeftSidePos.y);
        thumb.transform.position = thumbFirstPos;

        Vector2 ballPosScreen = cam.WorldToViewportPoint(ball.transform.position);        
        thumb.transform.position = new Vector2(thumbFirstPos.x + widthToGo * ballPosScreen.x, thumbFirstPos.y);

    }
}
