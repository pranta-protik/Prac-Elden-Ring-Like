using TMPro;

namespace ERL
{
    public static class TextMeshProExtensions
    {
        public static void ChangeAlpha(this TextMeshPro textMeshPro, float alpha)
        {
            var color = textMeshPro.color;
            color.a = alpha;
            textMeshPro.color = color;
        }

        public static void ChangeAlpha(this TextMeshProUGUI textMeshProUGUI, float alpha)
        {
            var color = textMeshProUGUI.color;
            color.a = alpha;
            textMeshProUGUI.color = color;
        }
    }
}