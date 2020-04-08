using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Population : MonoBehaviour
{
    //Evolution fields
    public enum Evolution { Random, SingleInheritance }
    [SerializeField] private Evolution evolutionStyle;
    [SerializeField] private float secondsPerGeneration;
    [SerializeField] private float mutationAmount;

    //Simulation control fields
    [SerializeField] private bool run;
    [SerializeField] private bool stop;
    [SerializeField] private bool step;
    private bool running;

    private Individual[] population;

    private void Awake()
    {
        population = GetComponentsInChildren<Individual>();
    }

    private void Update()
    {
        GetUserInput();
    }

    private void GetUserInput()
    {
        if (step)
        {
            CancelInvoke("Tick");
            Tick();

            running = false;
            run     = false;
            stop    = true;
            step    = false;
        }
        if(run && !running)
        {
            InvokeRepeating("Tick", secondsPerGeneration, secondsPerGeneration);
            running = true;
            stop = false;
        }
        else
        {
            if(stop && running)
            {
                CancelInvoke("Tick");
                running = false;
                run = false;
            }
            else
            {
                if(!run && running)
                {
                    run = true;
                }
                else
                {
                    if(!stop && !running)
                    {
                        stop = true;
                    }
                }
            }
        }
    }

    private void Tick()
    {
        foreach (Individual individual in population)
        {
            EvolveIndividual(individual);
        }
    }

    private void EvolveIndividual(Individual individual)
    {
        switch (evolutionStyle)
        {
            case Evolution.Random:
                individual.RandomizeAllTraits();
                break;
            case Evolution.SingleInheritance:
                individual.SingleInherit(mutationAmount);
                break;
        }
    }
}
