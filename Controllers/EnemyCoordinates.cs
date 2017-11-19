using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;

namespace smartdestroyer.Controllers
{
    public class EnemyCoordinates
    {
        public double Y { get; set; }
        public double X { get; set; }
    }
}