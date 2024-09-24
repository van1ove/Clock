using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Project.Scripts.Controllers;
using Project.Scripts.Models;

namespace Project.Scripts.Services
{
    public class DataRefresherService
    {
        private const int SecondsForDataRefresh = 3600;
        private readonly DataDownloaderService _downloaderService;
        private readonly ClockController _clockController;
        private readonly DigitalClockController _digitalClockController;
        public DataRefresherService(DataDownloaderService dataDownloaderService,
            ClockController clockController,
            DigitalClockController digitalClockController)
        {
            _downloaderService = dataDownloaderService;
            _clockController = clockController;
            _digitalClockController = digitalClockController;
        }
        
        public async UniTaskVoid RefreshDataAsync(string jsonUrl, CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(SecondsForDataRefresh), cancellationToken: token);
                TimeDto dto  = await _downloaderService.DownloadDataAsync(jsonUrl, token);
                TimeModel timeModel = dto.ToTimeModel();

                if (!timeModel.IsEqual(_clockController.ClockModel))
                {
                    _clockController.Init(timeModel);
                }

                if (!timeModel.IsEqual(_digitalClockController.ClockModel))
                {
                    _digitalClockController.Init(timeModel);
                }
            }
        }
    }
}