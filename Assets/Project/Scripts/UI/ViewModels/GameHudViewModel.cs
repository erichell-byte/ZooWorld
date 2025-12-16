using System;
using ZooWorld.UI.Models;

namespace ZooWorld.UI.ViewModels
{
    public class GameHudViewModel : IGameHudViewModel
    {
        public string PreyCountText { get; private set; } = "Prey: 0";
        public string PredatorCountText { get; private set; } = "Predators: 0";

        public event Action<string> PreyTextChanged;
        public event Action<string> PredatorTextChanged;

        private readonly AnimalDeathStatsModel _statsModel;
    
        public GameHudViewModel(AnimalDeathStatsModel statsModel)
        {
            _statsModel = statsModel;

            _statsModel.DeadPreyCountChanged += OnPreyCountChanged;
            _statsModel.DeadPredatorCountChanged += OnPredatorCountChanged;
        }
        
        public void Dispose()
        {
            _statsModel.DeadPreyCountChanged -= OnPreyCountChanged;
            _statsModel.DeadPredatorCountChanged -= OnPredatorCountChanged;
        }

        private void OnPreyCountChanged(int count)
        {
            PreyCountText = $"Prey: {count}";
            PreyTextChanged?.Invoke(PreyCountText);
        }

        private void OnPredatorCountChanged(int count)
        {
            PredatorCountText = $"Predators: {count}";
            PredatorTextChanged?.Invoke(PredatorCountText);
        }
    }
}
