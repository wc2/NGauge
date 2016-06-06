using System;
using System.Collections.Generic;
using Gauge.CSharp.Core;
using Gauge.Messages;

namespace Gauge.NCrunch.Specs.Reader
{
    internal sealed class GaugeSpecificationsService : IGaugeSpecificationsService
    {
        private readonly int _apiPort;

        public GaugeSpecificationsService(int apiPort)
        {
            _apiPort = apiPort;
        }

        IEnumerable<ProtoSpec> IGaugeSpecificationsService.GetSpecs()
        {
            var gaugeApiConnection = new GaugeApiConnection(new TcpClientWrapper(_apiPort));

            return GetSpecsFromGauge(gaugeApiConnection);
        }

        private static IEnumerable<ProtoSpec> GetSpecsFromGauge(GaugeApiConnection apiConnection)
        {
            var specsRequest = GetAllSpecsRequest.DefaultInstance;

            var apiMessage = APIMessage.CreateBuilder()
                .SetMessageId(GenerateMessageId())
                .SetMessageType(APIMessage.Types.APIMessageType.GetAllSpecsRequest)
                .SetAllSpecsRequest(specsRequest)
                .Build();

            var apiResponse = apiConnection.WriteAndReadApiMessage(apiMessage);

            return apiResponse
                .AllSpecsResponse
                .SpecsList;
        }

        private static long GenerateMessageId()
        {
            return DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
        }
    }
}