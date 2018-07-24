using Microsoft.AspNetCore.Mvc;

namespace DojoSurvey.Models
{
    public class User
    {
        public string Name {get; set;}
        public string Location {get; set;}
        public string Language {get; set;}
        public string Comment {get; set;}

    }
}