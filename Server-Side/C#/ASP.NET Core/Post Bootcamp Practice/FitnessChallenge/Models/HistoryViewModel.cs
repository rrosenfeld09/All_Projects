using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System;
using FitnessChallenge.Models;
using System.Collections.Generic;

namespace FitnessChallenge.Models
{
    public class HistoryViewModel
    {
        public List<Log> logs {get; set;}
    }
}