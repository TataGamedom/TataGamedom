﻿using Dapper;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Office2010.Excel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Linq;
using System.Web;
using TataGamedom.Controllers;
using TataGamedom.Models.Dtos.InventoryItems;
using TataGamedom.Models.EFModels;
using TataGamedom.Models.Interfaces;
using TataGamedom.Models.Services;
using TataGamedom.Models.ViewModels.InventoryItems;

namespace TataGamedom.Models.Infra.DapperRepositories
{
    public class InventoryRepository : IInventoryRepository
    {
        private string Connstr => System.Configuration.ConfigurationManager.ConnectionStrings["AppDbContext"].ToString();

        public IEnumerable<InventoryVM> Search()
        {
            using (var connection = new SqlConnection(Connstr)) 
            {
                string sql = @"SELECT 
P.IsVirtual AS ProductIsVirtual, G.ChiName AS GameName, G.GameCoverImg AS GameCoverImage, P.[Index] AS ProductIndex, IIT.ProductId,   

P.Id,

(SELECT COUNT(*) FROM OrderItems AS OI RIGHT JOIN InventoryItems AS II ON OI.InventoryItemId = II.Id WHERE II.ProductId = P.Id AND OI.ProductId IS NULL) 
AS [Count],

(SELECT SUM(II.Cost)FROM InventoryItems AS II LEFT JOIN OrderItems AS OI ON II.Id = OI.InventoryItemId WHERE II.ProductId = P.Id AND OI.InventoryItemId IS NULL)
AS Total

FROM InventoryItems AS IIT
RIGHT JOIN Products AS P ON IIT.ProductId = P.Id
RIGHT JOIN Games AS G ON P.GameId = G.Id 
GROUP BY IIT.ProductId, P.IsVirtual, G.ChiName, G.GameCoverImg, P.[Index], P.Id
ORDER BY G.ChiName, P.[Index]
";
                return connection.Query<InventoryVM>(sql);
            }
        }

        public IEnumerable<InventoryItemVM> Info(int? productId, InventoryCriteria criteria)
        {
            using (var connection = new SqlConnection(Connstr))
            {
				string sql = GetSql(productId, criteria);       
                return connection.Query<InventoryItemVM>(sql, new { ProductId = productId, Index = criteria.Index });
            }
        }

        private string GetSql(int? productId, InventoryCriteria criteria)
        {
			if (criteria.Index == null && criteria.SalesStatus == 1) 
			{
				return @"
SELECT IIT.[Index] AS SKU, IIT.Cost AS Cost, IIT.GameKey AS GameKey, SIS.[Index] AS StockInSheetIndex, G.ChiName AS GameName
FROM InventoryItems AS IIT
LEFT JOIN OrderItems AS OI ON OI.InventoryItemId = IIT.Id
JOIN StockInSheets AS SIS ON IIT.StockInSheetId = SIS.Id
JOIN Products AS P ON IIT.ProductId = P.Id
JOIN Games AS G ON P.GameId = G.Id
WHERE IIT.ProductId = @productId
ORDER BY SKU ";
            }
            if (criteria.Index == null && criteria.SalesStatus == 2)
            {
                return @"
SELECT IIT.[Index] AS SKU, IIT.Cost AS Cost, IIT.GameKey AS GameKey, SIS.[Index] AS StockInSheetIndex, G.ChiName AS GameName
FROM InventoryItems AS IIT
LEFT JOIN OrderItems AS OI ON OI.InventoryItemId = IIT.Id
JOIN StockInSheets AS SIS ON IIT.StockInSheetId = SIS.Id
JOIN Products AS P ON IIT.ProductId = P.Id
JOIN Games AS G ON P.GameId = G.Id
WHERE IIT.ProductId = @productId AND OI.Id IS NULL
ORDER BY SKU ";
            }
            if (criteria.Index == null && criteria.SalesStatus == 3)
            {
                return @"
SELECT IIT.[Index] AS SKU, IIT.Cost AS Cost, IIT.GameKey AS GameKey, SIS.[Index] AS StockInSheetIndex, G.ChiName AS GameName
FROM InventoryItems AS IIT
LEFT JOIN OrderItems AS OI ON OI.InventoryItemId = IIT.Id
JOIN StockInSheets AS SIS ON IIT.StockInSheetId = SIS.Id
JOIN Products AS P ON IIT.ProductId = P.Id
JOIN Games AS G ON P.GameId = G.Id
WHERE IIT.ProductId = @productId AND OI.Id IS NOT NULL
ORDER BY SKU ";
            }
            if (criteria.Index != null && criteria.SalesStatus == null)
            {
                return @"
SELECT IIT.[Index] AS SKU, IIT.Cost AS Cost, IIT.GameKey AS GameKey, SIS.[Index] AS StockInSheetIndex, G.ChiName AS GameName
FROM InventoryItems AS IIT
LEFT JOIN OrderItems AS OI ON OI.InventoryItemId = IIT.Id
JOIN StockInSheets AS SIS ON IIT.StockInSheetId = SIS.Id
JOIN Products AS P ON IIT.ProductId = P.Id
JOIN Games AS G ON P.GameId = G.Id
WHERE IIT.ProductId = @productId
AND IIT.[Index] LIKE '%' + @Index + '%'
ORDER BY SKU ";
            }

            if (criteria.Index != null && criteria.SalesStatus == 1)
            {
                return @"
SELECT IIT.[Index] AS SKU, IIT.Cost AS Cost, IIT.GameKey AS GameKey, SIS.[Index] AS StockInSheetIndex, G.ChiName AS GameName
FROM InventoryItems AS IIT
LEFT JOIN OrderItems AS OI ON OI.InventoryItemId = IIT.Id
JOIN StockInSheets AS SIS ON IIT.StockInSheetId = SIS.Id
JOIN Products AS P ON IIT.ProductId = P.Id
JOIN Games AS G ON P.GameId = G.Id
WHERE IIT.ProductId = @productId
AND IIT.[Index] LIKE '%' + @Index + '%'
ORDER BY SKU ";
            }
            if (criteria.Index != null && criteria.SalesStatus == 2)
            {
                return @"
SELECT IIT.[Index] AS SKU, IIT.Cost AS Cost, IIT.GameKey AS GameKey, SIS.[Index] AS StockInSheetIndex, G.ChiName AS GameName
FROM InventoryItems AS IIT
LEFT JOIN OrderItems AS OI ON OI.InventoryItemId = IIT.Id
JOIN StockInSheets AS SIS ON IIT.StockInSheetId = SIS.Id
JOIN Products AS P ON IIT.ProductId = P.Id
JOIN Games AS G ON P.GameId = G.Id
WHERE IIT.ProductId = @productId AND OI.Id IS NULL
AND IIT.[Index] LIKE '%' + @Index + '%'
ORDER BY SKU ";
            }
            if (criteria.Index != null && criteria.SalesStatus == 3)
            {
                return @"
SELECT IIT.[Index] AS SKU, IIT.Cost AS Cost, IIT.GameKey AS GameKey, SIS.[Index] AS StockInSheetIndex, G.ChiName AS GameName
FROM InventoryItems AS IIT
LEFT JOIN OrderItems AS OI ON OI.InventoryItemId = IIT.Id
JOIN StockInSheets AS SIS ON IIT.StockInSheetId = SIS.Id
JOIN Products AS P ON IIT.ProductId = P.Id
JOIN Games AS G ON P.GameId = G.Id
WHERE IIT.ProductId = @productId AND OI.Id IS NOT NULL
AND IIT.[Index] LIKE '%' + @Index + '%'
ORDER BY SKU ";
            }
			return @"
SELECT IIT.[Index] AS SKU, IIT.Cost AS Cost, IIT.GameKey AS GameKey, SIS.[Index] AS StockInSheetIndex, G.ChiName AS GameName
FROM InventoryItems AS IIT
LEFT JOIN OrderItems AS OI ON OI.InventoryItemId = IIT.Id
JOIN StockInSheets AS SIS ON IIT.StockInSheetId = SIS.Id
JOIN Products AS P ON IIT.ProductId = P.Id
JOIN Games AS G ON P.GameId = G.Id
WHERE IIT.ProductId = @productId
ORDER BY SKU ";
        }

