using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Common.Input
{
    public class InputAdaptor
    {
        public enum ControlMap {GAMPLAY, UI}
        
        public bool IsCurrentDeviceMouse => Gamepad.current == null ;
        
        private readonly PlayerInputAction _inputAction;

        private bool _isEnabled;

        public event Action<Vector2> OnMovement;
        public event Action<Vector2> OnLook;
        public event Action OnAttack;
        

        public InputAdaptor()
        {
            _inputAction = new PlayerInputAction();
        }



        public void SetActionMap(ControlMap map)
        {
            switch (map)
            {
                case ControlMap.GAMPLAY:
                    _inputAction.Gameplay.Enable();
                    _inputAction.UI.Disable();
                    break;
                
                case ControlMap.UI:
                    _inputAction.Gameplay.Disable();
                    _inputAction.UI.Enable();
                    break;
            }
        }

        public void Enable()
        {
            if (_isEnabled) return;
            
            _inputAction.Enable();

            _inputAction.Gameplay.Movement.performed += OnMovementHandler;
            _inputAction.Gameplay.Movement.canceled += OnMovementHandler;
            _inputAction.Gameplay.Look.performed += OnLookHandler;
            _inputAction.Gameplay.Look.canceled += OnLookHandler;
            _inputAction.Gameplay.Attack.started += OnAttackHandler;

            _isEnabled = true;
        }

       
        public void Disable()
        {
            if (!_isEnabled) return;
            
            _inputAction.Gameplay.Movement.performed -= OnMovementHandler;
            _inputAction.Gameplay.Movement.canceled -= OnMovementHandler;
            _inputAction.Gameplay.Look.performed -= OnLookHandler;
            _inputAction.Gameplay.Look.canceled -= OnLookHandler;
            _inputAction.Gameplay.Attack.started -= OnAttackHandler;

            _inputAction.Disable();
            _isEnabled = false;
        }
        
        
        private void OnAttackHandler(InputAction.CallbackContext ctx)
        {
            OnAttack?.Invoke();
        }
        
        
        private void OnMovementHandler(InputAction.CallbackContext ctx)
        {
            OnMovement?.Invoke(ctx.ReadValue<Vector2>());
        }

        
        private void OnLookHandler(InputAction.CallbackContext ctx)
        {
            OnLook?.Invoke(ctx.ReadValue<Vector2>());
        }
    }
}