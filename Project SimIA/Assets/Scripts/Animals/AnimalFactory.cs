using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalFactory : MonoBehaviour
{
    public static AnimalFactory instance;
    [SerializeField] private GameObject monkey;
    [SerializeField] private GameObject hawk;

    void Awake()
    {
        instance = this;
    }
    
    public static void CreateMonkey(float speed, float foodViewRange, float waterViewRange, Vector3 spawnPosition, Transform parent)
    {
        DNA dna = new DNA();

        dna.speed = speed;
        dna.foodViewRange = foodViewRange;
        dna.waterViewRange = waterViewRange;

        var monkey = Instantiate(instance.monkey, spawnPosition, Quaternion.identity, parent).GetComponent<Monkey>();

        monkey.Initialize(dna);
    }

    public static void CreateHawk(float speed, float foodViewRange, float waterViewRange, Vector3 spawnPosition, Transform parent)
    {
        DNA dna = new DNA();

        dna.speed = speed;
        dna.foodViewRange = foodViewRange;
        dna.waterViewRange = waterViewRange;

        var hawk = Instantiate(instance.hawk, spawnPosition, Quaternion.identity, parent).GetComponent<Hawk>();

        hawk.Initialize(dna);
    }

}
