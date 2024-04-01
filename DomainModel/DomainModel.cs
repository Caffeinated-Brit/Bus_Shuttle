namespace DomainModel;

public class DomainModel
{
    public class Bus
    {
        public int Id { get; set; }
        public int BusNumber { get; set; }

        public Bus(int id, int busNumber)
        {
            Id = id;
            BusNumber = busNumber;
        }

        public void Update(int busNumber)
        {
            BusNumber = busNumber;
        }
    }
    
    public class Entry
    {
        public int Id { get; set; }
        public DateTime TimeStamp { get; set; }
        public int Boarded { get; set; }
        public int LeftBehind { get; set; }

        public Entry(int id, DateTime timestamp, int boarded, int leftbehind)
        {
            Id = id;
            TimeStamp = timestamp;
            Boarded = boarded;
            LeftBehind = leftbehind;
        }

        public void Update(DateTime timestamp, int boarded, int leftbehind)
        {
            TimeStamp = timestamp;
            Boarded = boarded;
            LeftBehind = leftbehind;
        }
    }
    
    public class Loop
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Loop(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public void Update(string name)
        {
            Name = name;
        }
    }
    
    public class Route
    {
        public int Id { get; set; }
        public int Order { get; set; }

        public Route(int id, int order)
        {
            Id = id;
            Order = order;
        }

        public void Update(int order)
        {
            Order = order;
        }
    }
    
    public class Stop
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public Stop(int id, string name, double latitude, double longitude)
        {
            Id = id;
            Name = name;
            Latitude = latitude;
            Longitude = longitude;
        }

        public void Update(string name, double latitude, double longitude)
        {
            Name = name;
            Latitude = latitude;
            Longitude = longitude;
        }
    }
    
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsManager { get; set; }
        public bool IsDriver { get; set; }
        public bool IsAuthorizedDriver { get; set; }
        
        public User()
        {
        }
        public User(int id, string firstname, string lastname, string username, string password)
        {
            Id = id;
            FirstName = firstname;
            LastName = lastname;
            UserName = username;
            Password = password;
            IsManager = false;
            IsDriver = false;
            IsAuthorizedDriver = false;
        }

        public void Update(string firstname, string lastname, string username, string password, bool isManager, bool isDriver, bool isAuthorizedDriver)
        {
            FirstName = firstname;
            LastName = lastname;
            UserName = username;
            Password = password;
            IsManager = isManager;
            IsDriver = isDriver;
            IsAuthorizedDriver = isAuthorizedDriver;
        }
    }
    
    
    
    
    
    
}