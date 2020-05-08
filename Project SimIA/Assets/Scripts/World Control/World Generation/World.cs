using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    private int seed;
    public BiomeAttributes biome;

    public Transform player;
    public Light sunLight;
    public Transform userCamera;
    public float cameraOffset;
    public Vector3 spawnPosition;

    public Material material;
    public BlockType[] blockTypes;

    ChunkAttributes[,] chunks = new ChunkAttributes[VoxelData.worldSizeInChunks, VoxelData.worldSizeInChunks];

    List<ChunkCoord> activeChunks = new List<ChunkCoord>();
    ChunkCoord playerChunkCoord;
    ChunkCoord playerLastChunkCoord;

    private void Start()
    {
        //seed = Random.Range(0, 250000);
        //Random.InitState(seed);

        spawnPosition = new Vector3((VoxelData.worldSizeInChunks * VoxelData.chunkWidth) / 2f, 5f, (VoxelData.worldSizeInChunks * VoxelData.chunkWidth) / 2f);
        GenerateWorld();
        playerLastChunkCoord = GetChunkCoordFromVector3(player.position);
    }

    private void Update()
    {
        playerChunkCoord = GetChunkCoordFromVector3(player.position);

        if (!playerChunkCoord.Equals(playerLastChunkCoord))
            CheckViewDistance();
    }

    ChunkCoord GetChunkCoordFromVector3(Vector3 pos)
    {
        int x = Mathf.FloorToInt(pos.x/VoxelData.chunkWidth);
        int z = Mathf.FloorToInt(pos.z/VoxelData.chunkWidth);

        return new ChunkCoord(x, z);
    }

    private void CheckViewDistance()
    {
        ChunkCoord coord = GetChunkCoordFromVector3(player.position);

        List<ChunkCoord> previouslyActiveChunks = new List<ChunkCoord>(activeChunks);

        for (int x = coord.x - VoxelData.viewDistanceInChunks; x < coord.x + VoxelData.viewDistanceInChunks; x++)
        {
            for (int z = coord.z - VoxelData.viewDistanceInChunks; z < coord.z + VoxelData.viewDistanceInChunks; z++)
            {
                if (IsChunkInWorld(new ChunkCoord(x, z)))
                {
                    if(chunks[x, z] == null)
                    {
                        CreateNewChunk(x, z);
                    }
                    else
                    {
                        if(!chunks[x, z].isActive)
                        {
                            chunks[x, z].isActive = true;
                            activeChunks.Add(new ChunkCoord(x, z));
                        }
                    }
                }

                for (int i = 0; i < previouslyActiveChunks.Count; i++)
                {
                    if (previouslyActiveChunks[i].Equals(new ChunkCoord(x, z)))
                    {
                        previouslyActiveChunks.RemoveAt(i);
                    }
                }
            }
        }

        foreach(ChunkCoord c in previouslyActiveChunks)
        {
            chunks[c.x, c.z].isActive = false;
        }
    }

    private void GenerateWorld()
    {
        for (int x = (VoxelData.worldSizeInChunks / 2) - VoxelData.viewDistanceInChunks; x < (VoxelData.worldSizeInChunks / 2) + VoxelData.viewDistanceInChunks; x++)
        {
            for (int z = (VoxelData.worldSizeInChunks / 2) - VoxelData.viewDistanceInChunks; z < (VoxelData.worldSizeInChunks / 2) + VoxelData.viewDistanceInChunks; z++)
            {
                CreateNewChunk(x, z);
            }
        }
        
        GenerateVegetation();
        SpawnAnimals();

        player.position = spawnPosition;
        userCamera.position = new Vector3(player.position.x - cameraOffset, userCamera.position.y, player.position.z - cameraOffset);
        transform.SetParent(player);
        sunLight.color = biome.sunLightColor;
    }

    public byte GetVoxel(Vector3 pos)
    {
        int yPos = Mathf.FloorToInt(pos.y);

        //Immutable Pass
        
        //If outside of the world, spawn air
        if (!IsVoxelInWorld(pos))
            return 0;
        //If at the bottom of the world, spawn bedrock
        if (yPos == 0)
            return 1;

        //Basic Terrain Pass
        int terrainHeight = Mathf.FloorToInt(biome.terrainHeight * Noise.Get2DPerlin(new Vector2(pos.x, pos.z), 0, biome.terrainScale)) + biome.solidGroundHeight;
        byte voxelValue;

        if (yPos == terrainHeight)
        {
            voxelValue = biome.blockTypes[0];
        }
        else
        {
            if(yPos < terrainHeight && yPos > terrainHeight - 4)
            {
                voxelValue = biome.blockTypes[1];
            }
            else
            {
                if (yPos > terrainHeight)
                    return 0;
                else
                    voxelValue = 2;
            }
        }

        //Second Pass
        if(voxelValue == 2)
        {
            foreach (Lode lode in biome.lodes)
            {
                if(yPos > lode.minHeight && yPos < lode.maxHeight)
                {
                    if(Noise.Get3DPerlin(pos, lode.noiseOffset, lode.scale, lode.threshold))
                    {
                        voxelValue = lode.blockID;
                    }
                }
            }
        }
        
        return voxelValue;
    }

    private void CreateNewChunk (int x, int z)
    {
        chunks[x,z] = new ChunkAttributes(new ChunkCoord(x, z), this);
        activeChunks.Add(new ChunkCoord(x, z));
    }

    private bool IsChunkInWorld(ChunkCoord coord)
    {
        if (coord.x > 0 && coord.x < VoxelData.worldSizeInChunks - 1 && coord.z > 0 && coord.z < VoxelData.worldSizeInChunks - 1)
            return true;
        else
            return false;
    }

    private bool IsVoxelInWorld(Vector3 pos)
    {
        if (pos.x >= 0 && pos.x < VoxelData.worldSizeInVoxels && pos.y >= 0 && pos.y < VoxelData.chunkHeight && pos.z >= 0 && pos.z < VoxelData.worldSizeInVoxels)
            return true;
        else
            return false;
    }

    private void GenerateVegetation()
    {
        if (biome.hasTrees)
        {
            Vector3 randomPos;
            int randomIndex;

            for (int i = 0; i < VoxelData.worldSizeInChunks; i++)
            {
                for (int j = 0; j < VoxelData.worldSizeInChunks; j++)
                {
                    randomIndex = chunks[i, j].voxelPositions.Count - Random.Range(1, VoxelData.chunkWidth);
                    randomPos = chunks[i, j].voxelPositions[randomIndex];
                    
                    if (IsVoxelInWorld(randomPos) && Noise.Get2DPerlin(new Vector2(randomPos.x, randomPos.z), 0, biome.terrainScale) > 0.5f)
                    {
                        Instantiate(biome.trees, randomPos, Quaternion.identity, transform);
                    }
                }
            }
        }
    }

    private void SpawnAnimals()
    {
        Vector3 randomPos;
        int randomIndex;
        for (int k = 0; k < biome.animals.Length; k++)
        {
            for (int i = 0; i < VoxelData.worldSizeInChunks; i++)
            {
                for (int j = 0; j < VoxelData.worldSizeInChunks; j++)
                {
                    randomIndex = chunks[i, j].voxelPositions.Count - Random.Range(1, VoxelData.chunkWidth);
                    randomPos = chunks[i, j].voxelPositions[randomIndex];

                    if (IsVoxelInWorld(randomPos) && Noise.Get2DPerlin(new Vector2(randomPos.x, randomPos.z), 0, biome.terrainScale) > 0.7f)
                    {
                        switch (biome.animals[k])
                        {
                            case AnimalType.Monkey:
                                AnimalFactory.CreateMonkey(5, 10, 50, randomPos, transform);
                                break;
                            case AnimalType.Hawk:
                                AnimalFactory.CreateHawk(3, 100, 200, new Vector3(randomPos.x, biome.terrainHeight + 10f, randomPos.z), transform);
                                break;
                        }
                    }
                }
            }
        }

        AnimalStatistics.Instance.UpdateStatistics();
    }
}

[System.Serializable]
public class BlockType
{
    public string blockName;
    public bool isSolid;

    [Header("Texture Values")]
    public int backFaceTexture;
    public int frontFaceTexture;
    public int topFaceTexture;
    public int bottomFaceTexture;
    public int leftFaceTexture;
    public int rightFaceTexture;

    public int GetTextureID(int faceIndex)
    {
        switch (faceIndex)
        {
            case 0:
                return backFaceTexture;
            case 1:
                return frontFaceTexture;
            case 2:
                return topFaceTexture;
            case 3:
                return bottomFaceTexture;
            case 4:
                return leftFaceTexture;
            case 5:
                return rightFaceTexture;
            default:
                Debug.LogError("Error in GetTextureID: invalid face index");
                return 0;
        }
    }
}
