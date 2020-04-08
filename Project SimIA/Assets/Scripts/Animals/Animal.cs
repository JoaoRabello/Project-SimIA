using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SurvivalManagers;

public abstract class Animal : MonoBehaviour, IMovable
{
    protected enum State { Fed, Hungry }
    protected State state;
    protected NutritionManager nutritionManager;
    protected Rigidbody myRigidbody;

    [Header("Food Searching Attributes")]
    public LayerMask foodMask;
    public float viewRange;
    protected bool foodOnSight = false;
    protected GameObject food;

    public float speed;
    protected Vector3 randomDestiny;
    protected bool canRandomWalk = true;
    protected bool isRandomWalking = false;
    protected bool randomStop = false;

    public Text text;

    private void Awake()
    {
        nutritionManager = GetComponent<NutritionManager>();
        myRigidbody = GetComponent<Rigidbody>();
    }

    protected void Update()
    {
        StateCheck();
    }

    private void StateCheck()
    {
        if (!nutritionManager.IsHungry())
        {
            text.text = "Alimentado";
            state = State.Fed;
        }
        else
        {
            text.text = "Com fome";
            state = State.Hungry;
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
        Collider[] foodNextToThis = Physics.OverlapSphere(transform.position, viewRange, foodMask);
        int numberOfFoods = foodNextToThis.Length;

        if (numberOfFoods == 0)
        {
            foodOnSight = false;
        }
        else
        {
            if (foodNextToThis[0] != null)
            {
                food = foodNextToThis[0].gameObject;
                foodOnSight = true;
            }
        }
    }

    protected abstract bool FoodOnSight();
    protected virtual void EatFood(Fruit fruit) { }
    protected virtual void EatFood(Herbivore herbivore) { }
    #endregion
}
