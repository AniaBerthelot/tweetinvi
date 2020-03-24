﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Tweetinvi.Parameters
{
    /// <summary>
    /// Allow developers to enhance default requests with additional query parameters
    /// </summary>
    public interface ICustomRequestParameters
    {
        /// <summary>
        /// Collection of custom query parameters.
        /// </summary>
        List<Tuple<string, string>> CustomQueryParameters { get; }

        /// <summary>
        /// Formatted string containing all the query parameters to append to a query.
        /// </summary>
        string FormattedCustomQueryParameters { get; }

        /// <summary>
        /// Add a custom query parameter.
        /// </summary>
        void AddCustomQueryParameter(string name, string value);

        /// <summary>
        /// Clear the query parameters of the query.
        /// </summary>
        void ClearCustomQueryParameters();
    }

    public class CustomRequestParameters : ICustomRequestParameters
    {
        private readonly List<Tuple<string, string>> _customQueryParameters;

        public CustomRequestParameters()
        {
            _customQueryParameters = new List<Tuple<string, string>>();
        }

        public CustomRequestParameters(ICustomRequestParameters parameters)
        {
            if (parameters?.CustomQueryParameters == null)
            {
                _customQueryParameters = new List<Tuple<string, string>>();
                return;
            }

            _customQueryParameters = parameters.CustomQueryParameters;
        }

        /// <inheritdoc/>
        public void AddCustomQueryParameter(string name, string value)
        {
            _customQueryParameters.Add(new Tuple<string, string>(name, value));
        }

        /// <inheritdoc/>
        public void ClearCustomQueryParameters()
        {
            _customQueryParameters.Clear();
        }

        /// <inheritdoc/>
        public List<Tuple<string, string>> CustomQueryParameters => _customQueryParameters;

        /// <inheritdoc/>
        public string FormattedCustomQueryParameters
        {
            get
            {
                if (_customQueryParameters.Count == 0)
                {
                    return string.Empty;
                }

                var queryParameters = new StringBuilder($"{_customQueryParameters[0].Item1}={_customQueryParameters[0].Item2}");

                for (int i = 1; i < _customQueryParameters.Count; ++i)
                {
                    queryParameters.Append($"&{_customQueryParameters[i].Item1}={_customQueryParameters[i].Item2}");
                }

                return queryParameters.ToString();
            }
        }
    }
}