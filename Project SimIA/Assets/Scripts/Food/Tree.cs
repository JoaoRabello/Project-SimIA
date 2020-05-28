using TMPro;
using UnityEngine;

public class Tree : MonoBehaviour
{
    public int fruitCapacity;
    public int NumberOfFruits { get; private set; }

    private float timer = 0;
    [SerializeField] private float timeToFruitSpawn = 60f;

    [SerializeField] private TextMeshProUGUI fruitQuantityText;
    [SerializeField] private Fruit fruitType;
    [SerializeField] private Transform treeTop;
    [SerializeField] private Transform treeBot;

    private float heat = 0;
    private Fire fire;
    public float heatMax;
    private bool isBurning = false;

    private float fireTimer = 0;
    [SerializeField] private float timeToBurn = 10f;

    private void Start()
    {
        NumberOfFruits = fruitCapacity;
    }

    private void Update()
    {
        fruitQuantityText.text = NumberOfFruits.ToString();

        if(NumberOfFruits < fruitCapacity)
        {
            if(timer >= timeToFruitSpawn)
            {
                NumberOfFruits++;
                timer = 0;
            }
            else
            {
                timer += Time.deltaTime;
            }
        }
        else
        {
            timer = 0;
        }

        if (isBurning)
        {
            Burn();
        }
    }

    public Fruit GetFruit()
    {
        if (NumberOfFruits > 0)
        {
            NumberOfFruits--;
            return fruitType;
        }
        else
            return null;
    }

    public Vector3 GetTreeTopPosition()
    {
        return treeTop.position;
    }

    public Vector3 GetTreeBotPosition()
    {
        return treeBot.position;
    }

    public void IncreaseHeat(float heat, Fire fire)
    {
        if(this.heat < heatMax)
        {
            this.heat += heat;
        }
        else
        {
            if(!isBurning)
            {
                this.fire = fire.AddSpot(treeTop.position);
                isBurning = true;
            }
        }
    }

    private void Burn()
    {
        if(fireTimer > timeToBurn)
        {
            Destroy(transform.parent.gameObject);
            Destroy(fire.gameObject);
        }
        else
        {
            fireTimer += Time.deltaTime;
        }
    }
}
