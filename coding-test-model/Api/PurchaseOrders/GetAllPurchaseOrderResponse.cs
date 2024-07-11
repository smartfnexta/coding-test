using coding_test_model.Entities;
using System.Collections.Generic;

namespace coding_test_model.Api.PurchaseOrders
{
    public class GetAllPurchaseOrderResponse
    {
        public IEnumerable<PurchaseOrder> PurchaseOrders { get; set; }
    }
}
