using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class Guide : MonoBehaviour
{
    public TextMeshProUGUI baloon;
    public UnityEvent showGuide;
    public string[] animalTalks;
    public string[] monkeyExtinguishTalks;
    public string[] hawkExtinguishTalks;

    public void AnimalDialogue()
    {
        showGuide.Invoke();
        baloon.text = animalTalks[Random.Range(0, 3)];
        Time.timeScale = 0;
    }

    public void ExtinguishMonkey()
    {
        showGuide.Invoke();
        baloon.text = monkeyExtinguishTalks[0];
        Time.timeScale = 0;
    }

    public void ExtinguishHawk()
    {
        showGuide.Invoke();
        baloon.text = hawkExtinguishTalks[0];
        Time.timeScale = 0;
    }

    public void Resume()
    {
        Time.timeScale = 1;
    }
}
