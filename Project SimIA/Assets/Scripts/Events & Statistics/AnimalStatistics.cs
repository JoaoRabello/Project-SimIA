using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class AnimalStatistics : MonoBehaviour
{
    public static AnimalStatistics Instance;

    private int monkeyCount = 0;
    private int hawkCount = 0;
    private float maxAnimals;

    public TextMeshProUGUI monkeyAmountText;
    public TextMeshProUGUI hawkAmountText;

    public Image monkeyAmountWedge;
    public Image hawkAmountWedge;

    private bool monkeyExtincted = false;
    private bool hawkExtincted = false;

    private MonkeyStats monkeyStats;
    private HawkStats hawkStats;

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        monkeyAmountText.text = "Macacos: " + monkeyCount.ToString();
        hawkAmountText.text = "Gaviões: " + hawkCount.ToString();

        PieGraphic();

        if (monkeyCount == 0 && !monkeyExtincted)
        {
            GuideEventObserver.Instance.ExtinguishAnimal(1);
            EventLogger.Instance.LogEvent(EventType.MonkeyExtinction);
            monkeyExtincted = true;
        }
        else
        {
            if(hawkCount == 0 && !hawkExtincted)
            {
                GuideEventObserver.Instance.ExtinguishAnimal(2);
                EventLogger.Instance.LogEvent(EventType.HawkExctinction);
                hawkExtincted = true;
            }
        }

        if(monkeyCount <= 0 && hawkCount <= 0)
        {
            EventLogger.Instance.SetMonkeyStats(monkeyStats);
            EventLogger.Instance.SetHawkStats(hawkStats);
            EventLogger.Instance.EndSimulation();
        }
    }

    private void PieGraphic()
    {
        maxAnimals = hawkCount + monkeyCount;

        monkeyAmountWedge.fillAmount = maxAnimals == 0? 0 : monkeyCount / maxAnimals;
        hawkAmountWedge.fillAmount = maxAnimals == 0 ? 0 : hawkCount / maxAnimals;

        hawkAmountWedge.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, -monkeyAmountWedge.fillAmount * 360f));
    }

    public void AddAnimal(Monkey monkey)
    {
        monkeyCount++;

        if(monkeyStats.maxPopulation <= monkeyCount)
        {
            monkeyStats.maxPopulation = monkeyCount;
        }
    }

    public void AddAnimal(Hawk hawk)
    {
        hawkCount++;

        if (hawkStats.maxPopulation <= hawkCount)
        {
            hawkStats.maxPopulation = hawkCount;
        }
    }

    public void RemoveAnimal(Monkey monkey, DeathType type)
    {
        monkeyCount--;
        
        switch (type)
        {
            case DeathType.Hunt:
                monkeyStats.huntDeath++;
                break;
            case DeathType.Hunger:
                monkeyStats.hungerDeath++;
                break;
            case DeathType.Thirst:
                monkeyStats.thirstDeath++;
                break;
            case DeathType.Fire:
                monkeyStats.fireDeath++;
                break;
        }
    }

    public void RemoveAnimal(Hawk hawk, DeathType type)
    {
        hawkCount--;

        switch (type)
        {
            case DeathType.Hunger:
                hawkStats.hungerDeath++;
                break;
            case DeathType.Thirst:
                hawkStats.thirstDeath++;
                break;
            case DeathType.Fire:
                hawkStats.fireDeath++;
                break;
        }
    }

    public void SetInitialMonkeyAmount(int amount)
    {
        monkeyStats.initialPopulation = amount;
    }

    public void SetInitialHawkAmount(int amount)
    {
        hawkStats.initialPopulation = amount;
    }

}

public enum DeathType
{
    Hunt,
    Hunger,
    Thirst,
    Fire
}

public struct MonkeyStats
{
    public int huntDeath;
    public int hungerDeath;
    public int thirstDeath;
    public int fireDeath;
    public int initialPopulation;
    public int maxPopulation;
}

public struct HawkStats
{
    public int hungerDeath;
    public int thirstDeath;
    public int fireDeath;
    public int initialPopulation;
    public int maxPopulation;
}