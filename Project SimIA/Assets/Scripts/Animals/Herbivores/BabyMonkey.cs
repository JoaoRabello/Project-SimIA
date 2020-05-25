using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyMonkey : Monkey
{
    public Monkey matureMonkey;

    private MonkeyDNA babyDNA = new MonkeyDNA();
    private float timeToMature = 5;

    public new void Initialize(MonkeyDNA dna)
    {
        babyDNA = dna;

        speed = dna.speed;
        foodViewRange = dna.foodViewRange;
        waterViewRange = dna.waterViewRange;
        sex = dna.sex;
    }

    private void Start()
    {
        StartCoroutine(Maturation());
    }

    private new void Update()
    {
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
    }

    private IEnumerator Maturation()
    {
        yield return new WaitForSeconds(timeToMature);

        var monkey = Instantiate(matureMonkey, transform.position, Quaternion.identity);
        monkey.Initialize(babyDNA);

        Destroy(gameObject);
    }
}
