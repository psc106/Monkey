using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public static partial class GFunc
{
    [System.Diagnostics.Conditional("DEBUG_MODE")]
    public static void Log(object message)
    {
#if DEBUG_MODE
        Debug.Log(message);
#endif
    }

    [System.Diagnostics.Conditional("DEBUG_MODE")]
    public static void AssertLog(bool condition)
    {
#if DEBUG_MODE
        Debug.Assert(condition);
#endif
    }

    //gameobject -> 텍스트 컴포넌트 : 텍스트 필드 값 수정
    public static void SetText(this GameObject target, string text)
    {
        Text textComponent = target.GetComponent<Text>();
        if (textComponent == null || textComponent == default) return;

        textComponent.text = text;
    }


    //gameobject -> 텍스트 컴포넌트 : 텍스트 필드 값 수정
    public static void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
