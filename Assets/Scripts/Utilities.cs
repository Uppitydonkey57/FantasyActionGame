using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utilities
{
    //General Functions
    public static float HypotenuseLength(float sideALength, float sideBLength)
    {
        return Mathf.Sqrt(sideALength * sideALength + sideBLength * sideBLength);
    }

    public static Vector2 GetWorldPositionOfCanvasElement(RectTransform element)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(element, element.position, Camera.main, out var result);
        return result;
    }

    public static void DeleteChildren(this Transform t)
    {
        foreach (Transform child in t) Object.Destroy(child.gameObject);
    }

    public static Vector2 GetAngleDistance(float angle)
    {
        return new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
    }

    public static Vector3 MouseWorldPos3D(Vector2 screenPos, Camera camera = null) 
    {
        Debug.Log(camera);

        Camera chosenCamera = (camera != null) ? camera : Camera.main;
        Ray ray = chosenCamera.ScreenPointToRay(screenPos);
        if (Physics.Raycast(ray, out RaycastHit raycastHit)) 
        {
            return raycastHit.point;
        }

        return Vector3.zero;
    }

    public static T GetComponentInPrefab<T>(this GameObject gameObject) 
    {
        T inObject = gameObject.GetComponent<T>();
        T inChildren = gameObject.GetComponentInChildren<T>();

        if (inObject != null)
        {
            return inObject;
        } else if (inChildren != null)
        {
            return inChildren;
        }

        return default;
    }

    //Game Specific Functions
}

//Functions From
//https://docs.unity3d.com/ScriptReference/Mathf.Sqrt.html
//https://www.youtube.com/watch?v=JOABOQMurZo
