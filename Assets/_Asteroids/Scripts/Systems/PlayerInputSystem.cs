using _Asteroids.Scripts.Data;
using Unity.Entities;
using UnityEngine;

namespace _Asteroids.Scripts.Systems
{
    public class PlayerInputSystem : ComponentSystem
    {
        // @TODO: Move to data
        private const string HorizontalAxisName = "Horizontal";
        private const string VerticalAxisName = "Vertical";
        private Camera _camera;

        protected override void OnStartRunning()
        {
            _camera = Camera.main;
        }

        protected override void OnUpdate()
        {
            Entities.ForEach((ref PlayerInputData inputData) =>
            {
                inputData.HorizontalInput = Input.GetAxis(HorizontalAxisName);
                inputData.VerticalInput = Input.GetAxis(VerticalAxisName);
                
                if (_camera)
                {
                    inputData.MouseInput = _camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
                        Input.mousePosition.y, _camera.transform.position.y));
                }
            });
        }
    }
}