using System.Collections;
using UnityEngine;

public class Monkey : Herbivore
{
    protected MonkeyDNA dna;

    public void Initialize(MonkeyDNA dna)
    {
        this.dna = dna;

        speed = dna.speed;
        foodViewRange = dna.foodViewRange;
        waterViewRange = dna.waterViewRange;

        sex = dna.sex;
        print(sex);
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
                case State.Horny:
                    if(mate == null)
                    {
                        if (!mateOnSight)
                        {
                            SearchMate();
                            NormalWalk();
                        }
                    }
                    else
                    {
                        if (canMove)
                        {
                            MoveToThis(mate.transform.position);
                            if (IsAtDestiny(mate.transform.position) && !mate.GetComponentInParent<Herbivore>().IsAtTree)
                            {
                                Reproduce();
                            }
                        }
                    }
                    break;
            }
        }
        else
        {
            myRigidbody.velocity = Vector3.zero;
        }
    }

    protected void NormalWalk()
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

    private void OnTriggerEnter(Collider other)
    {
        GameObject gObject = other.gameObject;

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

    private void Reproduce()
    {
        for (int i = 0; i < 3; i++)
        {
            AnimalFactory.CreateBabyMonkey(dna, mate.transform);
        }
        mate = null;
        mateOnSight = false;
        reproductionUrge = 0;
        state = State.Nourished;
    }
}