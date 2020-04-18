using System;
using System.Collections;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class Herbivore : Animal
{
    [SerializeField] protected int fatAmount;
    public int FatAmount { get => fatAmount; }
    public bool IsAtTree { get; private set; }

    protected new void Update()
    {
        base.Update();
    }

    public override void MoveToThis(Vector3 destiny)
    {
        Vector3 direction = new Vector3(destiny.x - transform.position.x, 0, destiny.z - transform.position.z).normalized;
        myRigidbody.velocity = direction * speed;
    }

    protected override void EatFood(Fruit fruit)
    {
        nutritionManager.Eat(fruit.Nutrition);
        food = null;
        foodOnSight = false;
        state = State.Nourished;
    }

    protected override void DrinkWater()
    {
        nutritionManager.Drink(30);
        state = State.Nourished;
    }

    private void OnTriggerEnter(Collider col)
    {
        GameObject gObject = col.gameObject;

        if (gObject.CompareTag("Tree"))
        {
            if (nutritionManager.IsHungry())
            {
                //EatFood(gObject.GetComponent<Fruit>());
                //Destroy(gObject);
                ClimbTree(gObject.GetComponent<Tree>());
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

    private void ClimbTree(Tree tree)
    {
        print("climb tree");
        transform.position = tree.GetTreeTopPosition();
        StartCoroutine(EatTreeFruits(tree));
    }

    IEnumerator EatTreeFruits(Tree tree)
    {
        IsAtTree = true;
        while (tree.NumberOfFruits > 0)
        {
            if (nutritionManager.IsHungry())
            {
                print("come fruta no while");
                EatFood(tree.GetFruit());
            }
            yield return null;
        }
        print("acabou o while");
        IsAtTree = false;
    }
}