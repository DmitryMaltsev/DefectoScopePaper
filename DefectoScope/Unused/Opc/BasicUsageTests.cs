using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TitaniumAS.Opc.Client.Common;
using TitaniumAS.Opc.Client.Da;
using TitaniumAS.Opc.Client.Da.Browsing;

namespace DefectoScope
{
    
    public class BasicUsageTests
    {
        
        public void BrowsingAnOpcDaServerLocally()
        {
            var url = UrlBuilder.Build(OpcClient.OpcProgId);
            using (var server = new OpcDaServer(url))
            {
                server.Connect();

                var browser = new OpcDaBrowserAuto(server);
                BrowseChildren(browser);
            }
        }

        private static void BrowseChildren(IOpcDaBrowser browser, string itemId = null, int indent = 0)
        {
            var elements = browser.GetElements(itemId);

            foreach (var element in elements)
            {
                Trace.Write(new string(' ', indent));
                Trace.WriteLine(element);

                if (!element.HasChildren) continue;

                BrowseChildren(browser, element.ItemId, indent + 2);
            }
        }

        
        public void CreatingAGroupWithItemsInAnOpcDaServer()
        {
            var url = UrlBuilder.Build(OpcClient.OpcProgId);
            using (var server = new OpcDaServer(url))
            {
                server.Connect();

                CreateGroupWithItems(server);
            }
        }

        private static OpcDaGroup CreateGroupWithItems(IOpcDaServer server)
        {
            var group = server.AddGroup("opc.Device1.BDM2");
            group.IsActive = true;

            var itemsId = new[]
            {
                "opc.Device1.BDM2.Metso_OPC_Kep.Channel1.MetsoOPC.break",
                "opc.Device1.BDM2.Metso_OPC_Kep.Channel1.MetsoOPC.break2",
                "opc.Device1.BDM2.Metso_OPC_Kep.Channel1.MetsoOPC.smena_tambura",
                "opc.Device1.BDM2.Metso_OPC_Kep.Channel1.MetsoOPC.speed",
                "opc.Device1.BDM2.Metso_OPC_Kep.Channel1.MetsoOPC.weight",
                "opc.Device1.BDM2.Metso_OPC_Kep.Channel1.MetsoOPC.width"
            };

            var definitions = new OpcDaItemDefinition[itemsId.Length];
            for (var i = 0; i < definitions.Length; i++)
                definitions[i] = new OpcDaItemDefinition {ItemId = itemsId[i], IsActive = true};

            var results = group.AddItems(definitions);

            foreach (var result in results)
                if (result.Error.Failed) Console.WriteLine(@"Error adding items: {0}", result.Error);

            return group;
        }

        
        public void ReadValuesSynchronously()
        {
            var url = UrlBuilder.Build(OpcClient.OpcProgId);
            using (var server = new OpcDaServer(url))
            {
                server.Connect();

                var group = CreateGroupWithItems(server);

                var values = group.Read(group.Items, OpcDaDataSource.Device);

                foreach (var value in values)
                {
                    Console.WriteLine(
                        @"ItemId: {0}; Value: {1}; Quality: {2}; Timestamp: {3}",
                        value.Item.ItemId,
                        value.Value,
                        value.Quality,
                        value.Timestamp
                    );
                }
            }
        }

        
        public async Task ReadValuesAsynchronously()
        {
            var url = UrlBuilder.Build(OpcClient.OpcProgId);
            using (var server = new OpcDaServer(url))
            {
                server.Connect();

                var group = CreateGroupWithItems(server);

                var values = await group.ReadAsync(group.Items);

                foreach (var value in values)
                {
                    Console.WriteLine(
                        @"ItemId: {0}; Value: {1}; Quality: {2}; Timestamp: {3}",
                        value.Item.ItemId,
                        value.Value,
                        value.Quality,
                        value.Timestamp
                    );
                }
            }
        }

        
        public void WriteValuesToAnItemOfAnOpcServerSynchronously()
        {
            var url = UrlBuilder.Build(OpcClient.OpcProgId);
            using (var server = new OpcDaServer(url))
            {
                server.Connect();

                var group = CreateGroupWithItems(server);

                var item = group.Items.FirstOrDefault(i => i.ItemId == "smena_tambura");
                OpcDaItem[] items = {item};
                object[] values = {123};

                var results = group.Write(items, values);

                if (results[0].Failed) Console.WriteLine(@"Error writing value");

                var value = group.Read(items, OpcDaDataSource.Device)[0];
                Console.WriteLine(
                    @"ItemId: {0}; Value: {1}; Quality: {2}; Timestamp: {3}",
                    value.Item.ItemId,
                    value.Value,
                    value.Quality,
                    value.Timestamp
                );
            }
        }

        
        public async Task WriteValuesToAnItemOfAnOpcServerAsynchronously()
        {
            var url = UrlBuilder.Build(OpcClient.OpcProgId);
            using (var server = new OpcDaServer(url))
            {
                server.Connect();

                var group = CreateGroupWithItems(server);

                var item = group.Items.FirstOrDefault(i => i.ItemId == "Bucket Brigade.Int4");
                OpcDaItem[] items = {item};
                object[] values = {123};

                var results = await group.WriteAsync(items, values);

                if (results[0].Failed) Console.WriteLine(@"Error writing value");

                var value = group.Read(items, OpcDaDataSource.Device)[0];
                Console.WriteLine(
                    @"ItemId: {0}; Value: {1}; Quality: {2}; Timestamp: {3}",
                    value.Item.ItemId,
                    value.Value,
                    value.Quality,
                    value.Timestamp
                );
            }
        }

        
        public async Task GetAValueOfAnItemBySubscription()
        {
            var url = UrlBuilder.Build(OpcClient.OpcProgId);
            using (var server = new OpcDaServer(url))
            {
                server.Connect();

                var group = CreateGroupWithItems(server);

                group.ValuesChanged += OnGroupValuesChanged;
                group.UpdateRate = TimeSpan.FromMilliseconds(100); 

                await Task.Delay(1000);
            }
        }

        private static void OnGroupValuesChanged(object sender, OpcDaItemValuesChangedEventArgs args)
        {
            foreach (var value in args.Values)
            {
                Console.WriteLine(
                    @"ItemId: {0}; Value: {1}; Quality: {2}; Timestamp: {3}",
                    value.Item.ItemId,
                    value.Value,
                    value.Quality,
                    value.Timestamp
                );
            }
        }
    }
}