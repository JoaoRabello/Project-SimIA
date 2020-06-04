using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    private static int fireCount = 0;

    public float heatRange;
    private float heatAmount = 2;
    public LayerMask treeLayerMask;

    private float timer; 
    [SerializeField] private float timeToExtinguish = 20f;

    private void Start()
    {
        if(fireCount == 0)
        {
            EventLogger.Instance.LogEvent(EventType.Fire);
        }
        fireCount++;
    }

    void Update()
    {
        Collider[] trees = Physics.OverlapSphere(transform.position, heatRange, treeLayerMask);
        
        if(trees.Length > 0)
        {
            float treeHeat = 0;
            float treeDistance = 0;
            for (int i = 0; i < trees.Length; i++)
            {
                treeDistance = Vector3.Distance(transform.position, trees[i].transform.position);
                treeHeat = heatRange / treeDistance * heatAmount / 100;

                trees[i].GetComponent<Tree>().IncreaseHeat(treeHeat, this);
            }
        }

        LifeSpan();
    }

    private void LifeSpan()
    {
        if (timer > timeToExtinguish)
        {
            Destroy(gameObject);
        }
        else
        {
            timer += Time.deltaTime;
        }
    }

    public Fire AddSpot(Vector3 pos)
    {
        return Instantiate(gameObject, new Vector3(pos.x, pos.y + 0.5f, pos.z), Quaternion.identity).GetComponent<Fire>();
    }
}
