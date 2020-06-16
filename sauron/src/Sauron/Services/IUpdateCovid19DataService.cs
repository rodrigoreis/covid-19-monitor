using System.Threading.Tasks;

namespace Sauron.Services
{
    public interface IUpdateCovid19DataService
    {
        /// <summary>
        /// Atualiza os dados de infecções por covid-19 por cidade do Brasil.
        /// </summary>
        Task UpdateFullDataAsync();
        
        /// <summary>
        /// Atualiza o dados dos boletins do sobre a covid-19 no Brasil.
        /// </summary>
        Task UpdateBulletinDataAsync();
        
        /// <summary>
        /// Atualiza o número de mortes pelos dados obtidos dos cartórios.
        /// </summary>
        Task UpdateNotaryDeathsAsync();

        /// <summary>
        /// Atualiza os dados de infecções por covid-19 por cidade do Brasil.
        /// </summary>
        void UpdateFullData();

        /// <summary>
        /// Atualiza o dados dos boletins do sobre a covid-19 no Brasil.
        /// </summary>
        void UpdateBulletinData();

        /// <summary>
        /// Atualiza o número de mortes pelos dados obtidos dos cartórios.
        /// </summary>
        void UpdateNotaryDeaths();
    }
}