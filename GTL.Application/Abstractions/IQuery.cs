using GTL.Domain.Common;
using MediatR;

namespace GTL.Application.Abstractions;

public interface IQuery<TResponse> : IRequest<Result<TResponse>> { }