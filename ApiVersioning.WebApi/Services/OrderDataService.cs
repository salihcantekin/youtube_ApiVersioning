using System;
using System.Collections.Generic;
using ApiVersioning.WebApi.Models;

namespace ApiVersioning.WebApi.Services
{
    public class OrderDataService
    {
        public static List<OrderViewModel> GetOrders()
        {
            var resultList = new List<OrderViewModel>()
            {
              new OrderViewModel() 
              {
                  Id = 1,
                  ProductId = 1,
                  Quantity = 4,
                  Price = CalculatePrice()
              },
              new OrderViewModel()
              {
                  Id = 1,
                  ProductId = 45,
                  Quantity = 8,
                  Price = CalculatePrice()
              },
              new OrderViewModel()
              {
                  Id = 3,
                  ProductId = 75,
                  Quantity = 21,
                  Price = CalculatePrice()
              },
            };

            return resultList;
        }

        public static List<OrderViewModelv2> GetOrdersv2()
        {
            var resultList = new List<OrderViewModelv2>()
            {
              new OrderViewModelv2()
              {
                  Id = 1,
                  ProductId = 1,
                  Quantity = 4,
                  Price = CalculatePrice()
              },
              new OrderViewModelv2()
              {
                  Id = 1,
                  ProductId = 45,
                  Quantity = 8,
                  Price = CalculatePrice()
              },
              new OrderViewModelv2()
              {
                  Id = 3,
                  ProductId = 75,
                  Quantity = 21,
                  Price = CalculatePrice()
              },
            };

            resultList.
                ForEach(i => i.DiscountedPrice = CalculatePriceWithDiscount(i.Price, i.Quantity));

            return resultList;
        }


        private static double CalculatePrice()
        {
            return new Random().Next(1, 1000); // Get price from db
        }

        private static double CalculatePriceWithDiscount(double price, int quantity)
        {
            return quantity switch
            {
                > 10 => price * 0.5,
                > 5 => price * 0.75,
                _ => price
            };
        }
    }
}