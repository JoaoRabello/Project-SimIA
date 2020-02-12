using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SurvivalControllers
{
    public class HungerController : MonoBehaviour
    {
        [Header("Attributes")]
        [SerializeField] private int maxHunger = 10;
        private int actualHunger;
        private bool isHungry = true;

        public Text text;

        private void Start()
        {
            actualHunger = maxHunger;
        }

        private void Update()
        {
            if (isHungry)
            {
                StartCoroutine(HungerTimer());
            }
            if (actualHunger <= 0)
            {
                Destroy(this.gameObject);
            }
        }

        public bool IsHungry()
        {
            if (actualHunger >= (maxHunger * 0.75f))
                return false;
            else
                return true;
        }

        public void Eat()
        {
            print("Ate");
            actualHunger += 4;
        }

        IEnumerator HungerTimer()
        {
            isHungry = false;
            yield return new WaitForSecondsRealtime(2f);
            actualHunger--;
            text.text = "Energia: " + actualHunger;
            isHungry = true;
        }
    }
}
