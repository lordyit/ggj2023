using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public int Lives;

    public void ChangeLives(int change)
    {
        Lives -= change;
        Debug.Log("Damage taken");
    }
}
