using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

[System.Serializable]
public class SceneChange 
{
    enum SwitchMode { Name, AddBuildOrder, SetBuildOrder }
    [SerializeField] private SwitchMode switchMode;

    [SerializeField] private string sceneName;

    [SerializeField] private int buildNumber;

    public void ChangeScene() 
    {
        switch (switchMode) 
        {
            case SwitchMode.Name:
                SceneManager.LoadScene(sceneName);
                break;

            case SwitchMode.AddBuildOrder:
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + buildNumber);
                break;

            case SwitchMode.SetBuildOrder:
                SceneManager.LoadScene(buildNumber);
                break;
        }
    }
}

#if UNITY_EDITOR

// [CustomPropertyDrawer(typeof(SceneChange), true)]
// public class SceneChagneEditor : PropertyDrawer
// {
//     public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
//     {
//         EditorGUI.BeginProperty(position, label, property);

//         position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

//         Rect rect = new Rect(position.position, Vector2.one * 20);

//         if (EditorGUI.DropdownButton(rect, new GUIContent((Texture2D)Resources.Load("Resources/unity_builtin_extra/Library/DropdownArrow")), 
//             FocusType.Keyboard, new GUIStyle() 
//             {
//                 fixedWidth = 50f, border = new RectOffset(1,1,1,1)
//             })) 
//         {
//             GenericMenu menu = new GenericMenu();
//             menu.AddItem(new GUIContent("Name"), false, () => {Debug.Log("hi");});
//         }

//         EditorGUI.EndProperty();
//     }
// }

#endif 