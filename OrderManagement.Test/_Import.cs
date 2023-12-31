﻿global using Microsoft.Extensions.DependencyInjection;
global using OrderManagement.Application.Common.Models;
global using OrderManagement.Application.Features.Orders.Commands.CreateOrder;
global using OrderManagement.Domain.Entities;
global using OrderManagement.Domain.Interfaces;
global using OrderManagement.Test.Common;
global using System.Net.Http;
global using System.Net.Http.Headers;
global using System.Net.Http.Json;
global using System.Text.Json;
global using System.Threading.Tasks;
global using Xunit;
using OrderManagement.Application.Common.Enums;
using OrderManagement.Application.Features.Products.Queries.GetProductById;