namespace Bus_Shuttle.Models;

public class LoopModels
{
    public class LoopViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public static LoopViewModel FromLoop(DomainModel.DomainModel.Loop loop)
        {
            return new LoopViewModel
            {
                Id = loop.Id,
                Name = loop.Name
            };
        }
    }
    
    public class LoopCreateModel
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
    
    public class LoopEditModel
    {
        public int Id { get; set; }
        public string Name { get; set; }


        public static LoopEditModel FromLoop(DomainModel.DomainModel.Loop loop)
        {
            return new LoopEditModel
            {
                Id = loop.Id,
                Name = loop.Name
            };
        }
    }
}