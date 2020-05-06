using System.Collections;
using UnityEngine;

public class Monkey : Herbivore
{
    public void Initialize(DNA dna)
    {
        speed = dna.speed;
        foodViewRange = dna.foodViewRange;
        waterViewRange = dna.waterViewRange;
    }
    
    protected new void Update()
    {
        base.Update();

        if (!IsAtTree)
        {
            switch (state)
            {
                case State.Nourished:
                    NormalWalk();
                    break;
                case State.Hungry:
                    if (tree == null)
                    {
                        if (!treeOnSight)
                        {
                            SearchTree();
                            NormalWalk();
                        }
                    }
                    else
                    {
                        if (canMove)
                            MoveToThis(tree.transform.position);
                    }
                    break;
                case State.Thirsty:
                    if (water == null)
                    {
                        if (!riverOnSight)
                            SearchRiver();
                    }
                    else
                    {
                        if (canMove)
                            MoveToThis(water.transform.position);
                    }
                    break;
            }
        }
        else
        {
            myRigidbody.velocity = Vector3.zero;
        }
    }

    private void NormalWalk()
    {
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
        return new Vector3(Random.Range(transform.position.x - 15, transform.position.x + 15),
                            0,
                            Random.Range(transform.position.z - 15, transform.position.z + 15));
    }

    private IEnumerator StartRandomWalk(Vector3 destiny)
    {
        isRandomWalking = true;

        yield return new WaitUntil(() => IsAtDestiny(randomDestiny));

        isRandomWalking = false;
    }
}