﻿namespace TMap.WPFCore.Commands.Settings.Map
{
    public class RemoveMapLayerCommand : ParameterizedCommandBase<Layer>
    {
        private readonly MapSettingsViewModel _viewModel;

        public RemoveMapLayerCommand(MapSettingsViewModel mapSettingsViewModel)
        {
            ArgumentNullException.ThrowIfNull(mapSettingsViewModel, nameof(mapSettingsViewModel));

            _viewModel = mapSettingsViewModel;
        }

        protected override void Execute(Layer parameter)
        {
            ArgumentNullException.ThrowIfNull(parameter, nameof(parameter));

            _viewModel.Settings.MapSoilLayers.Remove(parameter);
        }
    }
}
