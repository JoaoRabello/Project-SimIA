using UnityEngine;

public class Carnivore : Animal
{
    protected Herbivore prey;
    protected bool isHunting = false;

    public override void MoveToThis(Vector3 destiny)
    {
        Vector3 direction = (destiny - transform.position).normalized;

        transform.forward = Vector3.Lerp(transform.forward, direction, 5 * Time.deltaTime);
        
        myRigidbody.velocity = direction * actualSpeed;
    }

    protected virtual void Hunt(Herbivore preyToHunt) { }

    protected void SetPrey(Herbivore preyToHunt)
    {
        if (prey == null)
        {
            prey = preyToHunt;
            prey.SetHunter(this);
        }
    }

    public void StopHunting()
    {
        prey = null;
        isHunting = false;
        food = null;
        foodOnSight = false;
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