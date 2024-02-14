using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ZaverecnyProjektForman2.Models;

namespace ZaverecnyProjektForman2.Controllers
{
    /// <summary>
    /// Kontroler pro hlavní stránku aplikace.
    /// </summary>
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        /// <summary>
        /// Inicializuje novou instanci <see cref="HomeController"/> tøídy.
        /// </summary>
        /// <param name="logger">Logger pro záznam událostí.</param>
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        /// <summary>
        /// Zobrazuje hlavní stránku.
        /// </summary>
        /// <returns>View hlavní stránky.</returns>
        public IActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// Zobrazuje stránku s informacemi o ochranì soukromí.
        /// </summary>
        /// <returns>View stránky Ochrany soukromí.</returns>
        public IActionResult Privacy()
        {
            return View();
        }
        /// <summary>
        /// Zobrazuje stránku chyby.
        /// </summary>
        /// <returns>View stránky chyby.</returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        /// <summary>
        /// Ukázka akce pro zobrazení vlastního obsahu.
        /// </summary>
        /// <returns>View s vlastním obsahem.</returns>
        public IActionResult ActionToShowMessage()
        {
            return View();
        }
        /// <summary>
        /// Zobrazuje stránku s informacemi o aplikaci.
        /// </summary>
        /// <returns>View stránky O aplikaci.</returns>
        public IActionResult About()
        {
            return View();
        }
    }
}
