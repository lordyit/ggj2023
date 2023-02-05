using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    public static List<EnemyOne> EnemyOne;
    public static List<EnemyTwo> EnemyTwo;
    public static List<NormalTree> NormalTree;

    [SerializeField] Portal _portal;

    private void Awake()
    {
        Instance = this;
        EnemyOne = new List<EnemyOne>();
        EnemyTwo = new List<EnemyTwo>();
        NormalTree = new List<NormalTree>();
    }

    public void CheckEndLevel()
    {
        if (NormalTree.Count > 0) return;
        if (EnemyOne.Count > 0) return;
        if (EnemyTwo.Count > 0) return;

        _portal.OpenGate();
    }
}
