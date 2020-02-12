using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SurvivalControllers;

public class Monkey : MonoBehaviour, IMovable
{
    private HungerController hungerController;

    [Header("Food Searching Attributes")]
    [SerializeField] private LayerMask foodMask;
    [SerializeField] private float viewRange;
    private bool foundFood = false;
    private GameObject food;

    private Rigidbody myRigidbody;
    [SerializeField] private float speed;

    public Text text;

    private void Awake()
    {
        hungerController = GetComponent<HungerController>();
        myRigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (hungerController.IsHungry())
        {
            text.text = "Faminto";

            if (!foundFood)
                SeachFood();
            else
                MoveToThis(food.transform.position);
        }
        else
        {
            text.text = "Saciado";
        }

    }

    private void SeachFood()
    {
        Collider[] foodNextToThis = Physics.OverlapSphere(transform.position, viewRange, foodMask);
        int numberOfFoods = foodNextToThis.Length;

        if (numberOfFoods == 0)
        {
            foundFood = false;
            print("Can't find food, gonna die!");
        }
        else
        {
            food = foodNextToThis[0].gameObject;
            foundFood = true;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, viewRange);
    }

    public void MoveToThis(Vector3 destiny)
    {
        Vector3 direction = new Vector3(destiny.x - transform.position.x, 0, destiny.z - transform.position.z).normalized;
        myRigidbody.velocity = direction * speed;
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Food"))
        {
            hungerController.Eat();
            foundFood = false;
            Destroy(col.gameObject);
        }
    }
}
