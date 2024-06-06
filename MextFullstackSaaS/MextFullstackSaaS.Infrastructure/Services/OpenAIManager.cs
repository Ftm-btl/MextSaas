﻿using MextFullstackSaaS.Application.Common.Interfaces;
using MextFullstackSaaS.Application.Common.Models.OpenAI;
using OpenAI.ObjectModels.RequestModels;
using OpenAI.ObjectModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MextFullstackSaaS.Domain.Enums;

namespace MextFullstackSaaS.Infrastructure.Services
{
    public class OpenAIManager : IOpenAIService
    {
        private readonly OpenAI.Interfaces.IOpenAIService _openAIService;
        private readonly  ICurrentUserService _currentUserService;
        public OpenAIManager(OpenAI.Interfaces.IOpenAIService openAIService, ICurrentUserService currentUserService)
        {
            _openAIService = openAIService;
            _currentUserService = currentUserService;
        }

        public async Task<List<string>> DAllECreateIconAsync(DallECreateIconRequestDto requestDto, CancellationToken cancellationToken)
        {
            var imageResult = await _openAIService.Image.CreateImage(new ImageCreateRequest
            {
                Prompt = CreateIconPrompt(requestDto),
                N = requestDto.Model==AIModelType.DallE3 ? 3 : requestDto.Quantity,
                Size = GetSize(requestDto.Size),
                ResponseFormat = StaticValues.ImageStatics.ResponseFormat.Url,
                User = _currentUserService.UserId.ToString(),
                Model=Models.Dall_e_3
            },cancellationToken);
            // TODO: Add error handling / If the model is Dall-e-3, Image size must be at least 1024*1024
            return imageResult
                .Results
                .Select(x => x.Url)
                .ToList();
        }

        private string GetSize(IconSize size)
        {
            return size switch
            {
                IconSize.Small => StaticValues.ImageStatics.Size.Size256,
                IconSize.Medium => StaticValues.ImageStatics.Size.Size512,
                IconSize.Large => StaticValues.ImageStatics.Size.Size1024,
                _ => StaticValues.ImageStatics.Size.Size256
            };
        }
        private string CreateIconPrompt(DallECreateIconRequestDto request)
        {
            var promptBuilder = new StringBuilder();

            promptBuilder.Append(
                $"You're a World-class Icon Designer AI who is working on Mobile Application Icons. Generate icon with the following specifications: ");

            promptBuilder.Append(
                $"Design Type: {request.DesignType}, Colour Code (Hex Code): {request.ColourCode}, Shape: {request.Shape}, Description:{request.Description} ");

            promptBuilder.Append(
                $"I'll tip you 1000$ for your work, if I like it.");

            return promptBuilder.ToString();
        }
    }
}
