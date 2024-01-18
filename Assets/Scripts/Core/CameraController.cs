using Player;
using UnityEngine;
using Zenject;

namespace Core
{
    public sealed class CameraController : ITickable
    {
        private readonly Camera camera;
        private readonly PlayerModel playerModel;
        private readonly Vector3 initialPosition;

        public CameraController(Camera camera, PlayerModel playerModel)
        {
            this.camera = camera;
            this.playerModel = playerModel;
            initialPosition = camera.transform.position;
        }

        public void Tick()
        {
            camera.transform.position = playerModel.transform.position + initialPosition;
        }
    }
}
