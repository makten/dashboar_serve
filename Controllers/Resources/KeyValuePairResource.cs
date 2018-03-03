using AutoMapper;

namespace dashboard.Controllers.Resources
{
    //Used to map resource with only Id and Name fields
    public class KeyValuePairResource
    {
        
        public int Id { get; set; } 
        
        public string Name { get; set; }
    }
}