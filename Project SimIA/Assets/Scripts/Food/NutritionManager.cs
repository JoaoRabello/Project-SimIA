using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace SurvivalManagers
{
    public class NutritionManager : MonoBehaviour
    {
        [Header("Attributes")]
        [SerializeField] private int maxNutrition = 10;
        [SerializeField] private float hungerIncreaseRate = 2f;
        private float actualHunger;
        private bool isHungry = true;
        [SerializeField] private int maxHydration = 10;
        [SerializeField] private float thirstIncreaseRate = 2f;
        private float actualThirst;
        private bool isThirsty = true;

        private void Start()
        {
            actualHunger = maxNutrition;
            actualThirst = maxHydration;
        }

        private void Update()
        {
            if (isThirsty)
                StartCoroutine(ThirstTimer());
            if (isHungry)
                StartCoroutine(HungerTimer());

            if (actualHunger <= 0)
            {
                Die(gameObject);
            }
        }

        public bool IsHungry()
        {
            return actualHunger < maxNutrition * 0.5f;
        }

        public void Eat(int nutrition)
        {
            actualHunger += nutrition;
        }

        public bool IsThirsty()
        {
            return actualThirst < maxHydration * 0.5f;
        }

        public void Drink(int hydration)
        {
            actualThirst += hydration;
        }

        IEnumerator HungerTimer()
        {
            isHungry = false;
            yield return new WaitForSecondsRealtime(hungerIncreaseRate);
            actualHunger--;
            isHungry = true;
        }

        IEnumerator ThirstTimer()
        {
            isThirsty = false;
            yield return new WaitForSecondsRealtime(thirstIncreaseRate);
            actualThirst--;
            isThirsty = true;
        }

        private void Die(GameObject animal)
        {
            if (animal.GetComponent<Monkey>())
            {
                AnimalStatistics.Instance.RemoveAnimal(animal.GetComponent<Monkey>(), DeathType.Hunger);
            }
            else
            {
                if (animal.GetComponent<Hawk>())
                {
                    AnimalStatistics.Instance.RemoveAnimal(animal.GetComponent<Hawk>(), DeathType.Hunger);
                }
            }
            Destroy(gameObject);
        }
    }
}