using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GuideEventObserver : MonoBehaviour
{
    public static GuideEventObserver Instance;

    private int animalSpawnCount = 0;
    private int animalExtinguishCount = 0;

    public UnityEvent animalSpawnEvent;
    public UnityEvent monkeyExtinguishEvent;
    public UnityEvent hawkExtinguishEvent;

    private void Awake()
    {
        Instance = this;
    }

    public void FirstAnimalSpawn()
    {
        if (animalSpawnCount == 0)
        {
            animalSpawnEvent.Invoke();
            animalSpawnCount++;
        }
    }

    public void ExtinguishAnimal(int animal)
    {
        if(animalExtinguishCount == 0)
        {
            switch (animal)
            {
                case 1:
                    monkeyExtinguishEvent.Invoke();
                    break;
                case 2:
                    hawkExtinguishEvent.Invoke();
                    break;
            }
            animalExtinguishCount++;
        }
    }
}
