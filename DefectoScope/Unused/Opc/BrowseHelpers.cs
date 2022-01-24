using System.Diagnostics;
using TitaniumAS.Opc.Client.Da.Browsing;

namespace DefectoScope
{
    internal static class BrowseHelpers
    {
        public static void BrowseChildren(string itemId, IOpcDaBrowser browser)
        {
            var opcElements = browser.GetElements(itemId);

            foreach (var opcBrowseElement in opcElements)
            {
                Debug.WriteLine(opcBrowseElement);
                foreach (var opcDaItemProperty in opcBrowseElement.ItemProperties.Properties)
                {
                    Debug.Indent();
                    Debug.WriteLine(opcDaItemProperty);
                    Debug.Unindent();
                }

                if (!opcBrowseElement.HasChildren) continue;

                Debug.Indent();
                BrowseChildren(opcBrowseElement.ItemId, browser);
                Debug.Unindent();
            }
        }
    }
}