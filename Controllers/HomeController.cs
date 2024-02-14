using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ZaverecnyProjektForman2.Models;

namespace ZaverecnyProjektForman2.Controllers
{
    /// <summary>
    /// Kontroler pro hlavn� str�nku aplikace.
    /// </summary>
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        /// <summary>
        /// Inicializuje novou instanci <see cref="HomeController"/> t��dy.
        /// </summary>
        /// <param name="logger">Logger pro z�znam ud�lost�.</param>
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        /// <summary>
        /// Zobrazuje hlavn� str�nku.
        /// </summary>
        /// <returns>View hlavn� str�nky.</returns>
        public IActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// Zobrazuje str�nku s informacemi o ochran� soukrom�.
        /// </summary>
        /// <returns>View str�nky Ochrany soukrom�.</returns>
        public IActionResult Privacy()
        {
            return View();
        }
        /// <summary>
        /// Zobrazuje str�nku chyby.
        /// </summary>
        /// <returns>View str�nky chyby.</returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        /// <summary>
        /// Uk�zka akce pro zobrazen� vlastn�ho obsahu.
        /// </summary>
        /// <returns>View s vlastn�m obsahem.</returns>
        public IActionResult ActionToShowMessage()
        {
            return View();
        }
        /// <summary>
        /// Zobrazuje str�nku s informacemi o aplikaci.
        /// </summary>
        /// <returns>View str�nky O aplikaci.</returns>
        public IActionResult About()
        {
            return View();
        }
    }
}
