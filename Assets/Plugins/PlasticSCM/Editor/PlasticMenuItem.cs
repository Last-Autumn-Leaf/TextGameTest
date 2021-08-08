using UnityEditor;

using Unity.PlasticSCM.Editor.UI;

namespace Unity.PlasticSCM.Editor
{
    [InitializeOnLoad]
    class PlasticMenuItem
    {
        static PlasticMenuItem()
        {
            EditorApplication.update += RunOnce;
        }

        static void RunOnce()
        {
            EditorApplication.update -= RunOnce;

            AddMenu();
        }

        static void AddMenu()
        {
            HandleMenuItem.AddMenuItem(
                MENU_ITEM_NAME, MENU_ITEM_PRIORITY,
                ShowPanel, ValidateMenu);

            HandleMenuItem.UpdateAllMenus();
        }

        static bool ValidateMenu()
        {
            return !CollabPlugin.IsEnabled();
        }

        static void ShowPanel()
        {
            ShowWindow.Plastic();
        }

        const string MENU_ITEM_NAME = "Window/" + UnityConstants.PLASTIC_WINDOW_TITLE;

        const int MENU_ITEM_PRIORITY = 1000;
    }
}
