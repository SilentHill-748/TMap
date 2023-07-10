using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace TMap.MVVM.Model.Settings;

public class PipelineSettingsModel
{
    /*
    Коллекция труб
        Изоляция трубы
            Материал изоляции
            Толщина изоляции
            радиус/диаметр изоляции

    Глубина погружения (на какой глубине идет канал).
    Центр оси трубопровода - ширина карты деленная на 2.

    Ширина канала
    Высота канала
    Толщина канала
    материал канала

    Высота Коллектора
    Ширина Коллектора
    Толщина коллектора
    материал коллектора
    Число труб в коллекторе.

    Температурная конфигурация.
     */

    public PipelineSettingsModel(MaterialHelper materialHelper)
    {
        //TODO: PipelineSettingsModel нужно внедрить материал железобетона.
        Channel = new PipelineChannel(new MaterialModel() { Name = "Железобетон", ColorHexCode = "#5c533e" });

        PipeMaterials = materialHelper.GetPipeMaterials();
        PipeInsulationMaterials = materialHelper.GetPipeInsulationMaterials();
        ChannelInsulationMaterials = materialHelper.GetChannelInsulationMaterials();
    }

    public bool IsSkiped { get; set; }
    public PipelineChannel Channel { get; set; }
    public ObservableCollection<MaterialModel> PipeMaterials { get; }
    public ObservableCollection<MaterialModel> PipeInsulationMaterials { get; }
    public ObservableCollection<MaterialModel> ChannelInsulationMaterials { get; }
}
