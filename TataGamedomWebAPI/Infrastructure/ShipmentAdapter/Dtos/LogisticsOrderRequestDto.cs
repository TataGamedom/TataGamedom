﻿namespace TataGamedomWebAPI.Infrastructure.ShipmentAdapter.Dtos;

public class LogisticsOrderRequestDto
{
    public string MerchantID { get; set; } = "2000132";
    public string MerchantTradeNo { get; set; } = string.Empty;
    public string MerchantTradeDate { get; set; } = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
    public string LogisticsType { get; set; } = "CVS";
    public string LogisticsSubType { get; set; } = "UNIMART"; //7-11
    public decimal GoodsAmount { get; set; }
    public string SenderName { get; set; } = "Tata";
    public string SenderCellPhone { get; set; } = "0916224867";
    public required string ReceiverName { get; set; }
    public string ReceiverCellPhone { get; set; } = "0916224867";
    public string ReceiverEmail { get; set; } = "tatagamedom@gmail.com";

    public string ServerReplyURL { get; set; } = "https://localhost:3000/Orders";

    public string ReceiverStoreID { get; set; } = "131386";  //7-11

}