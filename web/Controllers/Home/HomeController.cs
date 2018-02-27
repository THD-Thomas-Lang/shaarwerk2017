using Microsoft.AspNetCore.Mvc;

namespace web.Controllers.Home
{
    /// <inheritdoc />
    /// <summary>
    /// Home Controller.
    /// Handles the standard empty / routes.
    /// </summary>
    public sealed class HomeController : Controller
    {
        /// <summary>
        /// Renderes an index view.
        /// When /index is requested.
        /// Overridden method to provide some additional information like a message and a result flag.
        /// </summary>
        /// <param name="message">A given message to display.</param>
        /// <param name="result">A given flag to indicate success or failure.</param>
        /// <returns>IActionResult</returns>
        public IActionResult Index(string message = "", bool? result = null)
        {
            if (string.IsNullOrEmpty(message) || result == null) return View();
            ViewData["Message"] = message;
            ViewData["Success"] = result;
            return View();
        }
    }
}
