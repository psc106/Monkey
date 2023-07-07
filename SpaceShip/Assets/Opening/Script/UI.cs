using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    public void OnClickStart()
    {
        GFunc.LoadScene("PlayScene");
    }

    public void OnClickQuit()
    {
        Application.Quit();
    }
}
