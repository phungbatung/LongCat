using UnityEngine;

public class StopPointMovementHandler : IMovementHandler
{
    public Vector3 Destination { get; set; }
    public Transform Target { get; set; }
    public bool ReachedTarget { get; set; }
    public float MoveSpeed { get; set; }
    public Cell DestinationCell { get ; set ; }

    public void SetTarget(Cell destinationCell)
    {
        DestinationCell = destinationCell;
        Destination = DestinationCell.GetPosition();
    }

    public void StartMove(Transform target, float moveSpeed, GameObject bodyPrefab)
    {
        Target = target;
        MoveSpeed = moveSpeed;
        ReachedTarget = false;
    }

    public void UpdateMove()
    {
        Target.position = Vector3.MoveTowards(Target.position, Destination, MoveSpeed * Time.deltaTime);
        if (Vector3.Distance(Target.position, Destination) < 0.01f)
            ReachedTarget = true;
    }
}
