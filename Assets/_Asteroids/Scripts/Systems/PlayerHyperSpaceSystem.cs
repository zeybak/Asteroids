using _Asteroids.Scripts.Data;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Asteroids.Scripts.Systems
{
    public class PlayerHyperSpaceSystem : ComponentSystem
    {
        private const string HyperSpaceInputName = "Jump";
        private Camera _camera;

        protected override void OnStartRunning()
        {
            _camera = Camera.main;
        }
        
        protected override void OnUpdate()
        {
            Entities.ForEach((ref PlayerMovementData movementData, ref Translation translation) =>
            {
                if (!_camera)
                {
                    _camera = Camera.main;
                    if (!_camera) return;
                }
                
                if (!Input.GetButtonDown(HyperSpaceInputName)) return;
                
                var cameraHeight = _camera.transform.position.y;
                var bottomLeftCorner = _camera.ScreenToWorldPoint(new Vector3(0f, 0f, cameraHeight));
                var topRightCorner = _camera.ScreenToWorldPoint(new Vector3(_camera.pixelWidth, _camera.pixelHeight, cameraHeight));

                translation.Value = new float3(
                    Random.Range(bottomLeftCorner.x, topRightCorner.x),
                    translation.Value.y,
                    Random.Range(bottomLeftCorner.y, topRightCorner.y));
            });
        }
    }
}