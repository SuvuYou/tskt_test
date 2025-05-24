using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private readonly int MOVEMENT_SPEED = Animator.StringToHash("MovementSpeed");

    [SerializeField] private Animator _animationController;
    [SerializeField] private PlayerController _playerController;

    private void Start()
    {
        _playerController.PlayerMovementSystem.State.OnStartMoving += () => 
        { 
            _animationController.SetFloat(MOVEMENT_SPEED, 1); 
        };
        _playerController.PlayerMovementSystem.State.OnStopMoving += () => 
        { 
            _animationController.SetFloat(MOVEMENT_SPEED, 0); 

            Debug.Log("Stop moving");
        };
    }
}
