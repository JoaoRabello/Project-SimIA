﻿using System.Collections;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class Herbivore : Animal
{
    [SerializeField] protected int fatAmount;

    [Header("Gravity Attributes")]
    [SerializeField] protected LayerMask groundLayerMask;
    [SerializeField] protected float gravityScale;
    protected float gravity = 9.81f;

    private bool isTouchingGround = true;
    private bool isJumping = false;

    public int FatAmount { get => fatAmount; }
    public bool IsAtTree { get; private set; }

    protected new void Update()
    {
        base.Update();
        
        UseGravity();
    }

    private void UseGravity()
    {
        Vector3 gravityDirection = Vector3.down;
        
        if(!Physics.Raycast(transform.position, gravityDirection, 1, groundLayerMask) && !isTouchingGround && !isJumping)
        {
            myRigidbody.AddForce(Vector3.down * gravity * gravityScale * Time.deltaTime * 10, ForceMode.Force);
        }
    }

    public override void MoveToThis(Vector3 destiny)
    {
        Vector3 direction = new Vector3(destiny.x - transform.position.x, 0, destiny.z - transform.position.z);
        Vector3 moveDirection = direction.normalized;
        
        Color color = Physics.Raycast(transform.position, direction, groundLayerMask)? Color.white : Color.red;
        Debug.DrawRay(transform.position, direction * 50, color);
        
        myRigidbody.velocity = moveDirection * actualSpeed;
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

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Ground"))
        {
            isTouchingGround = true;
        }
    }

    private void OnCollisionExit(Collision col)
    {
        if (col.gameObject.CompareTag("Ground"))
        {
            isTouchingGround = false;
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        GameObject gObject = col.gameObject;

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

    private void ClimbTree(Tree tree)
    {
        IsAtTree = true;

        transform.position = new Vector3(tree.GetTreeTopPosition().x + Random.Range(-1,1), tree.GetTreeTopPosition().y, tree.GetTreeTopPosition().z + Random.Range(-1, 1));
        StartCoroutine(EatTreeFruits(tree));
    }

    private void DescendTree(Tree tree)
    {
        IsAtTree = false;
        transform.position = tree.GetTreeBotPosition();
        this.tree = null;
        treeOnSight = false;
    }

    IEnumerator EatTreeFruits(Tree tree)
    {
        while (tree.NumberOfFruits > 0)
        {
            if (nutritionManager.IsHungry())
            {
                EatFood(tree.GetFruit());
            }
            yield return null;
        }
        DescendTree(tree);
    }

    public void Die()
    {
        AnimalStatistics.Instance.RemoveAnimal(GetComponent<Monkey>());
    }
}