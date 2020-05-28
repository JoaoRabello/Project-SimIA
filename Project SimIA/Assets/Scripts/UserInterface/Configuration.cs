using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Configuration : MonoBehaviour
{
    public void SetAnimalType(int type)
    {
        WorldAnimalType worldAnimalType;
        switch (type)
        {
            case 0:
                worldAnimalType = WorldAnimalType.DEFAULT;
                break;
            case 1:
                worldAnimalType = WorldAnimalType.RAW;
                break;
            default:
                worldAnimalType = WorldAnimalType.DEFAULT;
                break;
        }

        ConfigurationData.SetWorldAnimalType(worldAnimalType);
    }

    public void SetBiome(int type)
    {
        WorldBiome biome;
        switch (type)
        {
            case 0:
                biome = WorldBiome.DEFAULT;
                break;
            case 1:
                biome = WorldBiome.FOREST;
                break;
            case 2:
                biome = WorldBiome.MOUNTAINS;
                break;
            case 3:
                biome = WorldBiome.DESERT;
                break;
            case 4:
                biome = WorldBiome.TAIGA;
                break;
            default:
                biome = WorldBiome.DEFAULT;
                break;
        }

        ConfigurationData.SetWorldBiome(biome);
    }
}
