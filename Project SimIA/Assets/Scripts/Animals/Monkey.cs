using System.Collections;
using UnityEngine;

public class Monkey : Herbivore
{
    protected new void Update()
    {
        base.Update();

        if (!IsAtTree)
        {
            switch (state)
            {
                case State.Nourished:
                    if (canMove)
                    {
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
                        if (canMove)
                            MoveToThis(food.transform.position);
                    }
                    break;
                case State.Thirsty:
                    if (river == null)
                    {
                        if (!riverOnSight)
                            SearchRiver();
                    }
                    else
                    {
                        if (canMove)
                            MoveToThis(river.transform.position);
                    }
                    break;
            }
        }
        else
        {
            myRigidbody.velocity = Vector3.zero;
        }
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(transform.position, foodViewRange);
    //    Gizmos.color = Color.blue;
    //    Gizmos.DrawWireSphere(transform.position, riverViewRange);
    //}
    
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