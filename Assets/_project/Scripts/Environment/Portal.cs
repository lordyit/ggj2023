using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public bool Open;
    public int NextSceneIndex;

    public void OpenGate()
    {
        Open = true;
        GetComponentInChildren<MeshRenderer>().enabled = false;
    }

    public void EnterGate()
    {
        SceneManager.LoadScene(NextSceneIndex);
    }
}
