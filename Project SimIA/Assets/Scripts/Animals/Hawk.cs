using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hawk : Carnivore
{
    private int flybyCount;

    protected new void Update()
    {
        base.Update();
        switch (state)
        {
            case State.Nourished:
                NormalFly();
                break;
            case State.Hungry:
                if (food == null)
                {
                    if (!foodOnSight)
                    {
                        SearchFood();
                    }
                }
                else
                {
                    print(food);
                    if (canMove && !food.GetComponent<Herbivore>().IsAtTree)
                    {
                        Flyby();
                    }
                    else
                    {
                        NormalFly();
                    }
                }
                break;
            //case State.Thirsty:
            //    if (river == null)
            //    {
            //        if (!riverOnSight)
            //            SearchRiver();
            //    }
            //    else
            //    {
            //        if (canMove)
            //            MoveToThis(river.transform.position);
            //    }
            //    break;

        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, foodViewRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, riverViewRange);
    }

    private void NormalFly()
    {
        if (canMove)
        {
            if (!isRandomWalking)
            {
                randomDestiny = GetRandomDestiny();
                StartCoroutine(StartRandomFly(randomDestiny));
            }
            else
            {
                if (canRandomWalk)
                {
                    MoveToThis(randomDestiny);
                }
            }
        }
    }

    private void Flyby()
    {
        if(flybyCount == 0)
        {
            speed *= 3;
            flybyCount++;
        }
        MoveToThis(food.transform.position);
    }

    private Vector3 GetRandomDestiny()
    {
        float randomY = Random.Range(transform.position.y - 5, transform.position.y + 5);
        if(randomY > 6)
        {
            return new Vector3(Random.Range(transform.position.x - 10, transform.position.x + 10),
                            randomY,
                            Random.Range(transform.position.z - 10, transform.position.z + 10));
        }
        else
        {
            return new Vector3(Random.Range(transform.position.x - 10, transform.position.x + 10),
                            transform.position.y,
                            Random.Range(transform.position.z - 10, transform.position.z + 10));
        }
    }

    private IEnumerator StartRandomFly(Vector3 destiny)
    {
        isRandomWalking = true;

        yield return new WaitUntil(() => IsAtDestiny(randomDestiny));

        isRandomWalking = false;
    }
}
