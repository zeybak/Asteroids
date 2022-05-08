using _Asteroids.Scripts.Data;
using Unity.Entities;
using Unity.Physics;
using Unity.Transforms;
using UnityEngine;

namespace _Asteroids.Scripts.Systems
{
    public class PlayerMovementSystem : ComponentSystem
    {
        // @TODO: Move to data
        private const float MovementSpeed = 1.0f;
        
        protected override void OnUpdate()
        {
            Entities.ForEach((ref PhysicsVelocity physicsVelocity, ref PlayerInputData inputData, ref Rotation rotation, ref Translation translation) =>
            {
                physicsVelocity.Linear.x = inputData.HorizontalInput * MovementSpeed;
                physicsVelocity.Linear.z = inputData.VerticalInput * MovementSpeed;

                var playerLocation = new Vector3(translation.Value.x, translation.Value.y, translation.Value.z);
                var forwardDirection = (new Vector3(inputData.MouseInput.x, inputData.MouseInput.y, inputData.MouseInput.z) - playerLocation).normalized;

                rotation.Value = Quaternion.LookRotation(forwardDirection, Vector3.up);
            });
        }
    }
}