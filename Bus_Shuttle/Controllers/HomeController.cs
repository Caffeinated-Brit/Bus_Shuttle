using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Bus_Shuttle.Models;
using DomainModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Bus_Shuttle.Service;

namespace Bus_Shuttle.Controllers;


[Authorize]
public class HomeController : Controller
{
    
    //TODO: go through and verify httppost and httpget and ValidateAntiForgeryToken are on the right things
    
    private readonly ILogger<HomeController> _logger;
    private readonly IUserService _userService;
    private readonly IBusService _busService;
    private readonly ILoopService _loopService;
    private readonly EntryServiceInterface _entryService;
    private readonly IRouteService _routeService;
    private readonly IStopService _stopService;

    public HomeController(ILogger<HomeController> logger, IBusService busService, ILoopService loopService, EntryServiceInterface entryService, IRouteService routeService, IStopService stopService, IUserService userService)
    {
        _logger = logger;
        _busService = busService;
        _loopService = loopService;
        _entryService = entryService;
        _routeService = routeService;
        _stopService = stopService;
        _userService = userService;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult AuthDrivers()
    {
        var drivers = _userService.GetUnauthorizedDrivers();
        return View(drivers);
    }

    
    public IActionResult ViewDrivers()
    {
        var drivers = _userService.GetDrivers();
        return View(drivers);
    }
    
    
    [HttpPost]
    public IActionResult SetAuthorized(int userId)
    {
        _userService.SetAuthorized(userId);
        return RedirectToAction("AuthDrivers");
    }
    
    
    public IActionResult BusView()
    {
        return View(_busService.GetBusses().Select(b => BusModels.BusViewModel.FromBus(b)));
    }
    
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult BusDelete(int id)
    {
        if (ModelState.IsValid)
        {
            _busService.DeleteBus(id);
            return RedirectToAction("BusView");
        }
        return RedirectToAction("BusView");
        
    }
    
    
    public IActionResult BusEdit([FromRoute] int id)
    {
        var bus = _busService.FindBusByID(id);
        var busEditModel = BusModels.BusEditModel.FromBus(bus);
        return View(busEditModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> BusEdit(int id, [Bind("BusNumber")] BusModels.BusEditModel bus)
    {
        if (ModelState.IsValid)
        {
            _busService.UpdateBusByID(id, bus.BusNumber);
            return RedirectToAction("BusView");
        }
        return View(bus);
        
    }

    public IActionResult BusCreate()
    {
        return View();
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> BusCreate([Bind("BusNumber")] BusModels.BusCreateModel bus)
    {
        if (ModelState.IsValid)
        {
            _busService.CreateBus(bus.BusNumber);
            return RedirectToAction("BusView");
        }
        return View();
        
    }

    public IActionResult EntryView()
    {

        return View(_entryService.GetEntries().Select(e => EntryModels.EntryViewModel.FromEntry(e)));

    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult EntryDelete(int id)
    {
        if (ModelState.IsValid)
        {
            _entryService.DeleteEntry(id);
            return RedirectToAction("EntryView");
        }
        return RedirectToAction("EntryView");
        
    }

    public IActionResult EntryEdit([FromRoute] int id)
    {
        var entry = _entryService.FindEntryByID(id);
        var entryEditModel = EntryModels.EntryEditModel.FromEntry(entry);
        return View(entryEditModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EntryEdit(int id, [Bind("TimeStamp, Boarded, LeftBehind")] EntryModels.EntryEditModel entry)
    {
        if (ModelState.IsValid)
        {
            _entryService.UpdateEntryByID(id, entry.TimeStamp, entry.Boarded, entry.LeftBehind);
            return RedirectToAction("EntryView");
        }
        return View(entry);
    }

    public IActionResult EntryCreate()
    {
        return View();
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EntryCreate([Bind("TimeStamp, Boarded, LeftBehind")] EntryModels.EntryCreateModel entry)
    {
        if (ModelState.IsValid)
        {
            _entryService.CreateEntry(entry.TimeStamp, entry.Boarded, entry.LeftBehind);
            return RedirectToAction("EntryView");
        }
        return View();
        
    }

    //Loop
    public IActionResult LoopView()
    {

        return View(_loopService.GetLoops().Select(l => LoopModels.LoopViewModel.FromLoop(l)));

    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult LoopDelete(int id)
    {
        if (ModelState.IsValid)
        {
            _loopService.DeleteLoop(id);
            return RedirectToAction("LoopView");
        }
        return RedirectToAction("LoopView");
        
    }
    public IActionResult LoopEdit([FromRoute] int id)
    {
        var loop = _loopService.FindLoopByID(id);
        var loopEditModel = LoopModels.LoopEditModel.FromLoop(loop);
        return View(loopEditModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> LoopEdit(int id, [Bind("Name")] LoopModels.LoopEditModel loop)
    {
        if (ModelState.IsValid)
        {
            _loopService.UpdateLoopByID(id, loop.Name);
            return RedirectToAction("LoopView");
        }
        return View(loop);
        
    }

    public IActionResult LoopCreate()
    {
        return View();
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> LoopCreate([Bind("Name")] LoopModels.LoopCreateModel loop)
    {
        if (ModelState.IsValid)
        {
            _loopService.CreateLoop(loop.Name);
            return RedirectToAction("LoopView");
        }
        return View();
        
    }



    //Route

    public IActionResult RouteView()
    {

        return View(_routeService.GetRoutes().Select(r => RouteModels.RouteViewModel.FromRoute(r)));

    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult RouteDelete(int id)
    {
        if (ModelState.IsValid)
        {
            _routeService.DeleteRoute(id);
            return RedirectToAction("RouteView");
        }
        return RedirectToAction("RouteView");
        
    }
    public IActionResult RouteEdit([FromRoute] int id)
    {
        var route = _routeService.FindRouteByID(id);
        var routeEditModel = RouteModels.RouteEditModel.FromRoute(route);
        return View(routeEditModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> RouteEdit(int id, [Bind("Order")] RouteModels.RouteEditModel route)
    {
        if (ModelState.IsValid)
        {
            _routeService.UpdateRouteByID(id, route.Order);
            return RedirectToAction("RouteView");
        }

        return View(route);
        
    }

    public IActionResult RouteCreate()
    {
        return View();
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> RouteCreate([Bind("Order")] RouteModels.RouteCreateModel route)
    {
        if (ModelState.IsValid)
        {
            _routeService.CreateRoute(route.Order);
            return RedirectToAction("RouteView");
        }
        return View();
    }


    //Stop

    public IActionResult StopView()
    {

        return View(_stopService.GetStops().Select(s => StopModels.StopViewModel.FromStop(s)));

    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult StopDelete(int id)
    {
        if (ModelState.IsValid)
        {
            _stopService.DeleteStop(id);
            return RedirectToAction("StopView");
        }
        return RedirectToAction("StopView");
        
    }
    public IActionResult StopEdit([FromRoute] int id)
    {
        var stop = _stopService.FindStopByID(id);
        var stopEditModel = StopModels.StopEditModel.FromStop(stop);
        return View(stopEditModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> StopEdit(int id, [Bind("Name, Latitude, Longitude")] StopModels.StopEditModel stop)
    {
        if (ModelState.IsValid)
        {
            _stopService.UpdateStopByID(id, stop.Name, stop.Latitude, stop.Longitude);
            return RedirectToAction("StopView");
        }
        return View(stop);
        
    }

    public IActionResult StopCreate()
    {
        return View();
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> StopCreate([Bind("Name, Latitude, Longitude")] StopModels.StopCreateModel stop)
    {
        if (ModelState.IsValid)
        {
            _stopService.CreateStop(stop.Name, stop.Latitude, stop.Longitude);
            return RedirectToAction("StopView");
        }
        return View();

    }

    //Login
    
    [AllowAnonymous]
    [HttpGet]
    public IActionResult LoginView()
        {
            return View();
        }

    
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
    }
    
    
    [AllowAnonymous]
    public IActionResult SignUpView()
    {
        return View();
    }

    [AllowAnonymous]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SignUpView([Bind("FirstName, LastName, UserName, Password")] DomainModel.DomainModel.User user)
    {
        if (ModelState.IsValid)
        {
            this._userService.CreateUser(user.FirstName, user.LastName, user.UserName, user.Password);
            return RedirectToAction("Index");
        }
        return View();

    }
}
