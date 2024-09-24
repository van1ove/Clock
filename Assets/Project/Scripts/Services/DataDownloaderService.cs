using System.Threading;
using Cysharp.Threading.Tasks;
using Project.Scripts.Models;
using UnityEngine;
using UnityEngine.Networking;

namespace Project.Scripts.Services
{
    public class DataDownloaderService
    {
        public async UniTask<TimeDto> DownloadDataAsync(string url, CancellationToken token)
        {
            UnityWebRequest webRequest = await UnityWebRequest.Get(url)
                .SendWebRequest()
                .WithCancellation(token);
            
            byte[] data = webRequest.downloadHandler.data;
            string str = System.Text.Encoding.UTF8.GetString(data);
            TimeDto dto = JsonUtility.FromJson<TimeDto>(str);
            return dto;
        }
    }
}