namespace TMap.MVVM.Validation;

public static class ValidationErrors
{
    public class MaterialErrors
    {
        public const string ThicknessError = "Толщина слоя должна быть не меньше 1 см!";
        public const string MaterialError = "Не выбран материал!";
        public const string HumidityError = "Влажность должна быть больше 0 гр/м³!";
        public const string InitTemperatureError = "Начальная температура материала должна быть между -70,00 и +170,00 °С!";
    }

    public static class MapSettingsErrors
    {
        public const string MapWidthError = "Ширина карты должна быть больше 1000 см!";
        public const string EnvironmentTemperatureError = "Начальная температура внешней среды должна быть между -70,00 и +170,00 °С!";
        public const string MapHeightError = "Общая глубина слоев должна быть не менее 100 см!";
    }

    public static class RoadSettingsErrors
    {
        public const string LayersError = "Число слоев дорожной одежды должно быть больше 0!";

        public static class CreateRoadLayerErrors
        {
            public const string WidthError = "Ширина слоя должна быть больше 700 см!";
        }

        public static class InputRoadSettingsErrors
        {
            public const string RoadWidthError = "Ширина дороги должна быть больше 700 см!";
            public const string MoundWidthError = "Ширина насыпи должна быть между 50 и 100 см!";
            public const string MoundHeightError = "Высота насыпи должна быть между 20 и 100 см!";
            public const string RoadsideWidthError = "Ширина обочины должна быть между 25 и 50 см!";
            public const string EdgeWidthError = "Край дороги должен быть не менее 50 см!";
        }
    }

    public static class PipelineSettingsErrors
    {
        public static class InsulationErrors
        {
            public const string RadiusError = "Радиус изоляционного покрытия должен быть больше радиуса трубы!";
            public const string ThicknessError = "Толщина слоя изоляции трубы должна быть между 1 и 50 см!";
        }

        public static class PipelineChannelErrors
        {
            public const string ThicknessError = "Толщина стенок коллектора должна быть от 5 до 25 см!";
            public const string HeightError = "Высота коллектора не должна быть меньше 14 и больше 3000 см";
            public const string PipeCenterlineError = "Глубина оси заложения труб должна быть ниже точки заложения коллектора минимум на 6 см!";
            public const string ChannelDepthError = "Глубина заложения коллектора должна быть ниже дорожной конструкции!";
            public const string InteraxalWidthError = "Расстояние между труб должно быть между 3 и 10 см!";
        }

        public static class PipelinePipeErrors
        {
            public static class CreatePipeErrors
            {
                public const string PipeMaterialTemperatureError = "Температура трубы должна быть между -70.00 и +170.00 °С";
                public const string RadiusError = "Радиус трубы должен быть между 6 и 27 см!";
                public const string PipeMaterialError = "Не выбран тип трубы!";
                public const string ThicknessError = "Толщина трубы должна быть между 1 и 3 см!";
                public const string TemperatureError = "Температура теплоносителя должна быть между -10.00 и +400.00 °С";
            }
        }
    } 
}