        public int GetMaxIdInDb()
		{
			using (var connection = new SqlConnection(Connstr))
			{
				string sql = "SELECT MAX(Id) FROM InventoryItems";
				int maxId = connection.QuerySingle<int>(sql);

				return maxId;
			}
		}
		public string GetProductIndex(int productId)
		{
			using (var connection = new SqlConnection(Connstr))
			{
				string sql = @"
SELECT DISTINCT p.[Index]
FROM Products AS p JOIN InventoryItems AS II ON II.ProductId = P.Id
WHERE II.ProductId = @productId";
				return connection.QuerySingleOrDefault<InventoryItemCreateDto>(sql, new { ProductId = productId }).Index;
			}
		}

		public void Create(InventoryItemCreateDto dto)
		{
			using (var connection = new SqlConnection(Connstr)) 
			{
				string sql = @"
INSERT INTO InventoryItems
([Index],[ProductId],[StockInSheetId] ,[Cost],[GameKey])
VALUES
(@Index, @ProductId, @StockInSheetId, @Cost, @GameKey)";
				connection.Execute(sql, dto);
			}
		}

		public void Update(InventoryItemDto dto)
		{
			using (var connection = new SqlConnection(Connstr)) 
			{
				string sql = @"
UPDATE InventoryItems SET
[ProductId] = @ProductId ,[StockInSheetId] = @StockInSheetId, [Cost]=@Cost, [GameKey]=@GameKey
WHERE [Index] = @Index;";
				connection.ExecuteScalar(sql, dto);
			}
		}

		public InventoryItemDto GetByIndex(string index)
		{
			using (var connection = new SqlConnection(Connstr))
			{
				string sql = @"SELECT * FROM InventoryItems WHERE [Index] = @Index";
				var inventoryItem = connection.QuerySingleOrDefault<InventoryItem>(sql, new { Index = index });
				return inventoryItem.ToDto();
			}
		}

		public int bulkCreate()
		{
			using (var connection = new SqlConnection(Connstr)) 
			{
				using (var trans = connection.BeginTransaction()) 
				{
					string sql = @"";

					var executeResult = connection.Execute(sql,trans);
					trans.Commit();

					return executeResult;
				}
			}
		}

        public InventoryItemDto GetById(int? id)
        {
            using (var connection = new SqlConnection(Connstr))
            {
                string sql = @"SELECT * FROM InventoryItems WHERE [Id] = @Id";
                var inventoryItem = connection.QuerySingleOrDefault<InventoryItem>(sql, new { Id = id });
                return inventoryItem.ToDto();
            }
        }
    }
}