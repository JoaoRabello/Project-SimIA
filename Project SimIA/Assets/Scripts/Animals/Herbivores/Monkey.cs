﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monkey : Herbivore
{
    protected MonkeyDNA dna;

    public void Initialize(MonkeyDNA dna)
    {
        this.dna = dna;

        speed = dna.speed;
        foodViewRange = dna.foodViewRange;
        waterViewRange = dna.waterViewRange;

        sex = dna.sex;
    }

    protected new void Update()
    {
        base.Update();

        if (!IsAtTree)
        {
            switch (state)
            {
                case State.Danger:
                    if (!isRunningFromDanger)
                    {
                        RunFrom();
                    }
                    break;
                case State.Nourished:
                    NormalWalk();
                    break;
                case State.Hungry:
                    if (tree == null)
                    {
                        if (!treeOnSight)
                        {
                            SearchTree();
                            NormalWalk();
                        }
                    }
                    else
                    {
                        if (canMove)
                            MoveToThis(tree.transform.position);
                    }
                    break;
                case State.Thirsty:
                    if (water == null)
                    {
                        if (!riverOnSight)
                            SearchRiver();
                    }
                    else
                    {
                        if (canMove)
                            MoveToThis(water.transform.position);
                    }
                    break;
                case State.Horny:
                    if(mate == null)
                    {
                        if (!mateOnSight)
                        {
                            SearchMate();
                            NormalWalk();
                        }
                    }
                    else
                    {
                        if (canMove)
                        {
                            MoveToThis(mate.transform.position);
                            if (IsAtDestiny(mate.transform.position) && !mate.GetComponentInParent<Herbivore>().IsAtTree && !IsAtTree)
                            {
                                Reproduce();
                            }
                        }
                    }
                    break;
            }
        }
        else
        {
            myRigidbody.velocity = Vector3.zero;
        }
    }

    protected void NormalWalk()
    {
        if (canMove)
        {
            if (!isRandomWalking)
            {
                randomDestiny = GetRandomDestiny();
                StartCoroutine(StartRandomWalk(randomDestiny));
            }
            else
            {
                if (canRandomWalk)
                {
                    MoveToThis(randomDestiny);
                }
            }
        }
    }

    protected void RunFrom()
    {
        StopCoroutine(StartRunAway(dangerRunAwayDestiny));
        
        dangerRunAwayDestiny = (transform.position - FindCentroid(danger)).normalized;

        StartCoroutine(StartRunAway(dangerRunAwayDestiny + transform.position));
    }

    private Vector3 FindCentroid(Collider[] targets)
    {

        Vector3 centroid;
        Vector3 minPoint = targets[0].transform.position;
        Vector3 maxPoint = targets[0].transform.position;

        for (int i = 1; i < targets.Length; i++)
        {
            Vector3 pos = targets[i].transform.position;
            if (pos.x < minPoint.x)
                minPoint.x = pos.x;
            if (pos.x > maxPoint.x)
                maxPoint.x = pos.x;
            if (pos.y < minPoint.y)
                minPoint.y = pos.y;
            if (pos.y > maxPoint.y)
                maxPoint.y = pos.y;
            if (pos.z < minPoint.z)
                minPoint.z = pos.z;
            if (pos.z > maxPoint.z)
                maxPoint.z = pos.z;
        }

        centroid = minPoint + 0.5f * (maxPoint - minPoint);
        return new Vector3(centroid.x, 0, centroid.z);
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(transform.position, foodViewRange);
    //    Gizmos.color = Color.blue;
    //    Gizmos.DrawWireSphere(transform.position, riverViewRange);
    //}

    private Vector3 GetRandomDestiny()
    {
        return new Vector3(Random.Range(transform.position.x - 15, transform.position.x + 15),
                            0,
                            Random.Range(transform.position.z - 15, transform.position.z + 15));
    }

    private IEnumerator StartRandomWalk(Vector3 destiny)
    {
        isRandomWalking = true;

        yield return new WaitUntil(() => IsAtDestiny(randomDestiny));

        isRandomWalking = false;
    }

    private IEnumerator StartRunAway(Vector3 destiny)
    {
        isRunningFromDanger = true;
        while (!IsAtDestiny(destiny))
        {
            MoveToThis(destiny);
            yield return null;
        }
        isRunningFromDanger = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject gObject = other.gameObject;

        if (gObject.CompareTag("Tree"))
        {
            Tree tree = gObject.GetComponent<Tree>();

            if (nutritionManager.IsHungry() && tree.Equals(this.tree))
            {
                ClimbTree(tree);
            }
        }
        else
        {
            if (gObject.CompareTag("River"))
            {
                if (nutritionManager.IsThirsty())
                {
                    DrinkWater();
                }
            }
        }
        
    }

    private void Reproduce()
    {
        AnimalFactory.CreateBabyMonkey(dna, mate.transform);
       
        mate = null;
        mateOnSight = false;
        reproductionUrge = 0;
        state = State.Nourished;
    }
}