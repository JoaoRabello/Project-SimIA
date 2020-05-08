using UnityEngine;

public class DNA
{
    public const float SPEED_MAX = 10f;
    public const float SPEED_MIN = 2f;
    public float speed;

    public const float FOODVIEWRANGE_MAX = 30f;
    public const float FOODVIEWRANGE_MIN = 5f;
    public float foodViewRange;

    public const float WATERVIEWRANGE_MAX = 100f;
    public const float WATERVIEWRANGE_MIN = 20f;
    public float waterViewRange;

}

public class Mutation
{
    private const float MUTATION_MAX = 10;
    private const float MUTATION_MIN = -10;

    public float MutateGene(float gene)
    {
        float mutationAmount = Random.Range(0, MUTATION_MAX);

        return gene + mutationAmount;
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