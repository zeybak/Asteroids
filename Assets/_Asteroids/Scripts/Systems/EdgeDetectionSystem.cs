using _Asteroids.Scripts.Data;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Rendering;
using Unity.Transforms;
using UnityEngine;

namespace _Asteroids.Scripts.Systems
{
    public class EdgeDetectionSystem : JobComponentSystem
    {
        private Camera _camera;
        
        protected override void OnCreate()
        {
            _camera = Camera.main;
        }

        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            var bScreenDataFound = false;
            var minHorizontalLocation = 0f;
            var maxHorizontalLocation = 0f;
            var minVerticalLocation = 0f;
            var maxVerticalLocation = 0f;

            if (_camera)
            {
                bScreenDataFound = true;

                var cameraHeight = _camera.transform.position.y;
                var bottomLeftCorner = _camera.ScreenToWorldPoint(new Vector3(0f, 0f, cameraHeight));
                var topRightCorner =
                    _camera.ScreenToWorldPoint(new Vector3(_camera.pixelWidth, _camera.pixelHeight, cameraHeight));

                minHorizontalLocation = bottomLeftCorner.x;
                minVerticalLocation = bottomLeftCorner.z;

                maxHorizontalLocation = topRightCorner.x;
                maxVerticalLocation = topRightCorner.z;
            }
            else
                _camera = Camera.main;

            return Entities.ForEach((ref Translation translation) =>
            {
                if (!bScreenDataFound) return;
                
                if (translation.Value.x > maxHorizontalLocation)
                    translation.Value.x = minHorizontalLocation;
                else if (translation.Value.x < minHorizontalLocation)
                    translation.Value.x = maxHorizontalLocation;

                if (translation.Value.z > maxVerticalLocation)
                    translation.Value.z = minVerticalLocation;
                else if (translation.Value.z < minVerticalLocation)
                    translation.Value.z = maxVerticalLocation;
            }).Schedule(inputDeps);
        }
    }
}