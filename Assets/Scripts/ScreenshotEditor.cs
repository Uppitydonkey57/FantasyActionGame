using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;

#if UNITY_EDITOR
public class ScreenshotEditor : EditorWindow
{
    string previousScene;

    string path = "/Graphics/Image";

    [MenuItem("Tools/Screenshot Editor")]
    public static void ShowWindow()
    {
        GetWindow<ScreenshotEditor>("Screenshot Editor");
    }

    private void OnGUI()
    {
        GUILayout.Space(12);
        if (GUILayout.Button("Take Screenshot"))
        {
            GameObject tempCamera = new GameObject();
            tempCamera.transform.position = SceneView.lastActiveSceneView.camera.transform.position;
            tempCamera.transform.rotation = SceneView.lastActiveSceneView.camera.transform.rotation;
            Camera camera = tempCamera.AddComponent<Camera>();
            camera.clearFlags = CameraClearFlags.Nothing;
            tempCamera.AddComponent<PhotoTaker>().Photo(path);
            
        }
        GUILayout.Label("Path:");
        path = GUILayout.TextField(path);

        GUILayout.Space(12);
        GUILayout.Label("Scene Loading:");

        if (GUILayout.Button("Load Empty Scene"))
        {
            EditorSceneManager.OpenScene("Assets/Scenes/Empty.unity");
        }

        if (GUILayout.Button("Load Previous Scene"))
        {
            if (!string.IsNullOrEmpty(previousScene))
                SceneManager.LoadScene(previousScene);
            else
                Debug.LogWarning("You have not been in a previous scene!");

        }
    }
}

public class PhotoTaker : MonoBehaviour 
{
    bool triggered;

    public void Photo(string path)
    {
        if (!triggered)
        {
            StartCoroutine(TakePhoto(GetComponent<Camera>(), path));
            triggered = true;
        }
    }

    IEnumerator TakePhoto(Camera photoCamera, string path)
    {
        yield return new WaitForEndOfFrame();
        Debug.Log("PHOTO TAKEN");
        int width = Screen.width;
        int height = Screen.height;
        Texture2D screenshotTexture = new Texture2D(width, height, TextureFormat.ARGB32, false);
        Rect rect = new Rect(0, 0, width, height);
        screenshotTexture.ReadPixels(rect, 0, 0);
        screenshotTexture.Apply();
        for (int y = 0; y < height; y++)
            for (int x = 0; x < width; x++)
            {
                Color currentPixel = screenshotTexture.GetPixel(x, y);
                //if (y < 20)Debug.Log(currentPixel.r + " " + currentPixel.g + " " + currentPixel.b + " " + currentPixel.a);
                if (currentPixel == new Color(0, 0, 0, 0))
                    screenshotTexture.SetPixel(x, y, new Color(currentPixel.r, currentPixel.g, currentPixel.b, 0));
                else
                    screenshotTexture.SetPixel(x, y, new Color(currentPixel.r, currentPixel.g, currentPixel.b, 1));
            }
        byte[] byteArray = (screenshotTexture).EncodeToPNG();
        System.IO.File.WriteAllBytes(Application.dataPath + path + ".png", byteArray);
        AssetDatabase.Refresh();
        DestroyImmediate(gameObject);
    }
}
#endif