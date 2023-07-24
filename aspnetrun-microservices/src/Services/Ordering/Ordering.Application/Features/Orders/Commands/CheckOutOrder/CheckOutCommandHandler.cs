using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Contracts.Infrastructure;
using Ordering.Application.Contracts.Persistence;
using Ordering.Application.Models;
using Ordering.Domain.Entities;

namespace Ordering.Application.Features.Orders.Commands.CheckOutOrder;

public class CheckOutCommandHandler : IRequestHandler<CheckOutOrderCommand,int>
{
    public CheckOutCommandHandler(IOrderRepository repository, ILogger<CheckOutOrderCommand> logger, IEmailService? emailService)
    {
        Repository = repository;
        Logger = logger;
        EmailService = emailService;
    }

    public IOrderRepository Repository { get; }
    public ILogger<CheckOutOrderCommand> Logger { get; }
    public IEmailService? EmailService { get; } 
    
    public async Task<int> Handle(CheckOutOrderCommand request, CancellationToken cancellationToken)
    {
        var newOrder = await Repository.AddAsync(request.Order);
        await SendMail(newOrder)!;
        Logger.LogInformation($"Order Added sucesully with Order Id : {newOrder.Id}");
        return newOrder.Id;

    }

    private async Task? SendMail(Order newOrder)
    {
        var email = new Email("dummyEmail@gmail.com", "Order Created", "Order Details Confirmation");
        try
        {
            await EmailService?.SendEmail(email)!;
        }
        catch (Exception e)
        {
         Logger.LogError($"There was error  in sending confimation email with Order Id : {newOrder.Id} \n Here is the exception : {e.Message}");
        }
    }
}