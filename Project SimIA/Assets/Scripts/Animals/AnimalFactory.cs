using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalFactory : MonoBehaviour
{
    public static AnimalFactory Instance;
    [SerializeField] private GameObject maleMonkey;
    [SerializeField] private GameObject femaleMonkey;
    [SerializeField] private GameObject maleHawk;
    [SerializeField] private GameObject femaleHawk;
    [SerializeField] private GameObject babyMonkey;
    [SerializeField] private GameObject babyHawk;

    void Awake()
    {
        Instance = this;
    }
    
    public static void CreateMonkey(float speed, float foodViewRange, float waterViewRange, Vector3 spawnPosition, Transform parent)
    {
        MonkeyDNA dna = new MonkeyDNA();
        Mutation mutation = new Mutation();

        dna.speed = mutation.MutateGene(speed, DNA.SPEED_MIN, DNA.SPEED_MAX);
        dna.foodViewRange = mutation.MutateGene(foodViewRange, DNA.FOODVIEWRANGE_MIN, DNA.FOODVIEWRANGE_MAX);
        dna.waterViewRange = mutation.MutateGene(waterViewRange, DNA.WATERVIEWRANGE_MIN, DNA.FOODVIEWRANGE_MAX);
        
        dna.sex = mutation.RandomizeSex();

        Monkey monkey;

        if(dna.sex == BiologicalSex.Male)
        {
            monkey = Instantiate(Instance.maleMonkey, spawnPosition, Quaternion.identity, parent).GetComponent<Monkey>();
        }
        else
        {
            monkey = Instantiate(Instance.femaleMonkey, spawnPosition, Quaternion.identity, parent).GetComponent<Monkey>();
        }

        AnimalStatistics.Instance.AddAnimal(monkey);
        monkey.Initialize(dna);
    }

    public static void CreateMonkey(float speed, float foodViewRange, float waterViewRange, Vector3 spawnPosition, BiologicalSex sex)
    {
        MonkeyDNA dna = new MonkeyDNA();
        Mutation mutation = new Mutation();

        dna.speed = mutation.MutateGene(speed, DNA.SPEED_MIN, DNA.SPEED_MAX);
        dna.foodViewRange = mutation.MutateGene(foodViewRange, DNA.FOODVIEWRANGE_MIN, DNA.FOODVIEWRANGE_MAX);
        dna.waterViewRange = mutation.MutateGene(waterViewRange, DNA.WATERVIEWRANGE_MIN, DNA.FOODVIEWRANGE_MAX);

        dna.sex = sex;

        Monkey monkey;

        if (dna.sex == BiologicalSex.Male)
        {
            monkey = Instantiate(Instance.maleMonkey, spawnPosition, Quaternion.identity).GetComponent<Monkey>();
        }
        else
        {
            monkey = Instantiate(Instance.femaleMonkey, spawnPosition, Quaternion.identity).GetComponent<Monkey>();
        }

        AnimalStatistics.Instance.AddAnimal(monkey);
        monkey.Initialize(dna);
    }

    public static void CreateHawk(float speed, float foodViewRange, float waterViewRange, float flybySpeedRate, Vector3 spawnPosition, Transform parent)
    {
        HawkDNA dna = new HawkDNA();
        Mutation mutation = new Mutation();

        dna.speed = mutation.MutateGene(speed, DNA.SPEED_MIN, DNA.SPEED_MAX);
        dna.foodViewRange = mutation.MutateGene(foodViewRange, DNA.FOODVIEWRANGE_MIN, DNA.FOODVIEWRANGE_MAX);
        dna.waterViewRange = mutation.MutateGene(waterViewRange, DNA.WATERVIEWRANGE_MIN, DNA.FOODVIEWRANGE_MAX);
        dna.flybySpeedRate = mutation.MutateGene(flybySpeedRate, HawkDNA.FLYBY_SPEEDRATE_MIN, HawkDNA.FLYBY_SPEEDRATE_MAX);

        dna.sex = mutation.RandomizeSex();

        Hawk hawk;

        if (dna.sex == BiologicalSex.Male)
        {
            hawk = Instantiate(Instance.maleHawk, spawnPosition, Quaternion.identity, parent).GetComponent<Hawk>();
        }
        else
        {
            hawk = Instantiate(Instance.femaleHawk, spawnPosition, Quaternion.identity, parent).GetComponent<Hawk>();
        }

        AnimalStatistics.Instance.AddAnimal(hawk);
        hawk.Initialize(dna);
    }

    public static void CreateHawk(float speed, float foodViewRange, float waterViewRange, float flybySpeedRate, Vector3 spawnPosition, BiologicalSex sex)
    {
        HawkDNA dna = new HawkDNA();
        Mutation mutation = new Mutation();

        dna.speed = mutation.MutateGene(speed, DNA.SPEED_MIN, DNA.SPEED_MAX);
        dna.foodViewRange = mutation.MutateGene(foodViewRange, DNA.FOODVIEWRANGE_MIN, DNA.FOODVIEWRANGE_MAX);
        dna.waterViewRange = mutation.MutateGene(waterViewRange, DNA.WATERVIEWRANGE_MIN, DNA.FOODVIEWRANGE_MAX);
        dna.flybySpeedRate = mutation.MutateGene(flybySpeedRate, HawkDNA.FLYBY_SPEEDRATE_MIN, HawkDNA.FLYBY_SPEEDRATE_MAX);

        dna.sex = sex;

        Hawk hawk;

        if (dna.sex == BiologicalSex.Male)
        {
            hawk = Instantiate(Instance.maleHawk, spawnPosition, Quaternion.identity).GetComponent<Hawk>();
        }
        else
        {
            hawk = Instantiate(Instance.femaleHawk, spawnPosition, Quaternion.identity).GetComponent<Hawk>();
        }

        AnimalStatistics.Instance.AddAnimal(hawk);
        hawk.Initialize(dna);
    }

    public static void CreateBabyMonkey(MonkeyDNA motherDNA, MonkeyDNA fatherDNA, Transform mother)
    {
        MonkeyDNA dna = new MonkeyDNA();
        Mutation mutation = new Mutation();
        Crossover crossover = new Crossover();

        dna.speed = mutation.MutateGene(crossover.Cross(motherDNA.speed, fatherDNA.speed), DNA.SPEED_MIN, DNA.SPEED_MAX);
        dna.foodViewRange = mutation.MutateGene(crossover.Cross(motherDNA.foodViewRange, fatherDNA.foodViewRange), DNA.FOODVIEWRANGE_MIN, DNA.FOODVIEWRANGE_MAX);
        dna.waterViewRange = mutation.MutateGene(crossover.Cross(motherDNA.waterViewRange, fatherDNA.waterViewRange), DNA.WATERVIEWRANGE_MIN, DNA.FOODVIEWRANGE_MAX);

        dna.sex = mutation.RandomizeSex();

        var monkey = Instantiate(Instance.babyMonkey, mother.position, Quaternion.identity).GetComponent<BabyMonkey>();

        AnimalStatistics.Instance.AddAnimal(monkey.matureMaleMonkey);
        monkey.Initialize(dna);
    }

    public static void CreateBabyHawk(HawkDNA motherDNA, HawkDNA fatherDNA, Transform mother)
    {
        HawkDNA dna = new HawkDNA();
        Mutation mutation = new Mutation();
        Crossover crossover = new Crossover();

        dna.speed = mutation.MutateGene(crossover.Cross(motherDNA.speed, fatherDNA.speed), DNA.SPEED_MIN, DNA.SPEED_MAX);
        dna.foodViewRange = mutation.MutateGene(crossover.Cross(motherDNA.foodViewRange, fatherDNA.foodViewRange), DNA.FOODVIEWRANGE_MIN, DNA.FOODVIEWRANGE_MAX);
        dna.waterViewRange = mutation.MutateGene(crossover.Cross(motherDNA.waterViewRange, fatherDNA.waterViewRange), DNA.WATERVIEWRANGE_MIN, DNA.FOODVIEWRANGE_MAX);

        var hawk = Instantiate(Instance.babyHawk, mother.position, Quaternion.identity).GetComponent<BabyHawk>();

        AnimalStatistics.Instance.AddAnimal(hawk.matureHawk);
        hawk.Initialize(dna);
    }

}