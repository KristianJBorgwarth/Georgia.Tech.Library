using GTL.Domain.Common;
using MediatR;

namespace GTL.Application.Abstractions;

public interface ICommand : IRequest<Result> { }
public interface ICommand<TResponse> : IRequest<Result<TResponse>> { }