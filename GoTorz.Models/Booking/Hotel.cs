﻿namespace GoTorz.Models.Booking;

public class Hotel
{
    public int HotelId { get; set; }

    public string Name { get; set; }

    public string Address { get; set; }

    public string City { get; set; }

    public string Country { get; set; }

    public int StarRating { get; set; }

    public decimal PricePerNight { get; set; }
}
