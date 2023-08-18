using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// UNUSED
/// </summary>
public class EnemiesInLevels : ScriptableObject
{
    public static GameObject[] Level1Enemies { get; } = new GameObject[] { Resources.Load<GameObject>("Prefabs/Enemy") };
    public static GameObject[] Level2Enemies { get; } = new GameObject[] {
        Resources.Load<GameObject>("Prefabs/Enemy"),
        Resources.Load<GameObject>("Prefabs/Enemy Variant"),
    };

}
