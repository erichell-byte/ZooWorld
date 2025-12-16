using System;
using ZooWorld.UI.ViewModels;
using TMPro;
using UnityEngine;
using Zenject;

namespace ZooWorld.UI.Views
{
    public class GameHudView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _preyCountText;
        [SerializeField] private TMP_Text _predatorCountText;
    
        private IGameHudViewModel _viewModel;
        private Action<string> _preyTextChangedHandler;
        private Action<string> _predatorTextChangedHandler;
    
        [Inject]
        public void Construct(IGameHudViewModel viewModel)
        {
            _viewModel = viewModel;
            BindViewModel();
        }
    
        private void BindViewModel()
        {
            _preyCountText.text = _viewModel.PreyCountText;
            _predatorCountText.text = _viewModel.PredatorCountText;

            _preyTextChangedHandler = text => _preyCountText.text = text;
            _predatorTextChangedHandler = text => _predatorCountText.text = text;

            _viewModel.PreyTextChanged += _preyTextChangedHandler;
            _viewModel.PredatorTextChanged += _predatorTextChangedHandler;
        }
    
        private void OnDestroy()
        {
            if (_viewModel != null)
            {
                _viewModel.PreyTextChanged -= _preyTextChangedHandler;
                _viewModel.PredatorTextChanged -= _predatorTextChangedHandler;
                _viewModel.Dispose();
            }
        }
    }
}
