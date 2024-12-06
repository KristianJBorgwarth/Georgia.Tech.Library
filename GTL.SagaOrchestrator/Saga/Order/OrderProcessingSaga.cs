using GTL.Messaging.RabbitMq.Messages.OrderMessages;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace GTL.SagaOrchestrator.Saga.Order;

public class OrderProcessingSaga : MassTransitStateMachine<OrderProcessingSagaState>
{
    public Event<ProcessOrderRequestMessage> ProcessOrderRequestMessageReceived { get; private set; }
    public Event<Fault<PaymentRequestMessage>> PaymentRequestFailed { get; private set; }

    public State ProcessingOrderState { get; private set; }

    public OrderProcessingSaga(ILogger<OrderProcessingSaga> logger)
    {
        InstanceState(x => x.CurrentState);

        Event(() => ProcessOrderRequestMessageReceived, x => x.CorrelateById(m => m.Message.CorrelationId));

        Initially(
            When(ProcessOrderRequestMessageReceived)
                .ThenAsync(async context =>
                {
                    context.Saga.CustomerId = context.Message.CustomerId;
                    context.Saga.OrderId = context.Message.OrderId;
                    context.Saga.BookIds = context.Message.BookIds;
                    context.Saga.Price = context.Message.Price;
                    context.Saga.CardNumber = context.Message.CardNumber;
                    context.Saga.ExpirationDate = context.Message.ExpirationDate;
                    context.Saga.CVC = context.Message.CVC;

                    await context.Publish(new PaymentRequestMessage(context.Saga.Price, context.Saga.CardNumber, context.Saga.ExpirationDate, context.Saga.CVC,
                        context.Saga.CorrelationId, context.Message.Id));
                })
                .TransitionTo(ProcessingOrderState));

        During(ProcessingOrderState,
            When(PaymentRequestFailed)
                .ThenAsync(async context => { }));


    }
}