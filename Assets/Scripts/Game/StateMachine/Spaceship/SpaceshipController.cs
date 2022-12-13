using JellyButton.Exam.Core.Data;
using JellyButton.Exam.Core.StateMachineBase;
using JellyButton.Exam.Game.Interactables;
using UnityEngine;

namespace JellyButton.Exam.Game.StateMachine.Spaceship
{
    public class SpaceshipController : StateMachine<SpaceshipController>
    {
        [SerializeField] private GameObject _effects;
        
        [Space][Header("Spaceship Movement Settings")]
        [SerializeField] private float _movementSpeed = 18f;
        [SerializeField] private float _movementSpeedMultiplierOnBoost = 1.2f;
        [SerializeField] private float _rotationSpeed = 5f;
        [SerializeField] private float _maxRotation = 45f;

        private Transform _transform;
        private Rigidbody _rigidbody;
        private Renderer _renderer;

        private void Awake()
        {
            _transform = transform;
            _renderer = GetComponent<Renderer>();
            _rigidbody = GetComponent<Rigidbody>();
            _rigidbody.sleepThreshold = 0;
        }

        private void OnEnable()
        {
            GameStateEvents.PreGameState += OnPreGameState;
            GameStateEvents.InGameState += OnInGameState;
            GameStateEvents.GameOverState += OnGameOverState;
        }

        private void OnDisable()
        {
            GameStateEvents.PreGameState -= OnPreGameState;
            GameStateEvents.InGameState -= OnInGameState;
            GameStateEvents.GameOverState -= OnGameOverState;
        }

        #region Switch State Methods

        private void OnPreGameState()
        {
            SetState(new SpaceshipPreGameState(this));
        }
        
        private void OnInGameState()
        {
            SetState(new SpaceshipInGameState(this));
        }

        private void OnGameOverState()
        {
            SetState(new SpaceshipGameOverState(this));
        }

        #endregion

        private void Update()
        {
            CurrentState?.Update();
        }
        
        private void OnCollisionEnter(Collision collision)
        {
            var collider = collision.collider;
            collider.GetComponent<IInteractable>()?.Interact();
            
            switch (collision.collider.tag)
            {
                case "Obstacle":
                {
                    collider.transform.position = _transform.position;
                    break;
                }
            }
        }
        
        internal void SetUpSpaceship()
        {
            _renderer.enabled = true;
            _effects.SetActive(true);
            _transform.position = Vector3.zero;
            _transform.rotation = Quaternion.identity;
        }

        internal void ExplodeSpaceshipShip()
        {
            _renderer.enabled = false;
            _effects.SetActive(false);
        }

        public void ReadInput()
        {
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                Move(Vector3.right);
                Rotate(Vector3.right, _maxRotation);
            }
            else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                Move(Vector3.left);
                Rotate(Vector3.left, _maxRotation);
            }
            else
            {
                Rotate(Vector3.left, 0f);
            }
        }

        #region Movement

        public void Move(Vector3 direction)
        {
            if (!Physics.Raycast(_transform.position + Vector3.down + direction * 0.5f, Vector3.down)) return;
            var offsetSpeed = _movementSpeed * (GlobalData.IsSpeeding ? _movementSpeedMultiplierOnBoost : 1);
            _transform.position += direction * offsetSpeed * Time.deltaTime;
        }

        private void Rotate(Vector3 direction, float endRotation)
        {
            var directionMultiplier = -direction.x;
            var desiredRotation = Quaternion.Euler(Vector3.forward * directionMultiplier * endRotation);
            _transform.rotation = Quaternion.Lerp(_transform.rotation, desiredRotation, Time.deltaTime * _rotationSpeed);
        }

        #endregion
    }
}
