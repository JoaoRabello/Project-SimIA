using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace SurvivalManagers
{
    public class NutritionManager : MonoBehaviour
    {
        private CameraController cameraController;

        [Header("Attributes")]
        [SerializeField] private int maxNutrition = 10;
        [SerializeField] private float hungerIncreaseRate = 2f;
        private float actualHunger;
        private bool isHungry = true;
        [SerializeField] private int maxHydration = 10;
        [SerializeField] private float thirstIncreaseRate = 2f;
        private float actualThirst;
        private bool isThirsty = true;

        public Slider nutririonSlider;
        public Slider hydrationSlider;

        private void Start()
        {
            actualHunger = maxNutrition;
            actualThirst = maxHydration;
            cameraController = FindObjectOfType<CameraController>();
        }

        private void Update()
        {
            if (isThirsty)
                StartCoroutine(ThirstTimer());
            if (isHungry)
                StartCoroutine(HungerTimer());

            if (actualHunger <= 0)
            {
                cameraController.RemoveFromList(gameObject);
                Destroy(gameObject);
            }

            nutririonSlider.value = actualHunger / maxNutrition;
            hydrationSlider.value = actualThirst / maxHydration;
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
    }
}