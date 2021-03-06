﻿using System;

namespace Services.DTOs
{
    public class BranchResponseDTO : ResponseBaseDTO
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string BookingLink { get; set; }
        public string SocialLink { get; set; }
        public string AddressText { get; set; }
        public string AddressLink { get; set; }
        public int MainPhotoId { get; set; }
    }

    public class BranchRequestDTO
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string BookingLink { get; set; }
        public string SocialLink { get; set; }
        public string AddressLink { get; set; }
        public string StreetName { get; set; }
        public string District { get; set; }
    }
}