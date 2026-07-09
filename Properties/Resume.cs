using Newtonsoft.Json;

namespace ResumeApi;

public class Resume
{
    public string id { get; set; }
    public string name { get; set; }
    public string label { get; set; }
    public string about { get; set; }
    public string email { get; set; }
    public Location location { get; set; }
    public List<Profile> profiles { get; set; }
    public List<Education> education { get; set; }
    public List<Experience> experience { get; set; }
    public Dictionary<string, List<string>> skills { get; set; }
    public int visitor_count { get; set; }
}

public class Location
{
    public string city { get; set; }
    public string countryCode { get; set; }
    public string region { get; set; }
}

public class Profile
{
    public string network { get; set; }
    public string username { get; set; }
    public string url { get; set; }
}

public class Education
{
    public string institution { get; set; }
    public string startDate { get; set; }
    public string endDate { get; set; }
    public string area { get; set; }
}

public class Experience
{
    public string company { get; set; }
    public string position { get; set; }
    public string location { get; set; }
    public string startDate { get; set; }
    public string endDate { get; set; }
    public string summary { get; set; }
    public List<string> highlights { get; set; }
}