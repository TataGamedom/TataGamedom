﻿using Microsoft.AspNetCore.Mvc;
using TataGamedomWebAPI.Infrastructure.PaymentAdapter.LinePaymentAdapter.Dtos.Request.Payment;
using TataGamedomWebAPI.Infrastructure.PaymentAdapter.LinePaymentAdapter.Dtos.Request.PaymentConfirm;
using TataGamedomWebAPI.Infrastructure.PaymentAdapter.LinePaymentAdapter.Dtos.Response.Payment;
using TataGamedomWebAPI.Infrastructure.PaymentAdapter.LinePaymentAdapter.Dtos.Response.PaymentConfirm;
using TataGamedomWebAPI.Infrastructure.PaymentAdapter.LinePaymentAdapter;
using Microsoft.AspNetCore.Cors;
using TataGamedomWebAPI.Infrastructure.PaymentAdapter.LinePaymentAdapter.Dtos.Request.PaymentRefund;
using TataGamedomWebAPI.Infrastructure.PaymentAdapter.LinePaymentAdapter.Dtos.Response.PaymentRefund;
using TataGamedomWebAPI.Infrastructure.Data;
using MediatR;

namespace TataGamedomWebAPI.Controllers;


[ApiController]
[Route("api/[Controller]")]
[EnableCors("AllowAny")]
public class LinePayController : ControllerBase
{
    //to do DI
    private readonly LinePayService _linePayService;
    private readonly AppDbContext _dbContext;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IMediator _mediator;

    public LinePayController(
        AppDbContext dbContext, 
        IHttpContextAccessor httpContextAccessor,
        IMediator mediator)
    {
        this._dbContext = dbContext;
        this._httpContextAccessor = httpContextAccessor;
        this._mediator = mediator;
        _linePayService = new LinePayService(_dbContext, _httpContextAccessor, _mediator);
    }


    [HttpPost]
    [EnableCors("AllowCookie")]
    public async Task<PaymentResponseDto> CreatePayment(PaymentRequestDto dto)
    {

        return await _linePayService.SendPaymentRequest(dto);
    }

    [HttpPost("Create")]
    [EnableCors("AllowCookie")]
    public async Task<PaymentResponseDto> CreatePaymentByAccount(CreatePaymentRequestDto createPaymentRequestDto)
    {
        return await _linePayService.SendPaymentRequestWithCartInfo(createPaymentRequestDto);
    }

    [HttpPost("Confirm")]
    [EnableCors("AllowCookie")]
    public async Task<PaymentConfirmResponseDto> ConfirmPayment([FromQuery] string transactionId,PaymentConfirmDto dto)
    {
        return await _linePayService.ConfirmPayment(transactionId, dto);
    }


    [HttpPost("Refund")]
    [EnableCors("AllowCookie")]
    public async Task<PaymentRefundResponseDto> RefundPayment([FromQuery] string transactionId, PaymentRefundRequestDto dto)
    {
        return await _linePayService.RefundPayment(transactionId,dto);
    }

    [HttpGet("Cancel")]
    [EnableCors("AllowCookie")]
    public async void CancelTransaction([FromQuery] string transactionId)
    {
        _linePayService.TransactionCancel(transactionId);
    }
}
