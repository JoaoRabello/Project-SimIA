using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyHawk : Hawk
{
    [SerializeField] private Hawk matureHawk;

    private HawkDNA babyDNA = new HawkDNA();
    private float timeToMature = 5;

    public new void Initialize(HawkDNA dna)
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
                        NormalFly();
                    }
                }
                else
                {
                    Herbivore herbivore = food.GetComponent<Herbivore>();
                    if (canMove && !herbivore.IsAtTree)
                    {
                        Hunt(herbivore);
                    }
                    else
                    {
                        NormalFly();
                    }
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
                    {
                        Flyby(water);
                    }
                    else
                    {
                        NormalFly();
                    }
                }
                break;
        }
    }

    private IEnumerator Maturation()
    {
        yield return new WaitForSeconds(timeToMature);

        var hawk = Instantiate(matureHawk, transform.position, Quaternion.identity);
        hawk.Initialize(babyDNA);

        Destroy(gameObject);
    }
}
