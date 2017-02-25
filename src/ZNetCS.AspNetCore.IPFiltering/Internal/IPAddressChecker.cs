﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IPAddressChecker.cs" company="Marcin Smółka zNET Computer Solutions">
//   Copyright (c) Marcin Smółka zNET Computer Solutions. All rights reserved.
// </copyright>
// <summary>
//   The IP address checker.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ZNetCS.AspNetCore.IPFiltering.Internal
{
    #region Usings

    using System.Linq;
    using System.Net;

    using NetTools;

    #endregion

    /// <summary>
    /// The IP address checker.
    /// </summary>
    public class IPAddressChecker : IIPAddressChecker
    {
        #region Implemented Interfaces

        #region IIPAddressChecker

        /// <inheritdoc />
        public virtual bool IsAllowed(IPAddress ipAddress, IPFilteringOptions options)
        {
            var whitelist = options.Whitelist.Select(IPAddressRange.Parse).ToList();
            var blacklist = options.Blacklist.Select(IPAddressRange.Parse).ToList();

            switch (options.DefaultBlockLevel)
            {
                case DefaultBlockLevel.All:
                    return whitelist.Any(r => r.Contains(ipAddress)) && !blacklist.Any(r => r.Contains(ipAddress));

                case DefaultBlockLevel.None:
                    return !blacklist.Any(r => r.Contains(ipAddress)) || whitelist.Any(r => r.Contains(ipAddress));
            }

            return options.DefaultBlockLevel == DefaultBlockLevel.None;
        }

        #endregion

        #endregion
    }
}