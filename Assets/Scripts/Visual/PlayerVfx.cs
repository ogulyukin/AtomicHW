using Player;
using UnityEngine;

namespace Visual
{
    public class PlayerVfx : MonoBehaviour
    {
        [SerializeField] private PlayerModel playerModel;
        [SerializeField] private ParticleSystem takeDamageParticle;
        [SerializeField] private ParticleSystem fireParticle;
        public void OnEnable()
        {
            playerModel.onFire.Subscribe(OnFire);
            playerModel.OnTakeDamage.Subscribe(OnTakeDamage);
        }

        public void OnDisable()
        {
            playerModel.onFire.UnSubscribe(OnFire);
            playerModel.OnTakeDamage.UnSubscribe(OnTakeDamage);
        }

        private void OnFire()
        {
            fireParticle.transform.position = playerModel.firePoint.position;
            fireParticle.Play();
        }

        private void OnTakeDamage(int damage)
        {
            takeDamageParticle.Play();
        }
    }
}
