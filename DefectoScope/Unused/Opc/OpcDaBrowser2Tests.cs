using TitaniumAS.Opc.Client.Common;
using TitaniumAS.Opc.Client.Da;
using TitaniumAS.Opc.Client.Da.Browsing;

namespace DefectoScope
{
    
    public class OpcDaBrowser2Tests
    {
        private OpcDaBrowser2 _opcBrowser;
        private OpcDaServer _server;

        public void TestInitialize()
        {
            _server = new OpcDaServer(UrlBuilder.Build(OpcClient.OpcProgId));
            _server.Connect();
            _opcBrowser = new OpcDaBrowser2(_server);
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
            var _ = _opcBrowser.GetElements(
                "Simulation Items¥",
                new OpcDaElementFilter {ElementType = OpcDaBrowseFilter.Branches},
                new OpcDaPropertiesQuery(true, OpcDaItemPropertyIds.OPC_PROP_VALUE)
            );
        }

        
        public void Test_GetElements_Leafs()
        {
            var _ = _opcBrowser.GetElements(
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