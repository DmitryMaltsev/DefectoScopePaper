using TitaniumAS.Opc.Client.Common;
using TitaniumAS.Opc.Client.Da;
using TitaniumAS.Opc.Client.Da.Browsing;

namespace DefectoScope
{
    public class OpcDaBrowser1Tests
    {
        private OpcDaBrowser1 _opcBrowser;
        private OpcDaServer _server;

        public void TestInitialize()
        {
            var uri = UrlBuilder.Build(OpcClient.OpcProgId);
            _server = new OpcDaServer(uri);
            _server.Connect();
            _opcBrowser = new OpcDaBrowser1(_server);
        }

        public void TestCleanup()
        {
            _server.Dispose();
        }


        public void Test_Browser_TraverseTree()
        {
            BrowseChildren("");
        }


        public void Test_GetElements_AllProperties()
        {
            var _ = _opcBrowser.GetElements("", null, new OpcDaPropertiesQuery());
        }


        public void Test_GetElements_Property()
        {
            var _ = _opcBrowser.GetElements(
                "",
                null,
                new OpcDaPropertiesQuery(true, OpcDaItemPropertyIds.OPC_PROP_VALUE)
            );
        }


        public void Test_GetElements_Branches()
        {
            _opcBrowser.GetElements(""); // Expand root
            var _ = _opcBrowser.GetElements(
                "Simulation Items¥",
                new OpcDaElementFilter {ElementType = OpcDaBrowseFilter.Branches},
                new OpcDaPropertiesQuery(true, OpcDaItemPropertyIds.OPC_PROP_VALUE)
            );
        }


        public void Test_GetElements_Leafs()
        {
            var elements = _opcBrowser.GetElements(""); // Expand root
            foreach (var opcDaBrowseElement in elements)
            {
                if (opcDaBrowseElement.HasChildren) _opcBrowser.GetElements(opcDaBrowseElement.ItemId);
            }

            elements = _opcBrowser.GetElements(
                "Random",
                new OpcDaElementFilter {ElementType = OpcDaBrowseFilter.Items},
                new OpcDaPropertiesQuery(true, OpcDaItemPropertyIds.OPC_PROP_VALUE)
            );
        }


        public void Test_Browser_TraverseTree_With_GetElement()
        {
            BrowseHelpers.BrowseChildren(null, _opcBrowser);
        }

        private void BrowseChildren(string itemId)
        {
            var branches = _opcBrowser.GetElements(
                itemId,
                new OpcDaElementFilter {ElementType = OpcDaBrowseFilter.Branches}
            );
            foreach (var branch in branches) BrowseChildren(branch.ItemId);

            var leafs = _opcBrowser.GetElements(itemId, new OpcDaElementFilter {ElementType = OpcDaBrowseFilter.Items});
            foreach (var _ in leafs) { }
        }
    }
}