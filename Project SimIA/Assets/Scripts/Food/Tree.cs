using UnityEngine;

public class Tree : MonoBehaviour
{
    public int fruitCapacity;
    public int NumberOfFruits { get; private set; }
    [SerializeField] private Fruit fruitType;
    [SerializeField] private Transform treeTop;
    [SerializeField] private Transform treeBot;

    private void Start()
    {
        NumberOfFruits = fruitCapacity;
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
