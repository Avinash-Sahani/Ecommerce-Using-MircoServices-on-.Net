#nullable enable
using Ordering.Domain.Common;

namespace Ordering.Domain.Entities;
//Class Structure can be improved    

public class Order : EntityBase
{
    public string? UserName { get; set; }
    public decimal TotalPrice { get; set; }


    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? EmailAddress { get; set; }
    // BillingAddress
    public string? AddressLine { get; set; }
    public string? Country { get; set; }
    public string? State { get; set; }
    public string? ZipCode { get; set; }

    // Payment
    public string? CardName { get; set; }
    public string? CardNumber { get; set; }
    public string? Expiration { get; set; }
    public string? Cvv { get; set; }
    public int PaymentMethod { get; set; }
}