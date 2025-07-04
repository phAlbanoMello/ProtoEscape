using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    public static List<CustomCollision> obstacles = new List<CustomCollision>();

    [SerializeField] private string obstacleTag = "Obstacle";

    void Awake()
    {
        obstacles.Clear();

        GameObject[] objs = GameObject.FindGameObjectsWithTag(obstacleTag);
        foreach (GameObject obj in objs)
        {
            CustomCollision cc = obj.GetComponent<CustomCollision>();
            if (cc != null && !cc.isTrigger)
            {
                obstacles.Add(cc);
            }
        }
    }

    void Update()
    {
        foreach (var obs in obstacles)
        {
            if (obs.isDynamic) { obs.UpdateBounds(obs.transform.position); }
        }
    }
}
