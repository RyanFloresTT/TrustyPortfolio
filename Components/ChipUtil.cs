using MudBlazor;

namespace TrustyPortfolio.Components {
    public static class ChipUtil {
        enum ColorEnum {
            Secondary,
            Info,
            Success,
            Warning,
            Error
        }
        
        /// <summary>
        /// Uses the modulo operator to determine which color to return, will return the same color for the same ID everytime.
        /// </summary>
        /// <param name="tagId"></param>
        /// <returns>A MudBlazor color given the Tag's ID</returns>
        public static Color GetChipColor(Guid tagId) {
            byte[] bytes = tagId.ToByteArray();
            int sum = bytes.Sum(b => (int)b);
            int index = sum % Enum.GetNames(typeof(ColorEnum)).Length;
            ColorEnum colorEnum = (ColorEnum)index;

            switch (colorEnum) {
                case ColorEnum.Secondary:
                    return Color.Secondary;
                case ColorEnum.Info:
                    return Color.Info;
                case ColorEnum.Success:
                    return Color.Success;
                case ColorEnum.Warning:
                    return Color.Warning;
                case ColorEnum.Error:
                    return Color.Error;
                default:
                    return Color.Default;
            }
        }
    }
}
