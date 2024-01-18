using Player;
using UnityEngine;

namespace Visual
{
    public class PlayerAudio : MonoBehaviour
    {
        [SerializeField] private AudioSource fireAudio;
        [SerializeField] private AudioSource stepAudio;
        [SerializeField] private AudioSource takeDamageAudio;
        [SerializeField] private AudioSource deathAudio;
        [SerializeField] private AudioSource reloadAmoAudio;
        [SerializeField] private int reloadAmoSoundAt = 5;
        
        [SerializeField] private PlayerModel playerModel;
        [SerializeField] private AnimatorDispatcher animatorDispatcher;
        private int reloadAmoCounter;

        public void OnEnable()
        {
            playerModel.onFire.Subscribe(OnFire);
            playerModel.onReloadAmo.Subscribe(OnReloadAmo);
            animatorDispatcher.OnEventReceived += OnAnimatorEvent;
            playerModel.onDeath.Subscribe(OnDeath);
            playerModel.OnTakeDamage.Subscribe(OnTakeDamage);
        }

        public void OnDisable()
        {
            playerModel.onFire.UnSubscribe(OnFire);
            playerModel.onReloadAmo.UnSubscribe(OnReloadAmo);
            animatorDispatcher.OnEventReceived -= OnAnimatorEvent;
            playerModel.onDeath.UnSubscribe(OnDeath);
            playerModel.OnTakeDamage.Subscribe(OnTakeDamage);
        }

        private void OnFire()
        {
            fireAudio.Play();
        }

        private void OnReloadAmo()
        {
            if (reloadAmoCounter < 0)
            {
                reloadAmoAudio.Play();
                reloadAmoCounter = reloadAmoSoundAt;
                return;
            }

            reloadAmoCounter--;
        }

        private void OnAnimatorEvent(string eventName)
        {
            if (eventName == "step")
            {
                stepAudio.Play();
            }
        }

        private void OnDeath()
        {
            deathAudio.Play();
        }

        private void OnTakeDamage(int _)
        {
            if(playerModel.isAlive.Value) takeDamageAudio.Play();
        }
    }
}
