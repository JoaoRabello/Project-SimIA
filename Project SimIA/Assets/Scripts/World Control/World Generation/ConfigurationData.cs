using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WorldAnimalType { DEFAULT, RAW }
public enum WorldBiome { DEFAULT, FOREST, MOUNTAINS, DESERT, TAIGA}

public static class ConfigurationData
{
    public static WorldAnimalType worldAnimalType;
    public static WorldBiome biome;

    public static void SetWorldAnimalType(WorldAnimalType worldAnimalType)
    {
        ConfigurationData.worldAnimalType = worldAnimalType;
    }

    public static void SetWorldBiome(WorldBiome biome)
    {
        ConfigurationData.biome = biome;
    }
}
