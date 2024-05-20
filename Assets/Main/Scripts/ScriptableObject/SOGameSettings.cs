using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "GameSettings" , menuName = "ScriptableObject/GameSettings", order = 1)]
public class SOGameSettings : ScriptableObject
{
    public bool Locked;
    public bool IsComplated;

    public int CardCount;
    public int GridX;
    public int GridY;

    public float CameraSize;
}