/* Options:
Date: 2015-01-25 18:34:56
Version: 1
BaseUrl: http://localhost:13011

//MakePartial: True
//MakeVirtual: True
//MakeDataContractsExtensible: False
//AddReturnMarker: True
//AddDescriptionAsComments: True
//AddDataContractAttributes: False
//AddIndexesToDataMembers: False
//AddResponseStatus: False
//AddImplicitVersion: 
//InitializeCollections: True
//AddDefaultXmlNamespace: http://schemas.servicestack.net/types
*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using ServiceStack;
using ServiceStack.DataAnnotations;
using Google.Maps.Direction;
using Google.Maps;
using Hitchhiker.ServiceModel;


namespace Google.Maps
{

    public partial class LatLng
        : Location
    {
        public virtual double Latitude { get; set; }
        public virtual double Longitude { get; set; }
    }

    public partial class Location
    {
    }

    public enum TravelMode
    {
        driving,
        walking,
        bicycling,
    }

    public partial class ValueText
    {
        public virtual string Value { get; set; }
        public virtual string Text { get; set; }
    }
}

namespace Google.Maps.Direction
{

    public partial class DirectionLeg
    {
        public DirectionLeg()
        {
            Steps = new DirectionStep[]{};
        }

        public virtual DirectionStep[] Steps { get; set; }
        public virtual ValueText Duration { get; set; }
        public virtual ValueText Distance { get; set; }
        public virtual LatLng StartLocation { get; set; }
        public virtual LatLng EndLocation { get; set; }
        public virtual string StartAddress { get; set; }
        public virtual string EndAddress { get; set; }
    }

    public partial class DirectionRoute
    {
        public DirectionRoute()
        {
            Legs = new DirectionLeg[]{};
        }

        public virtual string Summary { get; set; }
        public virtual DirectionLeg[] Legs { get; set; }
        public virtual string Copyrights { get; set; }
        public virtual Polyline OverviewPolyline { get; set; }
    }

    public partial class DirectionStep
    {
        public virtual TravelMode TravelMode { get; set; }
        public virtual LatLng StartLocation { get; set; }
        public virtual LatLng EndLocation { get; set; }
        public virtual Polyline Polyline { get; set; }
        public virtual ValueText Duration { get; set; }
        public virtual string Maneuver { get; set; }
        public virtual string HtmlInstructions { get; set; }
        public virtual ValueText Distance { get; set; }
    }

    public partial class Polyline
    {
        public virtual string Points { get; set; }
        public virtual string Levels { get; set; }
    }
}

namespace Hitchhiker.ServiceModel
{

    [Route("/hello/{Name}")]
    public partial class Hello
        : IReturn<HelloResponse>
    {
        public virtual string Name { get; set; }
    }

    public partial class HelloResponse
    {
        public virtual string Result { get; set; }
    }

    [Route("/route")]
    public partial class Route
        : IReturn<RouteResponse>
    {
        public virtual string Origination { get; set; }
        public virtual string Destination { get; set; }
    }

    public partial class RouteResponse
    {
        public virtual DirectionRoute Route { get; set; }
    }

    [Route("/secure/{Name}")]
    public partial class Secure
        : IReturn<SecureResponse>
    {
        public virtual string Name { get; set; }
    }

    public partial class SecureResponse
    {
        public virtual string Name { get; set; }
    }
}

