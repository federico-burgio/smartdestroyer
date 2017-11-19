using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace smartdestroyer.Controllers
{
    [Route("api/[controller]")]
    public class SmartDestroyerController : Controller
    {
        // GET api/values
        [HttpGet]
        public async Task<string> Get()
        {
            try
            {
                HttpClient radarClient = new HttpClient();
                Console.WriteLine("Calling Radar service @ http://radar:4000/api/radar");
                var response = await radarClient.GetAsync("http://radar:4000/api/radar");

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    try
                    {
                        string message = await response.Content.ReadAsStringAsync();
                        List<EnemyCoordinates> foundEnemies = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<EnemyCoordinates>>(message).ToList();

                        if (foundEnemies.Any() == false)
                        {
                            return "Sky is clear :-)";
                        }
                        else
                        {
                            int hitCount = 0;
                            foreach (EnemyCoordinates enemyCoordinates in foundEnemies)
                            {
                                bool hit = await TryBlast(enemyCoordinates);
                                if (hit)
                                    hitCount++;
                            }

                            return $"Found {foundEnemies.Count} enemies, {hitCount} was blasted successfully.";
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                        return "Unable to get coordinates from Radar";
                    }
                }
                else
                {
                    return "Radar is not working";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return "Radar is not working";
            }
        }

        private async Task<bool> TryBlast(EnemyCoordinates enemyCoordinates)
        {
            HttpClient blastCannonClient = new HttpClient();
            StringContent content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(enemyCoordinates), Encoding.UTF8, "application/json");
            
            Console.WriteLine("Calling Blast Cannon service @ http://blastcannon:5000/api/blast");
            var response = await blastCannonClient.PostAsync("http://blastcannon:5000/api/blast", content);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                try
                {
                    string message = await response.Content.ReadAsStringAsync();

                    return string.IsNullOrWhiteSpace(message) == false
                        && message.Contains("HIT");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
