using _Asteroids.Scripts.Data;
using Unity.Entities;
using Unity.Physics;
using Unity.Transforms;
using UnityEngine;

namespace _Asteroids.Scripts.Systems
{
    public class PlayerMovementSystem : ComponentSystem
    {
        protected override void OnUpdate()
        {
            Entities.ForEach((ref PhysicsVelocity physicsVelocity, ref PlayerMovementData movementData, ref Rotation rotation, ref Translation translation) =>
            {
                physicsVelocity.Linear.x = movementData.HorizontalInput * movementData.Speed;
                physicsVelocity.Linear.z = movementData.VerticalInput * movementData.Speed;

                var playerLocation = new Vector3(translation.Value.x, translation.Value.y, translation.Value.z);
                var forwardDirection = (new Vector3(movementData.MouseInput.x, movementData.MouseInput.y, movementData.MouseInput.z) - playerLocation).normalized;

                rotation.Value = Quaternion.LookRotation(forwardDirection, Vector3.up);
            });
        }
    }
}