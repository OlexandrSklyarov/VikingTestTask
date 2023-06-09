using UnityEngine;

namespace Gameplay.Cameras
{
    public interface ICameraController
    {
        void ActiveCamera(CameraController.CameraType type);
        void SetGameplayTarget(Transform followTarget);
    }
}