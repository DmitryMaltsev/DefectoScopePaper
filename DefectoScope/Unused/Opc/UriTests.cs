using System;

namespace DefectoScope
{
    
    public class UriTests
    {
        public void TestCleanup()
        {
        }

        
        public void Test_Build()
        {
            var uriBuilder = new UriBuilder
            {
                Scheme = "opcda",
                Host = "localhost",
                Path = OpcClient.OpcProgId
            };
            var _ = uriBuilder.Uri;
        }
     }
}