using System;
using System.Net;

public class WrongItemTypeException : Exception
{
    public HttpStatusCode Status { get; private set; }

    public WrongItemTypeException(HttpStatusCode status, string msg) : base(msg)
    {
        Status = status;
    }
}