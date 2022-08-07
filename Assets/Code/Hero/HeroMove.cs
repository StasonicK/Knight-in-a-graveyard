using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class HeroMove : MonoBehaviour
{
    [SerializeField] private float _movementSpeed = 10f;

    private CharacterController _characterController;
    private IInputService _inputService;
    private HeroAnimator _heroAnimator;

    private void Awake()
    {
        _inputService = Game.InputService;

        _characterController = GetComponent<CharacterController>();
        _heroAnimator = GetComponent<HeroAnimator>();
    }

    private void Update()
    {
        Vector3 movementVector = Vector3.zero;

        if (!_heroAnimator.IsAttacking && _inputService.Axis.sqrMagnitude > Constants.Epsilon)
        {
            movementVector = Camera.main.transform.TransformDirection(_inputService.Axis);
            movementVector.y = 0;
            movementVector.Normalize();
            transform.forward = movementVector;
        }

        movementVector += Physics.gravity;

        _characterController.Move(_movementSpeed / 10 * movementVector * Time.deltaTime);
    }
}