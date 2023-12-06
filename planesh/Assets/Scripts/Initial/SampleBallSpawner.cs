using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleBallSpawner : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private GameObject[] sampleBalls;
    [SerializeField] private float interval;
    

    // Use this for initialization
    IEnumerator Start()
    {

        while (true)
        {
            spawn();
            yield return new WaitForSeconds(interval);
        }
    }

    private void spawn()
    {
        int index = Random.Range(0, sampleBalls.Length);

        Vector2 screenPos = new Vector2(Random.Range(0, Screen.width), 0);
        Vector2 spawnPos = cam.ScreenToWorldPoint(screenPos);
        spawnPos = new Vector2(spawnPos.x, this.gameObject.transform.position.y);

        Instantiate(sampleBalls[index], spawnPos, transform.rotation);
    }
}
