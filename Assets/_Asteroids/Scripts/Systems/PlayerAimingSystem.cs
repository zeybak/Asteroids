using _Asteroids.Scripts.Data;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

namespace _Asteroids.Scripts.Systems
{
    public class PlayerAimingSystem : ComponentSystem
    {
        private Camera _camera;

        protected override void OnStartRunning()
        {
            _camera = Camera.main;
        }
        
        protected override void OnUpdate()
        {
            Entities.ForEach((ref PlayerMovementData movementData, ref Rotation rotation, ref Translation translation) =>
            {
                if (!_camera)
                {
                    _camera = Camera.main;
                    if (!_camera) return;
                }
                
                var playerLocation = new Vector3(translation.Value.x, translation.Value.y, translation.Value.z);
                var mouseLocation = _camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, _camera.transform.position.y));
                var forwardDirection = (new Vector3(mouseLocation.x, mouseLocation.y, mouseLocation.z) - playerLocation).normalized;

                rotation.Value = Quaternion.LookRotation(forwardDirection, Vector3.up);
            });
        }
    }
}