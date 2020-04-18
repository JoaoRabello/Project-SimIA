using UnityEngine;

public class Carnivore : Animal
{
    public override void MoveToThis(Vector3 destiny)
    {
        Vector3 direction = (destiny - transform.position).normalized;
        transform.LookAt(destiny);

        myRigidbody.velocity = direction * speed;
    }

    protected override void EatFood(Herbivore herbivore)
    {
        nutritionManager.Eat(herbivore.FatAmount);
        food = null;
        foodOnSight = false;
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
        //else
        //{
        //    if (gObject.CompareTag("River"))
        //    {
        //        Debug.Log("At River");
        //        if (nutritionManager.IsThirsty())
        //        {
        //            Debug.Log("Trying to Drink");
        //            DrinkWater();
        //        }
        //    }
        //}
    }
}