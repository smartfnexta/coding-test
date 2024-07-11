using coding_test_model.Entities;
using System.Collections.Generic;

namespace coding_test_model.Api.Inventories
{
    public class GetAllInventoriesResponse
    {
        public IEnumerable<Inventory> Inventories { get; set; }
    }
}
