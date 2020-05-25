using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SurvivalManagers;
using System;

public enum BiologicalSex 
{ 
    Male, Female 
}

public abstract class Animal : MonoBehaviour, IMovable
{
    protected enum State { Nourished, Hungry, Thirsty, Horny, Danger }

    protected State state;
    protected BiologicalSex sex;

    protected NutritionManager nutritionManager;
    protected Rigidbody myRigidbody;

    [Header("Resources Searching Attributes")]
    public LayerMask foodMask;
    public LayerMask dangerMask;
    public LayerMask mateMask;
    public LayerMask treeMask;
    public LayerMask waterMask;
    public float foodViewRange;
    public float dangerViewRange;
    public float mateViewRange;
    public float waterViewRange;
    protected bool foodOnSight = false;
    protected bool mateOnSight = false;
    protected bool treeOnSight = false;
    protected bool riverOnSight = false;
    protected Tree tree;
    protected GameObject food;
    protected GameObject water;

    public float speed;
    protected float actualSpeed;
    protected Vector3 randomDestiny;
    protected Vector3 dangerRunAwayDestiny;
    protected bool canMove = true;
    protected bool canRandomWalk = true;
    protected bool isRandomWalking = false;
    protected bool randomStop = false;
    protected Collider[] danger;
    protected bool isRunningFromDanger = false;

    protected bool isHorny = true;
    protected int reproductionUrge; 
    [SerializeField] protected int maxReproductionUrge; 
    protected float reproductionUrgeRate;
    protected GameObject mate;
    public Collider sexualOrgan;

    private void Awake()
    {
        nutritionManager = GetComponent<NutritionManager>();
        myRigidbody = GetComponent<Rigidbody>();
        actualSpeed = speed;
    }

    protected void Update()
    {
        StateCheck();

        if (isHorny)
            StartCoroutine(ReproductionTimer());
    }

    private void StateCheck()
    {
        danger = Physics.OverlapSphere(transform.position, dangerViewRange, dangerMask);
        int dangerCount = danger.Length;
        if(dangerCount > 0)
        {
            state = State.Danger;
        }
        else
        {
            if (nutritionManager.IsThirsty())
            {
                state = State.Thirsty;
            }
            else
            {
                if (nutritionManager.IsHungry())
                {
                    state = State.Hungry;
                }
                else
                {
                    if (IsHorny())
                    {
                        state = State.Horny;
                    }
                    else
                    {
                        state = State.Nourished;
                    }
                }
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
        Collider[] riverNextToThis = Physics.OverlapSphere(transform.position, waterViewRange, waterMask);
        bool isRiverNext = riverNextToThis.Length > 0;

        if (!isRiverNext)
        {
            riverOnSight = false;
        }
        else
        {
            if (riverNextToThis[0] != null)
            {
                water = riverNextToThis[0].gameObject;
                riverOnSight = true;
            }
        }
    }

    protected void SearchMate()
    {
        if (sex == BiologicalSex.Male)
        {
            sexualOrgan.enabled = false;
            Collider[] possibleMates = Physics.OverlapSphere(transform.position, mateViewRange, mateMask);
            List<Collider> females = new List<Collider>();

            for (int i = 0; i < possibleMates.Length; i++)
            {
                if (possibleMates[i].gameObject.GetComponentInParent<Animal>().sex == BiologicalSex.Female)
                {
                    females.Add(possibleMates[i]);
                }
            }
            
            int matesCount = females. Count;

            if (matesCount == 0)
            {
                mateOnSight = false;
            }
            else
            {
                float smallerDistance = 10000;
                foreach (var female in females)
                {
                    float distance = Vector3.Distance(transform.position, female.transform.position);
                    if (distance < smallerDistance)
                    {
                        smallerDistance = distance;
                        mate = female.gameObject;
                    }
                    mateOnSight = true;
                }
                Request(mate.GetComponentInParent<Animal>());
            }
            sexualOrgan.enabled = true;
        }
    }

    public void Request(Animal mate)
    {
        if (mate.IsHorny())
        {
            mate.mate = this.gameObject;
        }
        else
        {
            this.mate = null;
            mateOnSight = false;
        }
    }

    private IEnumerator ReproductionTimer()
    {
        isHorny = false;
        yield return new WaitForSeconds(reproductionUrgeRate);
        reproductionUrge++;
        isHorny = true;
    }

    protected bool IsHorny()
    {
        return reproductionUrge > maxReproductionUrge * 0.8f;
    }

    protected virtual void Reproduce(MonkeyDNA dna) { }
    protected virtual void Reproduce(HawkDNA dna) { }
    protected virtual void EatFood(Fruit fruit) { }
    protected virtual void EatFood(Herbivore herbivore) { }
    protected virtual void DrinkWater() { }
    protected virtual void RunFrom(Transform danger) { }
    #endregion
}