using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace ReportesUdec.Controllers
{
    public class PruebasController : Controller
    {
        // GET: Pruebas
        public ActionResult Pruebas()
        {
            IWebDriver driver = new FirefoxDriver();
            driver.Navigate().GoToUrl("https://udcreports.azurewebsites.net/Acceso/NuevoReporte");
            var input = driver.FindElement(By.Id("Descripcion"));
            input.SendKeys("Prueba con Selenium");
            IWebElement boton = driver.FindElement(By.Id("crearR"));
            boton.Click();
            driver.Close();
            return View();
        }
    }
}