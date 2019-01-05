using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    public GameObject ball;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Awake()
    {
        //spawn balls
        for (int x = -20; x < 20; x++)
        {
            for (int z = -20; z < 20; z++)
            {
                GameObject spawnBall = Instantiate(ball, new Vector3(x, ball.transform.position.y, z), Quaternion.identity);
                spawnBall.transform.SetParent(this.transform.parent);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
