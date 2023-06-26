using System;

using TMap.WPFCore.Commands.Base;

namespace TMap.WPFCore.Commands.Settings
{
    public class RemoveMapLayerCommand : ParameterizedCommandBase<MapLayer>
    {
        private readonly MapSettingsViewModel _viewModel;

        public RemoveMapLayerCommand(MapSettingsViewModel mapSettingsViewModel)
        {
            ArgumentNullException.ThrowIfNull(mapSettingsViewModel, nameof(mapSettingsViewModel));    

            _viewModel = mapSettingsViewModel;
        }

        protected override void Execute(MapLayer parameter)
        {
            _viewModel.Settings.MapSoilLayers.Remove(parameter);
        }
    }
}
