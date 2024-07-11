using coding_test_model.Api.Inventories;
using coding_test_model.Entities;
using coding_test_qa_api.App.Modules;
using coding_test_qa_api.App.Repositories;

namespace coding_test_qa_api.App.Api.Inventories.Services
{
    /// <summary>
    /// 在庫取得サービスのインターフェース
    /// </summary>
    [DIComponent]
    public interface IGetInventoryService
    {
        /// <summary>
        /// 在庫を取得する
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        GetInventoryResponse Get(long id);

        /// <summary>
        /// 全在庫情報を取得する
        /// </summary>
        /// <returns></returns>
        GetAllInventoriesResponse GetAll();

        /// <summary>
        /// 全拠点での在庫合計を取得する
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        GetGetTotalStockResopnse GetTotalStock(long itemId);
    }

    /// <summary>
    /// 在庫取得サービス
    /// </summary>
    public class GetInventoryService : IGetInventoryService
    {
        private readonly IInventoryHeaderRepository inventoryHeaderRepository;
        private readonly IInventoryDetailRepository inventoryDetailRepository;
        private readonly IItemRepository itemRepository;
        private readonly IAreaRepository areaRepository;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="dbSession"></param>
        /// <param name="inventoryHeaderRepository"></param>
        /// <param name="inventoryDetailRepository"></param>
        /// <param name="itemRepository"></param>
        /// <param name="areaRepository"></param>
        public GetInventoryService(
            IDbSession dbSession,
            IInventoryHeaderRepository inventoryHeaderRepository,
            IInventoryDetailRepository inventoryDetailRepository,
            IItemRepository itemRepository,
            IAreaRepository areaRepository)
        {
            dbSession.Open();
            this.inventoryHeaderRepository = inventoryHeaderRepository;
            this.inventoryDetailRepository = inventoryDetailRepository;
            this.itemRepository = itemRepository;
            this.areaRepository = areaRepository;
        }

        /// <inheritdoc/>
        /// <remarks>
        /// 1. 在庫ヘッダを取得する
        /// 2. 上記1に関連付いた在庫詳細一覧を取得する
        /// 3. 上記1に関連付いた品番を取得する
        /// 4. 上記2に関連付いた拠点名を取得する
        /// 5. 上記2の在庫数を合計する
        /// 6. 上記結果を返す
        /// </remarks>
        public GetInventoryResponse Get(long id)
        {
            var inventoryHeader = this.inventoryHeaderRepository.Select(id);

            if (inventoryHeader == null)
            {
                return null;
            }

            var inventoryDetails = this.inventoryDetailRepository.SelectAny(new Dictionary<string, object>()
            {
                { nameof(InventoryDetail.InventoryHeaderId), inventoryHeader.Id }
            });
            var item = this.itemRepository.Select(inventoryHeader.ItemId);
            var areas = this.areaRepository.SelectAll();
            var areaMap = areas.ToDictionary(x => x.Id);

            var inventoryItems = new List<InventoryItem>();

            foreach (var inventoryDetail in inventoryDetails)
            {
                string areName;

                if (areaMap.TryGetValue(inventoryDetail.AreaId, out var area))
                {
                    areName = area.Name;
                }
                else
                {
                    areName = string.Empty;
                }

                inventoryItems.Add(new InventoryItem()
                {
                    AreaName = areName,
                    Stock = inventoryDetail.StockQuantity
                });
            }

            var totalStock = inventoryItems.Sum(x => x.Stock);

            return new GetInventoryResponse
            { 
                Inventory = new Inventory()
                {
                    Name = item.Name,
                    Items = inventoryItems,
                    TotalStock = totalStock
                }
            };
        }

        /// <inheritdoc/>
        /// <remarks>
        /// 1. 全在庫ヘッダを取得する
        /// 2. 全在庫詳細を取得する
        /// 3. 全品番を取得する
        /// 4. 全拠点を取得する
        /// 5. 上記1に明細、品番、拠点を関連付ける
        /// 6. 上記結果を返す
        /// </remarks>
        public GetAllInventoriesResponse GetAll()
        {
            var inventoryHeaders = this.inventoryHeaderRepository.SelectAll();
            var inventoryDetails = this.inventoryDetailRepository.SelectAll();
            var items = this.itemRepository.SelectAll();
            var itemMap = items.ToDictionary(x => x.Id);
            var areas = this.areaRepository.SelectAll();
            var areaMap = areas.ToDictionary(x => x.Id);

            var inventories = new List<Inventory>();

            foreach (var inventoryHeader in inventoryHeaders)
            {
                string itemName;
                
                if (itemMap.TryGetValue(inventoryHeader.ItemId, out var item))
                {
                    itemName = item.Name;
                }
                else
                {
                    itemName = string.Empty;
                }

                var inventoryItems = new List<InventoryItem>();
                var joinInventoryDetails = inventoryDetails.Where(x => x.InventoryHeaderId == inventoryHeader.Id);

                if (joinInventoryDetails.Any())
                { 
                    foreach (var inventoryDetail in joinInventoryDetails)
                    {
                        string areName;

                        if (areaMap.TryGetValue(inventoryDetail.AreaId, out var area))
                        {
                            areName = area.Name;
                        }
                        else
                        {
                            areName = string.Empty;
                        }

                        inventoryItems.Add(new InventoryItem()
                        {
                            AreaName = areName,
                            Stock = inventoryDetail.StockQuantity,
                        });
                    }
                }

                var totalStock = inventoryItems.Sum(x => x.Stock);

                inventories.Add(new Inventory()
                {
                    Name = itemName,
                    Items = inventoryItems,
                    TotalStock = totalStock
                });
            }

            return new GetAllInventoriesResponse()
            {
                Inventories = inventories,
            };
        }

        /// <inheritdoc/>
        /// <remarks>
        /// 1. 品番IDに一致する在庫ヘッダを取得する
        /// 2. 全在庫詳細を取得する
        /// 3. 上記1に在庫明細を関連付けする
        /// 4. 上記3の在庫を合計する
        /// 5. 上記結果を返す
        /// </remarks>
        public GetGetTotalStockResopnse GetTotalStock(long itemId)
        {
            var inventoryHeaders = this.inventoryHeaderRepository.SelectAny(new Dictionary<string, object>()
            {
                { nameof(InventoryHeader.ItemId), itemId }
            });

            var inventoryHeaderMap = inventoryHeaders.ToDictionary(x => x.Id);
            var inventoryDetails = this.inventoryDetailRepository.SelectAll();
            var area = this.areaRepository.Select(itemId);
            var joinInventoryDetails = inventoryDetails.Where(x => inventoryHeaderMap.ContainsKey(x.Id));
            var totalStock = joinInventoryDetails.Sum(x => x.StockQuantity);

            return new GetGetTotalStockResopnse()
            {
                Name = area != null ? area.Name : string.Empty,
                TotalStock = totalStock
            };
        }
    }
}
