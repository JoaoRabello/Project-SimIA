using System.Collections;
using UnityEngine;

public class Monkey : Herbivore
{
    protected new void Update()
    {
        base.Update();
        switch (state)
        {
            case State.Fed:
                if (!isRandomWalking)
                {
                    randomDestiny = GetRandomDestiny();
                    StartCoroutine(StartRandomWalk(randomDestiny));
                }
                else
                {
                    if (canRandomWalk)
                    {
                        MoveToThis(randomDestiny);
                    }
                }
                break;
            case State.Hungry:
                if (food == null)
                {
                    if (!foodOnSight)
                        SearchFood();
                }
                else
                {
                    MoveToThis(food.transform.position);
                }
                break;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, viewRange);
    }
    
    private Vector3 GetRandomDestiny()
    {
        return new Vector3(Random.Range(transform.position.x - 5, transform.position.x + 5), 
                            0,
                            Random.Range(transform.position.z - 5, transform.position.z + 5));
    }

    private IEnumerator StartRandomWalk(Vector3 destiny)
    {
        isRandomWalking = true;

        yield return new WaitUntil(() => IsAtDestiny(randomDestiny));

        isRandomWalking = false;
    }
}
