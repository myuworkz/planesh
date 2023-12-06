using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kinect = Windows.Kinect; //この書き方何？
using Windows.Kinect;

class ModelBody : MonoBehaviour
{

    [SerializeField] private float bodyScale;
    [SerializeField] private GameObject jointPrefab;
    [SerializeField] private GameObject childJointPrefab;
    private Camera cam; //for translate world axis to screen axis

    private Windows.Kinect.Body body;
    private ulong id;

    Dictionary<Kinect.JointType, List<GameObject>> bones;
    List<GameObject> childJoints;

    private Dictionary<Kinect.JointType, Kinect.JointType> boneMap = new Dictionary<Kinect.JointType, Kinect.JointType>()
    {
        //the  relationships between connected two joints
        { Kinect.JointType.FootLeft, Kinect.JointType.AnkleLeft },
        { Kinect.JointType.AnkleLeft, Kinect.JointType.KneeLeft },
        { Kinect.JointType.KneeLeft, Kinect.JointType.HipLeft },
        { Kinect.JointType.HipLeft, Kinect.JointType.SpineBase },

        { Kinect.JointType.FootRight, Kinect.JointType.AnkleRight },
        { Kinect.JointType.AnkleRight, Kinect.JointType.KneeRight },
        { Kinect.JointType.KneeRight, Kinect.JointType.HipRight },
        { Kinect.JointType.HipRight, Kinect.JointType.SpineBase },

        { Kinect.JointType.HandTipLeft, Kinect.JointType.HandLeft },
        { Kinect.JointType.ThumbLeft, Kinect.JointType.HandLeft },
        { Kinect.JointType.HandLeft, Kinect.JointType.WristLeft },
        { Kinect.JointType.WristLeft, Kinect.JointType.ElbowLeft },
        { Kinect.JointType.ElbowLeft, Kinect.JointType.ShoulderLeft },
        { Kinect.JointType.ShoulderLeft, Kinect.JointType.SpineShoulder },

        { Kinect.JointType.HandTipRight, Kinect.JointType.HandRight },
        { Kinect.JointType.ThumbRight, Kinect.JointType.HandRight },
        { Kinect.JointType.HandRight, Kinect.JointType.WristRight },
        { Kinect.JointType.WristRight, Kinect.JointType.ElbowRight },
        { Kinect.JointType.ElbowRight, Kinect.JointType.ShoulderRight },
        { Kinect.JointType.ShoulderRight, Kinect.JointType.SpineShoulder },

        { Kinect.JointType.SpineBase, Kinect.JointType.SpineMid },
        { Kinect.JointType.SpineMid, Kinect.JointType.SpineShoulder },
        { Kinect.JointType.SpineShoulder, Kinect.JointType.Neck },
        { Kinect.JointType.Neck, Kinect.JointType.Head },
    };


    public void init(Windows.Kinect.Body body, ulong id)
    {
        //attach camera
        cam =  Camera.main;

        this.body = body;
        this.id = id;

        //change the name of this object
        this.gameObject.name = "body" + id;
   
        bones = new Dictionary<Kinect.JointType, List<GameObject>>();

        //register JointType and JoiintObject to Joint Dictionary
        for (Kinect.JointType jt = Kinect.JointType.SpineBase; jt <= Kinect.JointType.ThumbRight; jt++)
        {
            childJoints = new List<GameObject>();

            if (jt != Kinect.JointType.Head && jt != Kinect.JointType.Neck && jt != Kinect.JointType.HandTipRight && jt != Kinect.JointType.ThumbLeft && 
                jt != Kinect.JointType.HandTipRight && jt != Kinect.JointType.ThumbRight)
            {
                //create the first joint
                GameObject jointObject = Instantiate(jointPrefab, body.Joints[jt].ToVector3(bodyScale, this.gameObject), Quaternion.identity);
                jointObject.name = jt.ToString();
                jointObject.transform.parent = this.gameObject.transform;
                childJoints.Add(jointObject);

                //assign two joints positions
                Vector3 jointPos = jointObject.transform.position;
                Vector3 nextJointPos = body.Joints[boneMap[jt]].ToVector3(bodyScale, this.gameObject);

                //create the second joints to the last
                float rad = Mathf.Atan2(nextJointPos.y - jointPos.y, nextJointPos.x - jointPos.x);
                float jointsDist = Vector3.Distance(jointPos, nextJointPos);
                float r = jointPrefab.GetComponent<SpriteRenderer>().bounds.size.x / 2;
                float childJointNum = (jointsDist - r) / (2 * r);
                //for(int i = 1; jointsDist - (r + r * 2 * i) > 0; i++)
                for (int i = 1; i <= childJointNum; i++)
                {
                    Vector3 childJointPos = new Vector3(jointPos.x + Mathf.Cos(rad) * 2*r * i , jointPos.y + Mathf.Sin(rad) * 2 * r * i, 0);
                    GameObject childJointObject = Instantiate(childJointPrefab, childJointPos, Quaternion.identity);
                    childJointObject.transform.parent = this.gameObject.transform;
                    childJoints.Add(jointObject);
                }


                //Vector3 jointsVector = new Vector3(jointPos.x - nextJointPos.x, jointPos.y - nextJointPos.y, 0);
                //Vector3 jointsUnit = jointsVector.normalized;
                //float jointsDist = Vector3.Distance(jointPos, nextJointPos);
                //float r = jointPrefab.GetComponent<SpriteRenderer>().bounds.size.x / 2;
                //float num = (jointsDist - r) / (2 * r);
                //Debug.Log(num);
                ////for(int i = 1; jointsDist - (r + r * 2 * i) > 0; i++)
                //for (int i = 1; i <= num; i++)
                //{
                //    Vector3 childJointPos = new Vector3(jointPos.x + jointsUnit.x * r * 2 * i, jointPos.y + jointsUnit.y * r * 2 * i, 0);
                //    GameObject childJointObject = Instantiate(jointPrefab, childJointPos, Quaternion.identity);
                //    childJointObject.transform.parent = this.gameObject.transform;
                //    childJoints.Add(jointObject);
                //}


                bones.Add(jt, childJoints);
  
            }
        }
    }//constructor

    public void refreshBodyObject()
    {
        Destroy(this.gameObject);
    }

}

public static class JointExtensions
{
    public static Vector3 ToVector3(this Windows.Kinect.Joint joint, float bodyScale, GameObject parentObject)
    //=> new Vector3(joint.Position.X * 10, joint.Position.Y * 10, joint.Position.Z * 10);
    {
        float kinectWidth = 1920;
        float kinectHeight = 1080;

        //convert la to screen address
        float wRatio = kinectWidth / Screen.width;
        float hRatio = kinectHeight / Screen.height;

        Vector3 pa = new Vector3(0, parentObject.transform.position.y, 0);//world adress of parent
        Vector3 wa = new Vector3(joint.Position.X, joint.Position.Y + pa.y, 0); //world address
        wa = new Vector3(wa.x * bodyScale, wa.y * bodyScale, wa.z * bodyScale); // world address of this joint

        // Vector3 screenLa = cam.WorldToViewportPoint(la);
        Vector3 la = new Vector3(wa.x * wRatio , wa.y * hRatio, wa.z);
        return la;
    }
        //new Vector3(joint.Position.X * bodyScale, joint.Position.Y * bodyScale , joint.Position.Z * bodyScale);
}

