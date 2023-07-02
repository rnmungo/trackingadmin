﻿namespace TrackingAdmin.ViewModels
{
    public class LocationResponseViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public float Latitude { get; set; }
        public float Longitude { get; set; }
    }
}
