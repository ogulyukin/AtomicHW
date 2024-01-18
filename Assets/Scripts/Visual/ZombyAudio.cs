using UnityEngine;
using Zomby;

namespace Visual
{
    public class ZombyAudio : MonoBehaviour
    {
        [SerializeField] private AudioSource startAudio;
        [SerializeField] private AudioSource attackAudio;
        [SerializeField] private AudioSource deathAudio;
        [SerializeField] private ZombyModel zombyModel;

        private void OnEnable()
        {
            startAudio.Play();
            zombyModel.onDeath.Subscribe(OnDeath);
            zombyModel.onHit.Subscribe(OnHit);
        }

        private void OnDisable()
        {
            zombyModel.onDeath.UnSubscribe(OnDeath);
            zombyModel.onHit.UnSubscribe(OnHit);
        }

        private void OnDeath()
        {
            deathAudio.Play();
        }
        

        private void OnHit()
        {
            attackAudio.Play();
        }
    }
}
