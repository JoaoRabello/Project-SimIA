using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalFactory : MonoBehaviour
{
    public static AnimalFactory instance;
    [SerializeField] private GameObject maleMonkey;
    [SerializeField] private GameObject femaleMonkey;
    [SerializeField] private GameObject maleHawk;
    [SerializeField] private GameObject femaleHawk;
    [SerializeField] private GameObject babyMonkey;
    [SerializeField] private GameObject babyHawk;

    void Awake()
    {
        instance = this;
    }
    
    public static void CreateMonkey(float speed, float foodViewRange, float waterViewRange, Vector3 spawnPosition, Transform parent)
    {
        MonkeyDNA dna = new MonkeyDNA();
        Mutation mutation = new Mutation();

        dna.speed = mutation.MutateGene(speed);
        dna.foodViewRange = mutation.MutateGene(foodViewRange);
        dna.waterViewRange = mutation.MutateGene(waterViewRange);
        
        dna.sex = mutation.RandomizeSex();

        Monkey monkey;

        if(dna.sex == BiologicalSex.Male)
        {
            monkey = Instantiate(instance.maleMonkey, spawnPosition, Quaternion.identity, parent).GetComponent<Monkey>();
        }
        else
        {
            monkey = Instantiate(instance.femaleMonkey, spawnPosition, Quaternion.identity, parent).GetComponent<Monkey>();
        }

        AnimalStatistics.Instance.AddAnimal(monkey);
        monkey.Initialize(dna);
    }

    public static void CreateMonkey(float speed, float foodViewRange, float waterViewRange, Vector3 spawnPosition, BiologicalSex sex)
    {
        MonkeyDNA dna = new MonkeyDNA();
        Mutation mutation = new Mutation();

        dna.speed = mutation.MutateGene(speed);
        dna.foodViewRange = mutation.MutateGene(foodViewRange);
        dna.waterViewRange = mutation.MutateGene(waterViewRange);

        dna.sex = sex;

        Monkey monkey;

        if (dna.sex == BiologicalSex.Male)
        {
            monkey = Instantiate(instance.maleMonkey, spawnPosition, Quaternion.identity).GetComponent<Monkey>();
        }
        else
        {
            monkey = Instantiate(instance.femaleMonkey, spawnPosition, Quaternion.identity).GetComponent<Monkey>();
        }

        AnimalStatistics.Instance.AddAnimal(monkey);
        monkey.Initialize(dna);
    }

    public static void CreateHawk(float speed, float foodViewRange, float waterViewRange, Vector3 spawnPosition, Transform parent)
    {
        HawkDNA dna = new HawkDNA();
        Mutation mutation = new Mutation();

        dna.speed = speed;
        dna.foodViewRange = mutation.MutateGene(foodViewRange);
        dna.waterViewRange = mutation.MutateGene(waterViewRange);
        
        dna.sex = mutation.RandomizeSex();

        Hawk hawk;

        if (dna.sex == BiologicalSex.Male)
        {
            hawk = Instantiate(instance.maleHawk, spawnPosition, Quaternion.identity, parent).GetComponent<Hawk>();
        }
        else
        {
            hawk = Instantiate(instance.femaleHawk, spawnPosition, Quaternion.identity, parent).GetComponent<Hawk>();
        }

        AnimalStatistics.Instance.AddAnimal(hawk);
        hawk.Initialize(dna);
    }

    public static void CreateHawk(float speed, float foodViewRange, float waterViewRange, Vector3 spawnPosition, BiologicalSex sex)
    {
        HawkDNA dna = new HawkDNA();
        Mutation mutation = new Mutation();

        dna.speed = speed;
        dna.foodViewRange = mutation.MutateGene(foodViewRange);
        dna.waterViewRange = mutation.MutateGene(waterViewRange);

        dna.sex = sex;

        Hawk hawk;

        if (dna.sex == BiologicalSex.Male)
        {
            hawk = Instantiate(instance.maleHawk, spawnPosition, Quaternion.identity).GetComponent<Hawk>();
        }
        else
        {
            hawk = Instantiate(instance.femaleHawk, spawnPosition, Quaternion.identity).GetComponent<Hawk>();
        }

        AnimalStatistics.Instance.AddAnimal(hawk);
        hawk.Initialize(dna);
    }

    public static void CreateBabyMonkey(MonkeyDNA babyDNA, Transform mother)
    {
        MonkeyDNA dna = new MonkeyDNA();
        Mutation mutation = new Mutation();

        dna.speed = mutation.MutateGene(babyDNA.speed);
        dna.foodViewRange = mutation.MutateGene(babyDNA.foodViewRange);
        dna.waterViewRange = mutation.MutateGene(babyDNA.waterViewRange);

        var monkey = Instantiate(instance.babyMonkey, mother.position, Quaternion.identity).GetComponent<BabyMonkey>();

        AnimalStatistics.Instance.AddAnimal(monkey.matureMonkey);
        monkey.Initialize(dna);
    }

    public static void CreateBabyHawk(HawkDNA babyDNA, Transform mother)
    {
        HawkDNA dna = new HawkDNA();
        Mutation mutation = new Mutation();

        dna.speed = mutation.MutateGene(babyDNA.speed);
        dna.foodViewRange = mutation.MutateGene(babyDNA.foodViewRange);
        dna.waterViewRange = mutation.MutateGene(babyDNA.waterViewRange);

        var hawk = Instantiate(instance.babyHawk, mother.position, Quaternion.identity).GetComponent<BabyHawk>();

        AnimalStatistics.Instance.AddAnimal(hawk.matureHawk);
        hawk.Initialize(dna);
    }

}