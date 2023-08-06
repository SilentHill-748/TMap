﻿namespace TMap.Exceptions;

internal class MaterialException : Exception
{
    private readonly string _message;

    public MaterialException(string message)
    {
        _message = message;
    }

    public override string Message => _message;
}
