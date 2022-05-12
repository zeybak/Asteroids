using _Asteroids.Scripts.Data;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Asteroids.Scripts.Systems
{
    public class PlayerMovementSystem : ComponentSystem
    {
        private const string HorizontalMovementInputName = "Horizontal";
        private const string VerticalMovementInputName = "Vertical";
        
        protected override void OnUpdate()
        {
            Entities.ForEach((ref PhysicsVelocity physicsVelocity, ref PlayerMovementData movementData) =>
            {
                physicsVelocity.Linear.x = Input.GetAxis(HorizontalMovementInputName) * movementData.Speed;
                physicsVelocity.Linear.z = Input.GetAxis(VerticalMovementInputName) * movementData.Speed;
            });
        }
    }
}