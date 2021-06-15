using System.Collections.Generic;

public class AddressComponent
{
    public string long_name { get; set; }
    public string short_name { get; set; }
    public List<string> types { get; set; }
}

public class Location
{
    public double lat { get; set; }
    public double lng { get; set; }
}

public class Northeast
{
    public double lat { get; set; }
    public double lng { get; set; }
}

public class Southwest
{
    public double lat { get; set; }
    public double lng { get; set; }
}

public class Viewport
{
    public Northeast northeast { get; set; }
    public Southwest southwest { get; set; }
}

public class Geometry
{
    public Location location { get; set; }
    public string location_type { get; set; }
    public Viewport viewport { get; set; }
}

public class PlusCode
{
    public string compound_code { get; set; }
    public string global_code { get; set; }
}

public class Result
{
    public List<AddressComponent> address_components { get; set; }
    public string formatted_address { get; set; }
    public Geometry geometry { get; set; }
    public string place_id { get; set; }
    public PlusCode plus_code { get; set; }
    public List<string> types { get; set; }
}

public class GeoCodingData
{
    public List<Result> results { get; set; }
    public string status { get; set; }
}
public class Distance
{
    public string text { get; set; }
    public int value { get; set; }
}

public class Duration
{
    public string text { get; set; }
    public int value { get; set; }
}

public class Element
{
    public Distance distance { get; set; }
    public Duration duration { get; set; }
    public string status { get; set; }
}

public class Row
{
    public List<Element> elements { get; set; }
}

public class GeoDistanceData
{
    public List<string> destination_addresses { get; set; }
    public List<string> origin_addresses { get; set; }
    public List<Row> rows { get; set; }
    public string status { get; set; }
}

public class GeoLocationData
{
    public Location location { get; set; }
    public int accuracy { get; set; }
}

