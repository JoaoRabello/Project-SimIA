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
}
