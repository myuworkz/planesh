using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kinect = Windows.Kinect; //この書き方何？
using Windows.Kinect;

public class MyBodySourceView : MonoBehaviour
{

    [SerializeField] private BodySourceManager _manager;
    [SerializeField] private ModelBody modelBodyPrefab;
    private List<ModelBody> modelBodies;
    [SerializeField] private GameObject bodyFPGuide;

    void Awake()
    {
        modelBodies = new List<ModelBody>();
        bodyFPGuide.SetActive(false);
    }

    private void Update()
    {

        // そもそも参照が取れていないときはダメ
        if (_manager == null) return;

        // ここで人の身体情報の配列(つまりは複数人の身体座標)を受け取る
        var bodies = _manager.GetData();

        if (bodies == null) return;

        foreach (var modelBody in modelBodies)
        {
            modelBody.refreshBodyObject();
        }
        modelBodies.RemoveRange(0, modelBodies.Count);

        // 複数人から一人一人の身体情報を取り出す
        foreach (var body in bodies)
        {
            if (body == null) continue;
            if (body.IsTracked == false) continue;

            //modelbody
            ModelBody modelBody = Instantiate(modelBodyPrefab, bodyFPGuide.transform.position, Quaternion.identity) as ModelBody;
            modelBody.init(body, body.TrackingId);
            modelBodies.Add(modelBody);
        }     
    }//update

} //class MyBodySourceView


