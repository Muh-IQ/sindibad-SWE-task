using Sindibad.Application.Common.Results;

namespace Sindibad.Api.Extensions;

public static class ErrorTypeExtensions
{
    public static int ToHttpStatus(this ErrorType type)
        => type == ErrorType.None ? 200 : (int)type;
}
