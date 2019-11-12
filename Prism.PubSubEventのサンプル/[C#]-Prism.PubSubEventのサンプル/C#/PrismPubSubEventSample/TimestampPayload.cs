using System;

namespace PrismPubSubEventSample
{
    /// <summary>
    /// EventAggregatorで発行するイベントで受け渡すオブジェクト
    /// </summary>
    public class TimestampPayload
    {
        public DateTimeOffset Timestamp { get; set; }
        public string Message { get; set; }
    }
}
