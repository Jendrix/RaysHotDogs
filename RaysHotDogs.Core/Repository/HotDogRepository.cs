using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RaysHotDogs.Core.Model;

namespace RaysHotDogs.Core.Repository
{
    public class HotDogRepository
    {

        private static List<HotDogGroup> _hotDogGroups = new List<HotDogGroup>();

        private string _url = "http://gillcleerenpluralsight.blob.core.windows.net/files/hotdogs.json";


        public HotDogRepository()
        {
            Task.Run(() => LoadDataAsync(new Uri(_url))).Wait();
        }
        private async Task LoadDataAsync(Uri uri)
        {
            string responseJson = null;


            using (var client = new HttpClient())
            {
                Task<HttpResponseMessage> getResponse = client.GetAsync(uri);
                var response = await getResponse;
                responseJson = await response.Content.ReadAsStringAsync();
                _hotDogGroups = JsonConvert.DeserializeObject<List<HotDogGroup>>(responseJson);
            }
        }

        public List<HotDog> GetAllHotDogs()
        {
            IEnumerable<HotDog> hotDogs =
                from hotDogGroup in _hotDogGroups
                from hotDog in hotDogGroup.HotDogs

                select hotDog;
            return hotDogs.ToList<HotDog>();
        }

        public List<HotDogGroup> GetGroupedHotDogs()
        {
            return _hotDogGroups;
        }

        public List<HotDog> GetHotDogsForGroup(int hotDogGroupId)
        {
            var group = _hotDogGroups.FirstOrDefault(h => h.HotDogGroupId == hotDogGroupId);

            return @group?.HotDogs;
        }

        public List<HotDog> GetFavoriteHotDogs()
        {
            IEnumerable<HotDog> hotDogs =
                from hotDogGroup in _hotDogGroups
                from hotDog in hotDogGroup.HotDogs
                where hotDog.IsFavorite
                select hotDog;

            return hotDogs.ToList<HotDog>();
        }

        public HotDog GetHotDogById(int hotDogId)
        {
            IEnumerable<HotDog> hotDogs =
                from hotDogGroup in _hotDogGroups
                from hotDog in hotDogGroup.HotDogs
                where hotDog.HotDogId == hotDogId
                select hotDog;

            return hotDogs.FirstOrDefault();
        }

    }
}