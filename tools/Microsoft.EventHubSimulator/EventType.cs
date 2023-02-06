﻿using Microsoft.Azure.Models.EventHubs.Events.V1;

namespace Microsoft.EventHubSimulator
{
    public static class EventType
    {
        public static string StreamingDataEventChangedV1 = typeof(StreamingDataChanged).FullName!;
        public static string AasStreamingDataEventChangedV1 = typeof(AasStreamingDataChanged).FullName!;
    }
}
