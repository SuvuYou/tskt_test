using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    private readonly int MOVEMENT_SPEED = Animator.StringToHash("MovementSpeed");

    [SerializeField] private Animator _animationController;
    [SerializeField] private EnemyController _enemyController;

    private void Start()
    {
        _enemyController.EnemyMovementSystem.State.OnStartMoving += () => 
        { 
            _animationController.SetFloat(MOVEMENT_SPEED, 1); 
        };
        _enemyController.EnemyMovementSystem.State.OnStopMoving += () => 
        { 
            _animationController.SetFloat(MOVEMENT_SPEED, 0); 

            Debug.Log("Stop moving");
        };
    }
}
