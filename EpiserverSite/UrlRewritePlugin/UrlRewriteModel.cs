﻿using EPiServer.Data;
using EPiServer.Data.Dynamic;
using System;

namespace EpiserverSite.UrlRewritePlugin
{
    public class UrlRewriteModel : IDynamicData
    {
        public Identity Id { get; set; }
        
        public string OldUrl { get; set; }

        public int ContextId { get; set; }
    }
}