using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IEndDragHandler, IBeginDragHandler, IDragHandler
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private Image slot;
    [SerializeField] private GameObject itemToSpawn;
    [SerializeField] private LayerMask layer;

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        rectTransform.anchoredPosition = slot.GetComponent<RectTransform>().anchoredPosition;

        Vector3 mousePos = Input.mousePosition;

        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit, 1000f, layer))
        {
            Vector3 worldPos = hit.point;

            if(itemToSpawn.GetComponent<Monkey>())
            {
                AnimalFactory.CreateMonkey(5, 10, 50, worldPos, itemToSpawn.GetComponent<Monkey>().sex);
            }
            else
            {
                if (itemToSpawn.GetComponent<Hawk>())
                {
                    AnimalFactory.CreateHawk(3, 100, 200, new Vector3(worldPos.x, worldPos.y + 5f, worldPos.z), itemToSpawn.GetComponent<Hawk>().sex);
                }
                else
                {
                    Instantiate(itemToSpawn, worldPos, Quaternion.identity);
                }
            }
        }
    }
}
