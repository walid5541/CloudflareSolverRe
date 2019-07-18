using CloudflareSolverRe.CaptchaProviders;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net.Http;

namespace CloudflareSolverRe.Tests
{
    [TestClass]
    public class WebsiteChallengeTests
    {
        [TestMethod]
        public void SolveWebsiteChallenge_uamhitmehardfun()
        {
            var cf = new CloudflareSolver
            {
                MaxTries = 3,
                ClearanceDelay = 3000
            };

            var httpClientHandler = new HttpClientHandler();
            var httpClient = new HttpClient(httpClientHandler);
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:66.0) Gecko/20100101 Firefox/66.0");

            var uri = new Uri("https://uam.hitmehard.fun/HIT");

            var result = cf.Solve(httpClient, httpClientHandler, uri).Result;

            Assert.IsTrue(result.Success);

            var response = httpClient.GetAsync(uri).Result;
            var html = response.Content.ReadAsStringAsync().Result;

            Assert.AreEqual("Dstat.cc is the best", html);
        }

        [TestMethod]
        public void SolveWebsiteChallenge_uamhitmehardfun_WithAntiCaptcha()
        {
            var cf = new CloudflareSolver(new AntiCaptchaProvider("YOUR_API_KEY"))
            {
                MaxTries = 1
            };

            var httpClientHandler = new HttpClientHandler();
            var httpClient = new HttpClient(httpClientHandler);

            var uri = new Uri("https://uam.hitmehard.fun/HIT");

            var result = cf.Solve(httpClient, httpClientHandler, uri).Result;

            Assert.IsTrue(result.Success);

            var response = httpClient.GetAsync(uri).Result;
            var html = response.Content.ReadAsStringAsync().Result;

            Assert.AreEqual("Dstat.cc is the best", html);
        }

        [TestMethod]
        public void SolveWebsiteChallenge_uamhitmehardfun_With2Captcha()
        {
            var cf = new CloudflareSolver(new TwoCaptchaProvider("YOUR_API_KEY"))
            {
                MaxTries = 1
            };

            var httpClientHandler = new HttpClientHandler();
            var httpClient = new HttpClient(httpClientHandler);

            var uri = new Uri("https://uam.hitmehard.fun/HIT");

            var result = cf.Solve(httpClient, httpClientHandler, uri).Result;

            Assert.IsTrue(result.Success);

            var response = httpClient.GetAsync(uri).Result;
            var html = response.Content.ReadAsStringAsync().Result;

            Assert.AreEqual("Dstat.cc is the best", html);
        }
    }
}
