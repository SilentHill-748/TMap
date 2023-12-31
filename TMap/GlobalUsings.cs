﻿global using System;
global using System.Collections;
global using System.Collections.Generic;
global using System.Collections.ObjectModel;
global using System.Collections.Specialized;
global using System.ComponentModel;
global using System.Diagnostics;
global using System.Diagnostics.CodeAnalysis;
global using System.Globalization;
global using System.IO;
global using System.Linq;
global using System.Reflection;
global using System.Runtime.CompilerServices;
global using System.Threading.Tasks;
global using System.Windows;
global using System.Windows.Data;
global using System.Windows.Input;
global using System.Windows.Media;
global using System.Windows.Media.Imaging;
global using System.Windows.Threading;

global using AutoMapper;

global using CommunityToolkit.Mvvm.Messaging;
global using CommunityToolkit.Mvvm.Messaging.Messages;

global using FluentValidation;

global using Microsoft.EntityFrameworkCore;

global using TMap.Application.Exceptions;
global using TMap.Application.Services.Material;
global using TMap.Configurations.DI;
global using TMap.Configurations.DI.Extentions;
global using TMap.Configurations.Extentions;
global using TMap.Domain.Abstractions.Repositories;
global using TMap.Domain.Abstractions.Services.Material;
global using TMap.Domain.DTO.Material;
global using TMap.Domain.Entities.Material;
global using TMap.Domain.Mapper;
global using TMap.MapperProfiles;
global using TMap.MVVM.Messages.Navigation;
global using TMap.MVVM.Messages.Settings;
global using TMap.MVVM.Messages.Settings.Map;
global using TMap.MVVM.Messages.Settings.Pipeline;
global using TMap.MVVM.Messages.Settings.PipelineChannel;
global using TMap.MVVM.Messages.Settings.Road;
global using TMap.MVVM.Model;
global using TMap.MVVM.Model.Drawing;
global using TMap.MVVM.Model.Map;
global using TMap.MathModel;
global using TMap.MVVM.Model.Navigation;
global using TMap.MVVM.Model.Pipeline;
global using TMap.MVVM.Model.Settings;
global using TMap.MVVM.Stores;
global using TMap.MVVM.Validation.Validators;
global using TMap.MVVM.View.Windows;
global using TMap.MVVM.ViewModel;
global using TMap.MVVM.ViewModel.Map;
global using TMap.MVVM.ViewModel.Settings.Map;
global using TMap.MVVM.ViewModel.Settings.Pipeline;
global using TMap.MVVM.ViewModel.Settings.PipelineChannel;
global using TMap.MVVM.ViewModel.Settings.Road;
global using TMap.Persistence;
global using TMap.Persistence.Repositories;
global using TMap.Services;
global using TMap.Services.Drawing;
global using TMap.WPFCore.Commands.Base;
global using TMap.WPFCore.Commands.Map;
global using TMap.WPFCore.Commands.Modeling;
global using TMap.WPFCore.Commands.Navigation;
global using TMap.WPFCore.Commands.Settings;
global using TMap.WPFCore.Commands.Settings.Map;
global using TMap.WPFCore.Commands.Settings.Pipeline;
global using TMap.WPFCore.Commands.Settings.PipelineChannel;
global using TMap.WPFCore.Commands.Settings.Road;
