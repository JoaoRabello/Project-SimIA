using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class AnimalStatistics : MonoBehaviour
{
    public static AnimalStatistics Instance;

    private List<Monkey> monkeys = new List<Monkey>();
    private List<Hawk> hawks = new List<Hawk>();

    public TextMeshProUGUI monkeyAmountText;
    public TextMeshProUGUI hawkAmountText;

    public Slider monkeyAmountSlider;
    public Slider hawkAmountSlider;

    private float initialMonkeyAmount;
    private float initialHawkAmount;

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        monkeyAmountText.text = "Macacos: " + monkeys.Count.ToString();
        hawkAmountText.text = "Gaviões: " + hawks.Count.ToString();

        monkeyAmountSlider.value = (monkeys.Count / initialMonkeyAmount);
        hawkAmountSlider.value = (hawks.Count / initialHawkAmount);
    }

    public void AddAnimal(Monkey monkey)
    {
        monkeys.Add(monkey);
    }

    public void AddAnimal(Hawk hawk)
    {
        hawks.Remove(hawk);
    }

    public void RemoveAnimal(Monkey monkey)
    {
        monkeys.Remove(monkey);
    }

    public void RemoveAnimal(Hawk hawk)
    {
        hawks.Remove(hawk);
    }

    public void UpdateStatistics()
    {
        Monkey[] monkeyArray = FindObjectsOfType<Monkey>();
        Hawk[] hawkArray = FindObjectsOfType<Hawk>();

        for (int i = 0; i < monkeyArray.Length; i++)
        {
            if (monkeyArray[i] != null)
            {
                monkeys.Add(monkeyArray[i]);
            }
        }
        for (int i = 0; i < hawkArray.Length; i++)
        {
            if(hawkArray[i] != null)
            {
                hawks.Add(hawkArray[i]);
            }
        }
        initialMonkeyAmount = monkeys.Count;
        initialHawkAmount = hawks.Count;
    }
}
