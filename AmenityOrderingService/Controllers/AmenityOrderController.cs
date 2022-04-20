using AmenityOrderingService.Data;
using AmenityOrderingService.Dtos;
using AmenityOrderingService.Models;
using AmenityOrderingService.Services;
using AmenityOrderingService.SyncService;
using AutoMapper;
using EventBus.Messages.Event;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace AmenityOrderingService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AmenityOrderController: ControllerBase
    {
        private readonly IAmenityOrderRepository _amenityOrderRepository;
        private readonly GrpcCustomerService _grpcCustomerService;
        private readonly IMapper _mapper;
        private readonly IBus _bus;
        private readonly AmenityOrderingHelper helper;

        public AmenityOrderController(IAmenityOrderRepository amenityOrderRepository, GrpcCustomerService grpcCustomerService, IMapper mapper, IBus bus, AmenityOrderingHelper helper)
        {
            _amenityOrderRepository = amenityOrderRepository;
            _grpcCustomerService = grpcCustomerService;
            _mapper = mapper;
            _bus = bus;
            this.helper = helper;
        }

        [HttpGet("{id}", Name = "GetAmenityOrderById")]
        public ActionResult<AmenityOrderReadDto> GetAmenityOrderById(int id)
        {
            var amenityOrder = _amenityOrderRepository.GetAmenityOrderById(id);
            if (amenityOrder != null)
            {
                return Ok(_mapper.Map<AmenityOrderReadDto>(amenityOrder));
            }
            return NotFound();
        }

        [HttpGet]
        public ActionResult<IEnumerable<AmenityOrderReadDto>> GetAllAmenityOrders()
        {
            Console.WriteLine("---- Getting all Amenities ----");

            var amenityOrders = _amenityOrderRepository.GetAllAmenityOrders();
            return Ok(_mapper.Map<IEnumerable<AmenityOrderReadDto>>(amenityOrders));
        }

        [HttpPost]
        public async Task<ActionResult<AmenityOrderReadDto>> AddAmenityOrder(AmenityOrderAddDto amenityOrderAddDto)
        {
            var amenityOrder = _mapper.Map<AmenityOrder>(amenityOrderAddDto);

            var response = await _grpcCustomerService.GetCustomerById(amenityOrderAddDto.CustomerId);
            try
            {
                var requestClient = _bus.CreateRequestClient<AmenityOrderManagerRequestEvent>(RequestTimeout.After(m: 30));
                var amenityOrderEventResponse = await requestClient.GetResponse<AmenityOrderManagerResponseEvent>(amenityOrderAddDto);
                if (amenityOrderEventResponse.ExpirationTime < DateTime.Now)
                {
                    if (amenityOrderEventResponse.Message.IsAvailable && amenityOrderEventResponse.Message.IsAccepted)
                    {
                        _amenityOrderRepository.AddAmenityOrder(amenityOrder);
                        _amenityOrderRepository.SaveChanges();
                        
                        var amenityOrderResponse = new AmenityOrderResponse
                        {
                            OrderId = amenityOrder.Id,
                            ProviderName = amenityOrderEventResponse.Message.ProviderName,
                            CustomerId = amenityOrderEventResponse.Message.CustomerId,
                            IsAccepted = amenityOrderEventResponse.Message.IsAccepted,
                            IsAvailable = amenityOrderEventResponse.Message.IsAvailable,
                            Amenity = amenityOrderEventResponse.Message.Amenity,
                            AmenityProviderId = amenityOrderEventResponse.Message.AmenityProviderId
                        };
                        await helper.SendOrderContextToNotificationService(amenityOrderResponse);
                        var amenityOrderReadDto = _mapper.Map<AmenityOrderReadDto>(amenityOrder);
                        return CreatedAtRoute(nameof(GetAmenityOrderById), new { Id = amenityOrderReadDto.Id }, amenityOrderReadDto);
                    }
                    else
                    {
                        return NotFound("Could not place the order!");
                    }
                }
                else
                {
                    throw new TimeoutException();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            
            //if(response.Customer != null)
            //{
            //    //var validCustomer = _mapper.Map<Customer>(response.Customer);
            //    _amenityOrderRepository.AddAmenityOrder(amenityOrder);
            //    _amenityOrderRepository.SaveChanges();

            //    var amenityOrderReadDto = _mapper.Map<AmenityOrderReadDto>(amenityOrder);
            //    return CreatedAtRoute(nameof(GetAmenityOrderById), new { Id = amenityOrderReadDto.Id }, amenityOrderReadDto);
            //}
            
            throw new HttpRequestException("Invalid customer!");
        }
    }
}
