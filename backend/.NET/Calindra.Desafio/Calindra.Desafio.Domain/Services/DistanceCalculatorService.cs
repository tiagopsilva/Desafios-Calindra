using Calindra.Desafio.Domain.Commands.Inputs;
using Calindra.Desafio.Domain.Exceptions;
using Calindra.Desafio.Domain.Models.Responses;
using Calindra.Desafio.Domain.Results;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Calindra.Desafio.Domain.Services
{
    public class DistanceCalculatorService
    {
        private readonly ILogger<DistanceCalculatorService> _logger;
        private readonly IGeolocationApiService _geolocationApiService;

        public DistanceCalculatorService(ILogger<DistanceCalculatorService> logger, IGeolocationApiService geolocationApiService)
        {
            _logger = logger;
            _geolocationApiService = geolocationApiService;
        }

        public async Task<MethodResult> CalculateDistancesFromAddressess(Addresses addresses)
        {
            try
            {
                var addressesInfo = await RequestAddressesInfo(addresses);
                if (addressesInfo.Count < 2)
                    return Result.FailWithData(nameof(addresses), HttpStatusCode.BadRequest, "There must be at least 2 valid addresses");

                var (shorterQuantity, greatherQuantity) = CalculateQuantities(addresses.AddressList.Count);

                foreach (var point in addressesInfo)
                {
                    foreach (var p in addressesInfo.Where(p => p != point))
                        point.Addresses.Add(new GeolocationInfo(p, CalculateEuclideanDistance(point, p)));

                    point.ShorterDistances = point.Addresses.OrderBy(p => p.Distance).Take(shorterQuantity).ToList();
                    point.GreatherDistances = point.Addresses.OrderByDescending(p => p.Distance).Take(greatherQuantity).ToList();
                }

                return Result.Ok(addressesInfo);
            }
            catch (CustomException e)
            {
                _logger.LogError(e, $"Erro ao processar as distâncias! \nMessage: {e.Message}");
                return Result.FailWithData(string.Empty, HttpStatusCode.BadRequest, "Unable to process distances", $"Message: {e.Message}");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Erro ao processar as distâncias");
                return Result.Fail(string.Empty, "Unable to process distances");
            }
        }

        private (int shorterQuantity, int greatherQuantity) CalculateQuantities(int quantity)
        {
            quantity -= 1;
            var shorter = (int)(quantity / 2);
            var greather = (int)(quantity / 2) + (quantity % 2);
            return (shorter, greather);
        }

        private async Task<List<GeolocationReferencesInfo>> RequestAddressesInfo(Addresses command)
        {
            var tasks = command.AddressList.Select(async address => await _geolocationApiService.GetGeolocationFrom(address));
            var results = await Task.WhenAll(tasks);

            var points = results
                .Where(result => result.Status == "OK")
                .SelectMany(result => result.Results)
                .Select(result => new GeolocationReferencesInfo
                {
                    PlaceId = result.PlaceId,
                    Address = result.FormattedAddress,
                    Location = new Location(result.Geometry.Location.Lat, result.Geometry.Location.Lng),
                })
                .ToList();

            return points;
        }

        private static double CalculateEuclideanDistance(GeolocationReferencesInfo pointA, GeolocationReferencesInfo pointB)
        {
            return Math.Sqrt(
                Math.Pow(pointA.Location.Lat - pointB.Location.Lat, 2) +
                Math.Pow(pointA.Location.Lng - pointB.Location.Lng, 2)
            );
        }
    }
}
