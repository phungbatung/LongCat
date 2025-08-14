using UnityEngine;

public interface IMovementHandler 
{
    Transform Target { get; set; }
    Cell DestinationCell { get; set; }
    Vector3 Destination { get; set; }
    float MoveSpeed { get; set; }
    bool ReachedTarget { get; set; }
    void SetTarget(Cell destinationCell);
    void StartMove(Transform target, float moveSpeed, GameObject bodyPrefab = null);
    void UpdateMove();
}
