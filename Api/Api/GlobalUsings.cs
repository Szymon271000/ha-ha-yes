﻿global using Api.Data.Context;
global using Microsoft.EntityFrameworkCore;
global using Newtonsoft.Json;
global using Newtonsoft.Json.Serialization;
global using Serilog;
global using Serilog.Events;
global using System.Diagnostics;
global using Api.Middlewares;
global using Microsoft.OpenApi.Models;
global using System.Reflection;
global using Microsoft.AspNetCore.Authentication.JwtBearer;
global using Microsoft.IdentityModel.Tokens;
global using System.Text;
global using Microsoft.AspNetCore.Mvc;
global using Api.Data.Model;
global using Api.DTOs.SeriesDTOs;
global using AutoMapper;
global using Api.Data.Repository;
global using Api.Data.Repository.Interfaces;
global using Api.DTOs.EpisodesDTOs;
global using Api.DTOs.SeasonsDTOs;