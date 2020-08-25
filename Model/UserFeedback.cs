namespace DaprDemoAPI.Model
{
    public class UserFeedback
    {
        public string Id { get; set; }        
        public string FirstName { get; set; }        
        public string LastName { get; set; }
        public bool DoesLikeSession { get; set; } = true;       
        public string Message { get; set; }
        public string EmailId { get; set; }
    }
}
