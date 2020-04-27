using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SurvivalManagers;
using System;

public abstract class Animal : MonoBehaviour, IMovable
{
    //[Flags]
    protected enum State { Nourished, Hungry, Thirsty, Horny}
    protected State state;

    protected NutritionManager nutritionManager;
    protected Rigidbody myRigidbody;

    [Header("Resources Searching Attributes")]
    public LayerMask foodMask;
    public LayerMask treeMask;
    public LayerMask riverMask;
    public float foodViewRange;
    public float riverViewRange;
    protected bool foodOnSight = false;
    protected bool treeOnSight = false;
    protected bool riverOnSight = false;
    protected Tree tree;
    protected GameObject food;
    protected GameObject river;

    public float speed;
    protected float actualSpeed;
    protected Vector3 randomDestiny;
    protected bool canMove = true;
    protected bool canRandomWalk = true;
    protected bool isRandomWalking = false;
    protected bool randomStop = false;

    public Text hungerText;
    public Text thirstText;

    private void Awake()
    {
        nutritionManager = GetComponent<NutritionManager>();
        myRigidbody = GetComponent<Rigidbody>();
        actualSpeed = speed;
    }

    protected void Update()
    {
        StateCheck();
    }

    private void StateCheck()
    {
        if (nutritionManager.IsThirsty())
        {
            thirstText.text = "Com Sede";
            state = State.Thirsty;
        }
        else
        {
            if (nutritionManager.IsHungry())
            {
                hungerText.text = "Com fome";
                state = State.Hungry;
            }
            else
            {
                hungerText.text = "Alimentado";
                thirstText.text = "Hidratado";
                state = State.Nourished;
            }
        }
    }

    #region Movement
    public abstract void MoveToThis(Vector3 destiny);

    protected bool IsAtDestiny(Vector3 destiny)
    {
        float minDif = 0.2f;

        float xDif = Mathf.Abs(destiny.x - transform.position.x);
        float zDif = Mathf.Abs(destiny.z - transform.position.z);

        if (xDif <= minDif && zDif <= minDif)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    #endregion

    #region Feeding
    protected void SearchFood()
    {
        Collider[] foodNextToThis = Physics.OverlapSphere(transform.position, foodViewRange, foodMask);
        int numberOfFoods = foodNextToThis.Length;

        if(numberOfFoods == 0)
        {
            foodOnSight = false;
        }
        else
        {
            float smallerDistance = 10000;
            foreach (var food in foodNextToThis)
            {
                float distance = Vector3.Distance(transform.position, food.transform.position);
                if (distance < smallerDistance)
                {
                    smallerDistance = distance;
                    this.food = food.gameObject;
                }
                foodOnSight = true;
            }
        }
    }

    protected void SearchTree()
    {
        Collider[] treeNextToThis = Physics.OverlapSphere(transform.position, foodViewRange, treeMask);
        int numberOfTrees = treeNextToThis.Length;

        if (numberOfTrees == 0)
        {
            treeOnSight = false;
        }
        else
        {
            float smallerDistance = 10000;
            foreach (var treeObject in treeNextToThis)
            {
                float distance = Vector3.Distance(transform.position, treeObject.transform.position);
                Tree tree = treeObject.GetComponent<Tree>();
                if (distance < smallerDistance && tree.NumberOfFruits > 0)
                {
                    smallerDistance = distance;
                    this.tree = tree;
                    treeOnSight = true;
                }
            }
        }
    }

    protected void SearchRiver()
    {
        Collider[] riverNextToThis = Physics.OverlapSphere(transform.position, riverViewRange, riverMask);
        bool isRiverNext = riverNextToThis.Length > 0;

        if (!isRiverNext)
        {
            riverOnSight = false;
        }
        else
        {
            if (riverNextToThis[0] != null)
            {
                river = riverNextToThis[0].gameObject;
                riverOnSight = true;
            }
        }
    }

    protected virtual void EatFood(Fruit fruit) { }
    protected virtual void EatFood(Herbivore herbivore) { }
    protected virtual void DrinkWater() { }
    #endregion
}