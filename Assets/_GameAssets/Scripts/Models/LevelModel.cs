using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

[CreateAssetMenu(fileName = "Level Data", menuName = "Magic Tile/New level Data", order = 1)]
public class LevelModel : ScriptableObject
{
    public PlayableAsset LevelSong;
    public int MaxScore;
    public float TileFallSpeed;
}
