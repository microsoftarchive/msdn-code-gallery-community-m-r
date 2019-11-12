// 
//  (c) Microsoft Corporation. See LICENSE.TXT file for licensing details
//  
namespace Microsoft.ServiceBus.AccessControlExtensions
{
    public abstract class ServiceBusRight : Right
    {
        public static readonly Right Listen = new ListenRight();
        public static readonly Right Manage = new ManageRight();
        public static readonly Right Send = new SendRight();

        public new static Right CreateGenericRight(string outputClaimType, string outputClaimValue)
        {
            if (outputClaimType == "net.windows.servicebus.action")
            {
                switch (outputClaimValue)
                {
                    case "Listen":
                        return Listen;
                    case "Send":
                        return Send;
                    case "Manage":
                        return Manage;
                }
            }
            return new GenericRight(outputClaimType, outputClaimValue);
        }

        sealed class ListenRight : ServiceBusRight
        {
            public override string ClaimType { get { return "net.windows.servicebus.action"; } }

            public override string ClaimValue { get { return "Listen"; } }
        }

        sealed class ManageRight : ServiceBusRight
        {
            public override string ClaimType { get { return "net.windows.servicebus.action"; } }

            public override string ClaimValue { get { return "Manage"; } }
        }

        sealed class SendRight : ServiceBusRight
        {
            public override string ClaimType { get { return "net.windows.servicebus.action"; } }

            public override string ClaimValue { get { return "Send"; } }
        }
    }
}