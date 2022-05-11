using _Asteroids.Scripts.Data;
using Unity.Entities;
using UnityEngine;

namespace _Asteroids.Scripts.Systems
{
    public class PlayerInputSystem : ComponentSystem
    {
        private const string HorizontalMovementInputName = "Horizontal";
        private const string VerticalMovementInputName = "Vertical";
        private Camera _camera;

        protected override void OnStartRunning()
        {
            _camera = Camera.main;
        }

        protected override void OnUpdate()
        {
            Entities.ForEach((ref PlayerMovementData inputData) =>
            {
                inputData.HorizontalInput = Input.GetAxis(HorizontalMovementInputName);
                inputData.VerticalInput = Input.GetAxis(VerticalMovementInputName);

                if (_camera)
                {
                    inputData.MouseInput = _camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
                        Input.mousePosition.y, _camera.transform.position.y));
                }
                else
                    _camera = Camera.main;
            });
        }
    }
}