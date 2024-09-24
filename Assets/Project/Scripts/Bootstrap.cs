using Cysharp.Threading.Tasks;
using Project.Scripts.Controllers;
using Project.Scripts.Models;
using Project.Scripts.Services;
using Project.Scripts.Utility;
using UnityEngine;

namespace Project.Scripts
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private ClockController clockController;
        [SerializeField] private DigitalClockController digitalClockController;
        
        private DataDownloaderService _downloaderService;
        private DataRefresherService _refresherService;
        
        private async void Awake()
        { 
            InitServices();
            
            TimeDto dto = await _downloaderService.DownloadDataAsync(WebConst.JsonUrl, this.GetCancellationTokenOnDestroy());
            TimeModel timeModel = dto.ToTimeModel();
            
            clockController.Init(timeModel);
            digitalClockController.Init(timeModel);
            
            _refresherService.RefreshDataAsync(WebConst.JsonUrl, this.GetCancellationTokenOnDestroy());
        }

        private void InitServices()
        {
            _downloaderService = new DataDownloaderService();
            _refresherService = new DataRefresherService(_downloaderService, 
                clockController, digitalClockController);
        }
    }
}
