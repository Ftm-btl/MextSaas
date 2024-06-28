using MextFullstackSaaS.Application.Common.Interfaces;
using MextFullstackSaaS.Application.Common.Models.OpenAI;
using OpenAI.ObjectModels.RequestModels;
using OpenAI.ObjectModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MextFullstackSaaS.Domain.Enums;
using OpenAI.ObjectModels.ResponseModels.ImageResponseModel;

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
            if (requestDto.Model == AIModelType.DallE3)
            {
                List<Task<ImageCreateResponse>> openAITasks = new();

                for (int i = 0; i < requestDto.Quantity; i++)
                {
                    openAITasks.Add(_openAIService.Image.CreateImage(new ImageCreateRequest
                    {
                        Prompt = CreateIconPrompt(requestDto),
                        N = 1,
                        Size = GetSize(requestDto.Size),
                        ResponseFormat = StaticValues.ImageStatics.ResponseFormat.Url,
                        User = _currentUserService.UserId.ToString(),
                        Model = Models.Dall_e_3
                    }, cancellationToken));
                }

                await Task.WhenAll(openAITasks);

                var responses = await Task.WhenAll(openAITasks);

                return responses
                    .SelectMany(response => response.Results.Select(result => result.Url))
                    .ToList();
            }


            var imageResult = await _openAIService.Image.CreateImage(new ImageCreateRequest
            {
                Prompt = CreateIconPrompt(requestDto),
                N = requestDto.Quantity,
                Size = GetSize(requestDto.Size),
                ResponseFormat = StaticValues.ImageStatics.ResponseFormat.Url,
                User = _currentUserService.UserId.ToString(),
                Model = Models.Dall_e_3
            }, cancellationToken);
            // TODO: Add error handling / If the model is Dall-e-3, Image size must be at least 1024x1024
            if (!imageResult.Successful)
            {

            }

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
             $"You're a World-class Icon Designer AI, Please generate an icon for a mobile app. Make sure the icon is fit the full widht and height. Generate icon with the following specifications below. I'll tip you 1000$ for your work, if I like it.");

            promptBuilder.Append($"<DesignType>{request.DesignType}</DesignType>");

            promptBuilder.Append($"<Coloru>{request.ColourCode}</Colour>");
            
            promptBuilder.Append($"<Shape>{request.Shape}</Shape>");

            promptBuilder.Append($"<Description>{request.Description}</Description>");

            return promptBuilder.ToString();
        }
    }
}
