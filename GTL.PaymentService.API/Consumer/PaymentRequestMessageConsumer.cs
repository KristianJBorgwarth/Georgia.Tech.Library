using System;
using System.Threading.Tasks;
using GTL.Messaging.RabbitMq.Messages;
using GTL.Messaging.RabbitMq.Messages.OrderMessages;
using GTL.Messaging.RabbitMq.Producer;
using MassTransit;
using Microsoft.Extensions.Logging;
using PSU_PaymentGateway.Models;
using Webshop.Payment.Api.Controllers;
using Webshop.Payment.Api.Models;
using Webshop.Payment.Api.Repository;
using Webshop.Payment.Api.Services;

namespace Webshop.Payment.Api.Consumer;

public class PaymentRequestMessageConsumer : IConsumer<PaymentRequestMessage>
{
    private readonly IMemoryRepository _transactionRepository;
    private readonly ILogger<PaymentController> _logger;
    private readonly IProducer<OperationSucceededMessage> _producer;
    public PaymentRequestMessageConsumer(IMemoryRepository transactionRepository, IThrottleService throttleService, ILogger<PaymentController> logger, IProducer<OperationSucceededMessage> producer)
    {
        _transactionRepository = transactionRepository;
        _logger = logger;
        _producer = producer;
    }
    public async Task Consume(ConsumeContext<PaymentRequestMessage> context)
    {
        try
        {
            var message = context.Message;

            var paymentResult = Models.Payment.Create(message.CardNumber, message.ExpirationDate, message.CVC);
            if (paymentResult.IsSuccess)
            {
                var transactionResult = Transaction.Create(message.Price, paymentResult.Value);
                if (transactionResult.IsSuccess)
                {
                    var result = _transactionRepository.AddTransaction(transactionResult.Value);
                    if (result.IsFailure)
                    {
                        throw new Exception(result.Error);
                    }
                }
            }
            else
            {
                _logger.LogWarning("Unable to create new Payment object with the following error: {error}", new { error = paymentResult.Error });
                throw new Exception(paymentResult.Error);
            }

            await _producer.PublishMessageAsync(new OperationSucceededMessage(OperationType.PaymentRequest, message.CorrelationId, message.Id));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while processing the payment request message");
            throw;
        }
    }
}