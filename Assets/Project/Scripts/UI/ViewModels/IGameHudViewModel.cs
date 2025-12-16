using System;

namespace ZooWorld.UI.ViewModels
{
    public interface IGameHudViewModel
    {
        string PreyCountText { get; }
        string PredatorCountText { get; }
        event Action<string> PreyTextChanged;
        event Action<string> PredatorTextChanged;
        void Dispose();
    }
}
