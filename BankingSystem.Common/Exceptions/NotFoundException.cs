﻿namespace BankingSystem.Common.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message) { }
    public NotFoundException(string entityName, object key) : base($"{entityName} з ключем {key} не знайдено") { }
}
