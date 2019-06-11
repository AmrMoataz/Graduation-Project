using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 1- Get frame Info from detection API (X, Y, width, height).
/// 2- Create an empty object in the position of X and Y
/// (considering Y to be the Z axis, and Y starts from the ground level).
/// 3- give the object a collider and make the boundery of the collider depending on
/// the width and height.
/// </summary>
public class MockingColliderGeneration : MonoBehaviour
{
    [System.Serializable]
    public struct BoundingBox
    {
       public float X_axis;
       public float Y_axis;
       public float Width;
       public float Height;
    }

    
    public float GroundLevel = 0;
    public float MinX = -10;
    public float MaxX = 10;
    public float MinY = -10;
    public float MaxY = 10;
    public float MinWidth = 5;
    public float MaxWidth = 50;
    public float MinHeight = 5;
    public float MaxHeight = 50;

    [SerializeField]
    private BoundingBox ObjectInfo;


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            MockingRandomization_LOL();
            GenerateCollider();
        }

        
    }

    private void MockingRandomization_LOL()
    {
        ObjectInfo.X_axis = Random.Range(MinX, MaxX);
        ObjectInfo.Y_axis = Random.Range(MinY, MaxY);
        ObjectInfo.Width = Random.Range(MinWidth, MaxWidth);
        ObjectInfo.Height = Random.Range(MinHeight, MaxHeight);
    }

    /// <summary>
    /// Considering that we got the fram info from the API
    /// </summary>
	private void GenerateCollider()
    {
        GameObject newObject = new GameObject("Detected Object");
        newObject.transform.position = new Vector3(ObjectInfo.X_axis, GroundLevel, ObjectInfo.Y_axis);
        newObject.transform.localScale = new Vector3(ObjectInfo.Width, ObjectInfo.Height, 1);
        BoxCollider Collider = newObject.AddComponent<BoxCollider>();

        #region Only For Visualization

        //GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        //cube.transform.position = new Vector3(ObjectInfo.X_axis, GroundLevel, ObjectInfo.Y_axis);
        //cube.transform.localScale = new Vector3(ObjectInfo.Width, ObjectInfo.Height, 1);
        //cube.AddComponent<BoxCollider>();

        #endregion

    }
}
