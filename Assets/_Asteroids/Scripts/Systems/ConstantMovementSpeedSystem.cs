using _Asteroids.Scripts.Data;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Random = UnityEngine.Random;

namespace _Asteroids.Scripts.Systems
{
    public class ConstantMovementSpeedSystem : ComponentSystem
    {
        protected override void OnUpdate()
        {
            Entities.ForEach((ref ConstantMovementSpeedData constantMovementSpeed, ref PhysicsVelocity physicsVelocity) =>
            {
                if (physicsVelocity.Linear.x != 0f) return;
                if (physicsVelocity.Linear.z != 0f) return;

                physicsVelocity.Linear = new float3(
                    Random.Range(-constantMovementSpeed.Speed, constantMovementSpeed.Speed), 
                    0f,
                    Random.Range(-constantMovementSpeed.Speed, constantMovementSpeed.Speed));
            });
        }
    }
}