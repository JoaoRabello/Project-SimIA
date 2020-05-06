using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BiomeAttributes", menuName = "WorldGen/Biome Attribute")]
public class BiomeAttributes : ScriptableObject
{
    public string biomeName;

    public int solidGroundHeight;
    public int terrainHeight;
    public float terrainScale;

    [Header("Biome Aesthetics")]
    public Color sunLightColor;
    public byte[] blockTypes;
    public Lode[] lodes;

    [Header("Biome Entities")]
    public bool hasTrees;
    public AnimalType[] animals;
    public GameObject trees;
}

public enum AnimalType
{
    Monkey,
    Hawk
}

[System.Serializable]
public class Lode
{
    public string nodeName;
    public byte blockID;
    public int minHeight;
    public int maxHeight;
    public float scale;
    public float threshold;
    public float noiseOffset;
}
