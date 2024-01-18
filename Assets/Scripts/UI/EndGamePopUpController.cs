using System;
using System.Threading.Tasks;
using Player;
using UnityEngine;


namespace UI
{
    public class EndGamePopUpController :IDisposable
    {
        private const float PopUpTimeout = 3f;
        private readonly PlayerModel playerModel;
        private readonly EndGamePopUpView endGamePopUp;

        public EndGamePopUpController(PlayerModel playerModel,EndGamePopUpView endGamePopUp)
        {
            this.playerModel = playerModel;
            this.endGamePopUp = endGamePopUp;
            playerModel.onDeath.Subscribe(EndGame);
        }

        private void EndGame()
        {
            EndGameTask();
        }

        private async void EndGameTask()
        {
            await Task.Delay(TimeSpan.FromSeconds(PopUpTimeout));
            endGamePopUp.ActivateObject();
            Time.timeScale = 0;
        }
        

        public void Dispose()
        {
            playerModel.onDeath.UnSubscribe(EndGame);
        }
    }
}
