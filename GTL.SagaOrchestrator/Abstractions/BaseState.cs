using MassTransit;

namespace GTL.SagaOrchestrator.Abstractions;

public class BaseState : SagaStateMachineInstance
{
    public Guid CorrelationId { get; set; }
    public string CurrentState { get; set; } = null!;
    public string? Error { get; set; }
    public DateTime Created { get; set; } = DateTime.UtcNow;

    public void SetError(ExceptionInfo[] exceptionInfo)
    {
        Error = string.Join(", ", exceptionInfo.Select(x => x.ExceptionType));
    }
}