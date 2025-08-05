using DG.Tweening;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float offset;
    private void Awake()
    {
        
    }
    public void Shake(Direction direction)
    {
        Vector3 dir;
        switch (direction)
        {
            case Direction.Left:
                dir = Vector3.left;
                break; 
            case Direction.Right:
                dir = Vector3.right;
                break;
            case Direction.Up:
                dir = Vector3.forward;
                break;
            case Direction.Down:
                dir = Vector3.back;
                break;
            default: 
                dir = Vector3.zero;
                break;
        }

        Vector3 shakePosition = transform.position + offset*dir;
        transform.DOMove(shakePosition, 0.005f).SetLoops(4, LoopType.Yoyo);
    }    
}
