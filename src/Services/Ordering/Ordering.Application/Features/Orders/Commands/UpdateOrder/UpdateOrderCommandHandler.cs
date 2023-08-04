using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Contracts.Persistence;
using Ordering.Application.Exceptions;
using Ordering.Application.Features.Orders.Commands.UpdateOrder;
using Ordering.Domain.Entities;

namespace Ordering.Application.Features.Orders.UpdateOrder
{
    public class UpdateOrderCommandHandler: IRequestHandler<UpdateOrderCommand,Unit>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        //private readonly IEmailService _emailService;
        //private readonly EmailSettings _emailSettings;
        private readonly ILogger<UpdateOrderCommandHandler> _logger;

        public UpdateOrderCommandHandler(IOrderRepository orderRepository, IMapper mapper, ILogger<UpdateOrderCommandHandler> logger)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Unit> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var orderToUpdate = await _orderRepository.GetByIdAsync(request.Id);

            if (orderToUpdate == null)
            {
                throw new NotFoundException(nameof(Order), request.Id);
            }

            _mapper.Map(request, orderToUpdate, typeof(UpdateOrderCommand), typeof(Order));

            await _orderRepository.UpdateAsync(orderToUpdate);

            //await SendMail(orderToUpdate);
            _logger.LogInformation($"Order {orderToUpdate.Id} is successfully updated.");

            return Unit.Value;
        }


        //private async Task SendMail(Order order)
        //{
        //    var email = new Email
        //    {
        //        To = order.EmailAddress,
        //        Body = $"Order was updated.",
        //        Subject = $"Order {order.Id} was updated."
        //    };

        //    try
        //    {
        //        //await _emailService.SendEmailAsync(email);
        //    }
        //    catch (Exception ex)
        //    { }
        //}
    }
}
