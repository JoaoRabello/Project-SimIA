using UnityEngine;

public class Fruit : MonoBehaviour
{
    [SerializeField] private int _nutrition;
    public int Nutrition { get => _nutrition;}
}
