using TitaniumAS.Opc.Client.Common;
using TitaniumAS.Opc.Client.Da;
using TitaniumAS.Opc.Client.Da.Browsing;

namespace DefectoScope
{
    public class OpcDaBrowser3Tests
    {
        private OpcDaServer _grayBox;
        private OpcDaBrowser3 _grayBoxBrowser;
        private OpcDaBrowser3 _opcBrowser;
        private OpcDaServer _opcServer;

        public void TestInitialize()
        {
            _opcServer = new OpcDaServer(UrlBuilder.Build(OpcClient.OpcProgId));
            _opcServer.Connect();
            _grayBox = new OpcDaServer(UrlBuilder.Build("Graybox.Simulator.1"));
            _grayBox.Connect();
            _opcBrowser = new OpcDaBrowser3(_opcServer);
            _grayBoxBrowser = new OpcDaBrowser3(_grayBox);
        }

        public void TestCleanup()
        {
            _grayBox.Dispose();
            _opcServer.Dispose();
        }


        public void Test_Browser_Filter_Branches()
        {
            var _ = _opcBrowser.GetElements(
                "",
                new OpcDaElementFilter {ElementType = OpcDaBrowseFilter.Branches}
            );
        }


        public void Test_Browser_Filter_Items()
        {
            var _ = _opcBrowser.GetElements(
                "",
                new OpcDaElementFilter {ElementType = OpcDaBrowseFilter.Items}
            );
        }


        public void Test_Browser_Filter_All()
        {
            var _ = _opcBrowser.GetElements("");
        }


        public void Test_Browser_Filter_Name()
        {
            var _ = _opcBrowser.GetElements(
                "",
                new OpcDaElementFilter {ElementType = OpcDaBrowseFilter.All, Name = "*Cli*"}
            );
        }


        public void Test_Browser_TraverseTree_Matrikon()
        {
            BrowseHelpers.BrowseChildren("", _opcBrowser);
        }


        public void Test_Browser_GetProperties()
        {
            var properties = _opcBrowser.GetProperties(
                new[] {"Random.Int1", "Random.Int2"},
                new OpcDaPropertiesQuery(true, 1, 2, 3, 4, 5, 6)
            );
            foreach (var _ in properties) { }
        }


        public void Test_Browser_GetProperties_All()
        {
            var properties = _grayBoxBrowser.GetProperties(
                new[] {"numeric.random.int32", "numeric.random.int16"},
                new OpcDaPropertiesQuery(true)
            );
            foreach (var _ in properties) { }
        }


        public void Test_Browser_GetProperties_NoValue()
        {
            var _ = _grayBoxBrowser.GetProperties(
                new[] {"numeric.random.int32", "numeric.random.int16"},
                new OpcDaPropertiesQuery()
            );
        }


        public void Test_Browser_GetElements_WithProperties()
        {
            var _ = _grayBoxBrowser.GetElements("numeric.random", null, new OpcDaPropertiesQuery());
        }


        public void Test_Browser_TraverseTree_GrayBox()
        {
            BrowseHelpers.BrowseChildren("", _grayBoxBrowser);
        }


        public void Test_GetProperties()
        {
            var _ = _grayBoxBrowser.GetProperties(new[] {"numeric.random.int32"});
        }
    }
}