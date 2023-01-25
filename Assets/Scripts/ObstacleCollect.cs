using System.Collections;
using System.Collections.Generic;
using RVO;
using UnityEngine;
using Lockstep.Math;

public class ObstacleCollect : MonoBehaviour
{
    void Awake()
    {
        BoxCollider[] boxColliders = GetComponentsInChildren<BoxCollider>();
        for (int i = 0; i < boxColliders.Length; i++)
        {
            float minX = boxColliders[i].transform.position.x -
                         boxColliders[i].size.x * boxColliders[i].transform.lossyScale.x * 0.5f;
            float minZ = boxColliders[i].transform.position.z -
                         boxColliders[i].size.z * boxColliders[i].transform.lossyScale.z * 0.5f;
            float maxX = boxColliders[i].transform.position.x +
                         boxColliders[i].size.x * boxColliders[i].transform.lossyScale.x * 0.5f;
            float maxZ = boxColliders[i].transform.position.z +
                         boxColliders[i].size.z * boxColliders[i].transform.lossyScale.z * 0.5f;

            IList<LFloat2> obstacle = new List<LFloat2>
            {
                new LFloat2(true, maxX, maxZ),
                new LFloat2(true, minX, maxZ),
                new LFloat2(true, minX, minZ),
                new LFloat2(true, maxX, minZ)
            };
            Simulator.Instance.addObstacle(obstacle);
        }
    }
}