using UnityEngine;
using Zomby;

namespace Visual
{
    public class ZombyVfx : MonoBehaviour
    {
        [SerializeField] private ZombyModel zombyModel;
        [SerializeField] private ParticleSystem takeDamageParticle;
        public void OnEnable()
        {
            zombyModel.OnTakeDamage.Subscribe(OnTakeDamage);
        }

        public void OnDisable()
        {
            zombyModel.OnTakeDamage.UnSubscribe(OnTakeDamage);
        }



        private void OnTakeDamage(int damage)
        {
            takeDamageParticle.Play();
        }
    }
}
