using TitaniumAS.Opc.Client.Common;

namespace DefectoScope
{
    
    public class OpcServerEnumeratorAutoTests
    {
        public void TestCleanup()
        {

        }

        
        public void Test_Enumerate_OpcDa10_Servers()
        {
            var enumerator = new OpcServerEnumeratorAuto();
            var _ = enumerator.Enumerate(enumerator.Localhost, OpcServerCategory.OpcDaServer10);
        }

        
        public void Test_Enumerate_OpcDa20_Servers()
        {
            var enumerator = new OpcServerEnumeratorAuto();
            var _ = enumerator.Enumerate(null, OpcServerCategory.OpcDaServer20);
        }

        
        public void Test_Enumerate_OpcDa30_Servers()
        {
            var enumerator = new OpcServerEnumeratorAuto();
            var _ = enumerator.Enumerate("localhost", OpcServerCategory.OpcDaServer30);
        }

        
        public void Test_Enumerate_OpcDa_Servers()
        {
            var enumerator = new OpcServerEnumeratorAuto();
            var _ = enumerator.Enumerate("", OpcServerCategory.OpcDaServers);
        }

        
        public void Test_Enumerate_Hosts()
        {
            var enumerator = new OpcServerEnumeratorAuto();
            var _ = enumerator.EnumrateHosts();
        }
    }
}