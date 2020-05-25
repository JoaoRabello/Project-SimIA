﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalFactory : MonoBehaviour
{
    public static AnimalFactory instance;
    [SerializeField] private GameObject monkey;
    [SerializeField] private GameObject hawk;
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

        var monkey = Instantiate(instance.monkey, spawnPosition, Quaternion.identity, parent).GetComponent<Monkey>();

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

        var hawk = Instantiate(instance.hawk, spawnPosition, Quaternion.identity, parent).GetComponent<Hawk>();

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

        AnimalStatistics.Instance.AddAnimal(hawk);
        hawk.Initialize(dna);
    }

}