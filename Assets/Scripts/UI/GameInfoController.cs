using System;
using System.Collections.Generic;
using Player;
using Zomby;

namespace UI
{
    public sealed class GameInfoController : IDisposable
    {
        private readonly List<UITextView> textViews = new();
        private readonly PlayerModel playerModel;
        private readonly ZombySpawnSystem zombySpawnSystem;

        public GameInfoController(List<UITextView> textViews, PlayerModel playerModel, ZombySpawnSystem zombySpawnSystem)
        {
            this.textViews.AddRange(textViews);
            this.playerModel = playerModel;
            this.zombySpawnSystem = zombySpawnSystem;
            playerModel.health.Subscribe(HitPointsChanged);
            HitPointsChanged(playerModel.health.Value);
            playerModel.amoAmount.Subscribe(BulletAmountChanged);
            BulletAmountChanged(playerModel.amoAmount.Value);
            zombySpawnSystem.killsCount.Subscribe(KillsChanged);
            KillsChanged(zombySpawnSystem.killsCount.Value);
        }

        public void Dispose()
        {
            playerModel.health.UnSubscribe(HitPointsChanged);
            playerModel.amoAmount.UnSubscribe(BulletAmountChanged);
            zombySpawnSystem.killsCount.UnSubscribe(KillsChanged);
        }

        private void HitPointsChanged(int value)
        {
            GetView(TextTypes.HitPoints).SetText($"HIP POINTS: {value}");
        }

        private void BulletAmountChanged(int value)
        {
            GetView(TextTypes.Bullet).SetText($"BULLETS: {value}/{playerModel.maxAmoAmount.Value}");
        }

        private void KillsChanged(int value)
        {
            GetView(TextTypes.Kills).SetText($"KILLS: {value}");
        }

        private UITextView GetView(TextTypes type)
        {
            foreach (var view in textViews)
            {
                if (view.type == type)
                {
                    return view;
                }
            }

            throw new Exception($"No view with type: {type}!!");
        }
    }
}
