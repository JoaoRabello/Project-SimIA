using UnityEngine;
public class Individual : MonoBehaviour
{
    private DNA dna = new DNA();
    private SpriteRenderer spRenderer;

    void Start()
    {
        spRenderer = GetComponent<SpriteRenderer>();

        RandomizeAllTraits();
    }

    public void RandomizeAllTraits()
    {
        dna.height = Random.Range(DNA.HEIGHT_MIN, DNA.HEIGHT_MAX);
        dna.width = Random.Range(DNA.WIDTH_MIN, DNA.WIDTH_MAX);
        dna.r = Random.Range(DNA.R_MIN, DNA.R_MAX);
        dna.g = Random.Range(DNA.G_MIN, DNA.G_MAX);
        dna.b = Random.Range(DNA.B_MIN, DNA.B_MAX);

        RefreshDisplay();
    }

    public void SingleInherit(float mutationRange)
    {
        Vector2 randomSizeMutation = new Vector2(RandomizeGene(mutationRange), RandomizeGene(mutationRange));
        Vector3 randomColorMutation = new Vector3(RandomizeGene(mutationRange), RandomizeGene(mutationRange), RandomizeGene(mutationRange));

        dna.width = Bound(dna.width + randomSizeMutation.x, DNA.WIDTH_MIN, DNA.WIDTH_MAX);
        dna.height = Bound(dna.height + randomSizeMutation.y, DNA.HEIGHT_MIN, DNA.HEIGHT_MAX);
        dna.r = Bound(dna.r + randomColorMutation.x, DNA.R_MIN, DNA.R_MAX);
        dna.g = Bound(dna.g + randomColorMutation.y, DNA.G_MIN, DNA.G_MAX);
        dna.b = Bound(dna.b + randomColorMutation.z, DNA.B_MIN, DNA.B_MAX);

        RefreshDisplay();
    }

    private float Bound(float value, float min, float max)
    {
        if (value > max)
            return max;
        else if (value < min)
            return min;
        else
            return value;
    }

    private float RandomizeGene(float mutationRange)
    {
        return Random.Range(-mutationRange, mutationRange);
    }

    private void RefreshDisplay()
    {
        spRenderer.color = new Color(dna.r, dna.g, dna.b);
        transform.localScale = new Vector3(dna.width, dna.height, 1f);
    }
}
