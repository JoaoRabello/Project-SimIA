using UnityEngine;

public class Herbivore : Animal
{
    protected new void Update()
    {
        base.Update();
    }

    public override void MoveToThis(Vector3 destiny)
    {
        Vector3 direction = new Vector3(destiny.x - transform.position.x, 0, destiny.z - transform.position.z).normalized;
        myRigidbody.velocity = direction * speed;
    }

    protected override bool FoodOnSight()
    {
        throw new System.NotImplementedException();
    }

    protected override void EatFood(Fruit fruit)
    {
        nutritionManager.Eat(fruit.Nutrition);
        foodOnSight = false;
    }

    private void OnTriggerEnter(Collider col)
    {
        GameObject gObject = col.gameObject;

        if (gObject.CompareTag("Food"))
        {
            if (nutritionManager.IsHungry())
            {
                EatFood(gObject.GetComponent<Fruit>());
                Destroy(gObject);
            }
        }
    }
}
