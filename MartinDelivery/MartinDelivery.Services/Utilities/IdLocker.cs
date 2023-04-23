using System;

namespace MartinDelivery.Services.Utilities;

public class IdLocker
{
    private static object _orderIdLock = new object();
    private static Dictionary<int, object> _orderIdLocks = new Dictionary<int, object>();

    public static object OrderIdLock(int orderId)
    {
        lock (_orderIdLock)
        {
            if (_orderIdLocks.ContainsKey(orderId))
            {
                return _orderIdLocks[orderId];
            }
            var result = new object();
            _orderIdLocks[orderId] = result;
            return result;
        }
    }
}
