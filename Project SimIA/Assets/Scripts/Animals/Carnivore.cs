using UnityEngine;

public class Carnivore : Animal
{
    public override void MoveToThis(Vector3 destiny)
    {
        Vector3 direction = (destiny - transform.position).normalized;

        transform.forward = Vector3.Lerp(transform.forward, direction, 5 * Time.deltaTime);
        
        myRigidbody.velocity = direction * actualSpeed;
    }

    protected override void EatFood(Herbivore herbivore)
    {
        nutritionManager.Eat(herbivore.FatAmount);
        herbivore.Die();
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
        GameObject gObject = col.gameObject;

        if (gObject.CompareTag("Herbivore"))
        {
            Herbivore herbivore = gObject.GetComponent<Herbivore>();
            if (nutritionManager.IsHungry())
            {
                EatFood(herbivore);
                Destroy(gObject);
            }
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("River"))
        {
            if (nutritionManager.IsThirsty())
            {
                DrinkWater();
            }
        }
    }
}