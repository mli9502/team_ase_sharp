﻿// <copyright file="RecommendationController.cs" company="ASE#">
//     Copyright (c) ASE#. All rights reserved.
// </copyright>

namespace Service.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Management.DomainModels;
    using Management.Ports;
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;

    /// <summary>
    /// class <c>RecommendationController</c> provides top 10 travel recommendations 
    /// and provide specific state's travel information
    /// </summary>
    [Route("api/recommendations")]
    [ApiController]
    public class RecommendationController : ControllerBase
    {
        /// <summary>
        /// The port of the recommendation.
        /// </summary>
        private readonly RecommendationPort recommendationPort;

        /// <summary>
        /// Initializes a new instance of the <see cref="RecommendationController"/> class.
        /// </summary>
        /// <param name="recommendationPort">The port of the recommendation.</param>
        public RecommendationController(RecommendationPort recommendationPort) 
        {
            this.recommendationPort = recommendationPort ?? throw new ArgumentNullException(nameof(recommendationPort));
        }

        [HttpGet]
        public async Task<IActionResult> GetTopRecommendations()
        {
            try
            {
                return this.Ok();
            }
            catch (Exception)
            {
                return this.NotFound();
            }
        }

        [HttpGet]
        [Route("country/{countryCode}")]
        public async Task<IActionResult> GetRecommendationByCountryCode([FromRoute] string countryCode)
        {
            try
            {
                return this.Ok();
            }
            catch (Exception)
            {
                return this.NotFound();
            }
        }

        /// <summary>
        /// method <c>GetRecommendationByCountryCodeAndStateCode</c> provide basic travel 
        /// information about a specific state.
        /// </summary>
        /// <param name="countryCode">The code of country.</param>
        /// <param name="stateCode">The code of state.</param>
        /// <returns>The state's information.</returns>
        [HttpGet]
        [Route("country/{countryCode}/state/{stateCode}")]
        public async Task<IActionResult> GetRecommendationByCountryCodeAndStateCode(
            [FromRoute] string countryCode, 
            [FromRoute] string stateCode)
        {
            try
            {
                Recommendation stateInfo = await this.recommendationPort.GetLocationInfoAsync(
                                                                         new Location(Country.Wrap(countryCode), State.Wrap(stateCode)),
                                                                         UserId.Wrap("testUser"));
                return this.Ok(stateInfo);
            }
            catch (Exception e)
            {
                return this.NotFound(e.Message);
            }
        }
    }
}
