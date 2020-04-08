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
        private int actualHunger;
        private bool isHungry = true;

        public Text text;

        private void Start()
        {
            actualHunger = maxNutrition;
            cameraController = FindObjectOfType<CameraController>();
        }

        private void Update()
        {
            if (isHungry)
            {
                StartCoroutine(HungerTimer());
            }
            if (actualHunger <= 0)
            {
                cameraController.RemoveFromList(gameObject);
                Destroy(gameObject);
            }
        }

        public bool IsHungry()
        {
            if (actualHunger >= (maxNutrition * 0.5f))
                return false;
            else
                return true;
        }

        public void Eat(int nutrition)
        {
            actualHunger += nutrition;
        }

        IEnumerator HungerTimer()
        {
            isHungry = false;
            yield return new WaitForSecondsRealtime(hungerIncreaseRate);
            actualHunger--;
            text.text = "Nutrição: " + actualHunger;
            isHungry = true;
        }
    }
}