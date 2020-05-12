using UnityEngine;

public class DNA
{
    public const float SPEED_MAX = 4f;
    public const float SPEED_MIN = 2f;
    public float speed;

    public const float FOODVIEWRANGE_MAX = 30f;
    public const float FOODVIEWRANGE_MIN = 5f;
    public float foodViewRange;

    public const float WATERVIEWRANGE_MAX = 100f;
    public const float WATERVIEWRANGE_MIN = 20f;
    public float waterViewRange;

    public const float REPRODUCTIONURGE_MAX = 3f;
    public const float REPRODUCTIONURGE_MIN = 1f;
    public float reproductionUrgeRate;

    public BiologicalSex sex;
}

public class MonkeyDNA : DNA
{
    public const float FAT_AMOUNT_MAX = 100f;
    public const float FAT_AMOUNT_MIN = 10f;
    public float fatAmount;
}

public class HawkDNA : DNA
{

}

public class Mutation
{
    private const float MUTATION_MAX = 2;
    private const float MUTATION_MIN = -10;

    public int mutationChance = 70;
    public int sexChance = 50;

    public float MutateGene(float gene)
    {
        if (Random.Range(0, 100) > mutationChance)
        {
            return gene + Random.Range(0, MUTATION_MAX);
        }
        else
        {
            return gene;
        }
    }

    public BiologicalSex RandomizeSex()
    {
        if (Random.Range(0, 100) > sexChance)
        {
            return BiologicalSex.Male;
        }
        else
        {
            return BiologicalSex.Female;
        }
    }

    private float Clamp(float value, float min, float max)
    {
        if(value >= max)
        {
            return max;
        }
        else
        {
            if(value <= min)
            {
                return min;
            }
            else
            {
                return value;
            }
        }
    }
}