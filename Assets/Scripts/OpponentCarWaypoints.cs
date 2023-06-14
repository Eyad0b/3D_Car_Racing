using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentCarWaypoints : MonoBehaviour
{
    [Header("Opponent Car")]
    public OpponentCar opponentCar;
    public Waypoint currentWayPoint;
    // Start is called before the first frame update
    private void Awake()
    {
        opponentCar = GetComponent<OpponentCar>();
    }

    private void Start()
    {
        opponentCar.LocateDestination(currentWayPoint.GetPosition());
    }

    // Update is called once per frame
    private void Update()
    {
        if(opponentCar.destinationReached)
        {
            currentWayPoint = currentWayPoint.nextWaypoint;
            opponentCar.LocateDestination(currentWayPoint.GetPosition());
        }
    }
}
