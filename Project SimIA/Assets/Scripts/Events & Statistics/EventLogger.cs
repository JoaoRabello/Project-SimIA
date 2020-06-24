using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class EventLogger : MonoBehaviour
{
    public static EventLogger Instance;
    public Image monkeyIcon;
    public Image hawkIcon;
    public Image fireIcon;

    private List<GeneralEvent> events = new List<GeneralEvent>();

    private float startTime;
    private float endTime;
    private float totalTime;

    public UnityEvent endSimulation;
    private bool hasSimulationEnded = false;
    
    [SerializeField] private Slider timeline;
    [SerializeField] private TextMeshProUGUI timeLabel;
    [SerializeField] private GameObject canvas;

    [Header("Monkey Stats")]
    [SerializeField] private TextMeshProUGUI monkeyInitialPopulationLabel;
    [SerializeField] private TextMeshProUGUI monkeyMaxPopulationLabel;
    [SerializeField] private TextMeshProUGUI monkeyHuntLabel;
    [SerializeField] private TextMeshProUGUI monkeyHungerLabel;
    [SerializeField] private TextMeshProUGUI monkeyThirstLabel;
    [SerializeField] private TextMeshProUGUI monkeyFireLabel;
    private MonkeyStats monkeyStats;

    [Header("Hawk Stats")]
    [SerializeField] private TextMeshProUGUI hawkInitialPopulationLabel;
    [SerializeField] private TextMeshProUGUI hawkMaxPopulationLabel;
    [SerializeField] private TextMeshProUGUI hawkHungerLabel;
    [SerializeField] private TextMeshProUGUI hawkThirstLabel;
    [SerializeField] private TextMeshProUGUI hawkFireLabel;
    private HawkStats hawkStats;

    private void Awake()
    {
        Instance = this;
        startTime = Time.time;
        StartSimulation();
    }

    public void StartSimulation()
    {
        Time.timeScale = 1;
        hasSimulationEnded = false;
    }

    public void LogEvent(EventType type)
    {
        GeneralEvent gEvent = new GeneralEvent(type, Time.time);

        events.Add(gEvent);
    }

    public List<GeneralEvent> GetEvents()
    {
        return events;
    }

    public void SetMonkeyStats(MonkeyStats stats)
    {
        monkeyStats = stats;
    }

    public void SetHawkStats(HawkStats stats)
    {
        hawkStats = stats;
    }

    public void EndSimulation()
    {
        if (!hasSimulationEnded && ConfigurationData.worldAnimalType == WorldAnimalType.DEFAULT)
        {
            endTime = Time.time;
            totalTime = endTime - startTime;
            
            endSimulation.Invoke();

            SetStats();
            SetTimeline();

            Time.timeScale = 0;
            hasSimulationEnded = true;
        }
    }

    private void SetStats()
    {
        monkeyInitialPopulationLabel.text = "Inicial: " + monkeyStats.initialPopulation;
        monkeyMaxPopulationLabel.text = "Máxima: " + monkeyStats.maxPopulation;
        monkeyHuntLabel.text = "Caça: " + monkeyStats.huntDeath;
        monkeyHungerLabel.text = "Fome: " + monkeyStats.hungerDeath;
        monkeyThirstLabel.text = "Sede: " + monkeyStats.thirstDeath;
        monkeyFireLabel.text = "Incêndio: " + monkeyStats.fireDeath;

        hawkInitialPopulationLabel.text = "Inicial: " + hawkStats.initialPopulation;
        hawkMaxPopulationLabel.text = "Máxima: " + hawkStats.maxPopulation;
        hawkHungerLabel.text = "Fome: " + hawkStats.hungerDeath;
        hawkThirstLabel.text = "Sede: " + hawkStats.thirstDeath;
        hawkFireLabel.text = "Incêndio: " + hawkStats.fireDeath;
    }

    private void SetTimeline()
    {
        timeLabel.text = "Tempo: " + string.Format("{0:0.0}", totalTime) + "s";

        foreach (var gEvent in events)
        {
            timeline.value = gEvent.timeStamp / totalTime;

            switch (gEvent.eventType)
            {
                case EventType.MonkeyExtinction:
                    Instantiate(monkeyIcon, new Vector3(timeline.handleRect.position.x, timeline.handleRect.position.y + 100, 0), Quaternion.identity, canvas.transform);
                    break;
                case EventType.HawkExctinction:
                    Instantiate(hawkIcon, new Vector3(timeline.handleRect.position.x, timeline.handleRect.position.y - 100, 0), Quaternion.identity, canvas.transform);
                    break;
                case EventType.Fire:
                    Instantiate(fireIcon, new Vector3(timeline.handleRect.position.x, timeline.handleRect.position.y + 100, 0), Quaternion.identity, canvas.transform);
                    break;
            }
        }
    }

}

public struct GeneralEvent
{
    public float timeStamp;
    public EventType eventType;

    public GeneralEvent(EventType type, float time)
    {
        timeStamp = time;
        eventType = type;
    }
}

public enum EventType 
{
    MonkeyExtinction,
    HawkExctinction,
    SimulationEnd,
    Fire
}