using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject CameraPlane;

    private void Start()
    {
        if(Application.isMobilePlatform)
        {
            GameObject cameraParent = new GameObject("cameraParent");
            cameraParent.transform.position = this.transform.position;
            transform.parent = cameraParent.transform;
            cameraParent.transform.Rotate(Vector3.right, -90);
            cameraParent.transform.Rotate(Vector3.up, -180);
        }

        #region Comment when not using Unity Remote (MUST COMMENT BEFORE BUILD)
//        GameObject CamParent = new GameObject("cameraParent");
//        CamParent.transform.position = this.transform.position;
//        transform.parent = CamParent.transform;
//        CamParent.transform.Rotate(Vector3.right, -90);
//        CamParent.transform.Rotate(Vector3.up, -180);
        #endregion

        Input.gyro.enabled = true;

        WebCamTexture webCamTexture = new WebCamTexture();
        CameraPlane.GetComponent<MeshRenderer>().material.mainTexture = webCamTexture;
        webCamTexture.Play(); 
    }

    private void Update()
    {
        Quaternion cameraRotation = new Quaternion(Input.gyro.attitude.x, Input.gyro.attitude.y, -Input.gyro.attitude.z, -Input.gyro.attitude.w);
        transform.localRotation = cameraRotation;
    }
}
