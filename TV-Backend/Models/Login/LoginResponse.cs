namespace TV_Backend.Models.Login
{
    public class LoginResponse
    {
        public Header header { get; set; }
        public Body body { get; set; }
    }

    public class Header
    {
        public string requestId { get; set; }
        public bool success { get; set; }
        public List<Message> messages { get; set; }
    }

    public class Message
    {
        public long id { get; set; }
        public string code { get; set; }
        public int messageType { get; set; }
        public string message { get; set; }
    }

    public class Body
    {
        public string token { get; set; }
        public string expiresOn { get; set; }
        public long tokenId { get; set; }
        public UserInfo userInfo { get; set; }
    }

    public class UserInfo
    {
        public string code { get; set; }
        public string name { get; set; }
        public MainAgency mainAgency { get; set; }
        public Agency agency { get; set; }
        public Office office { get; set; }
        public Operator operatorInfo { get; set; }
        public Market market { get; set; }
    }

    public class MainAgency
    {
        public string code { get; set; }
        public string name { get; set; }
        public string registerCode { get; set; }
    }

    public class Agency
    {
        public string code { get; set; }
        public string name { get; set; }
        public string registerCode { get; set; }
    }

    public class Office
    {
        public string code { get; set; }
        public string name { get; set; }
    }

    public class Operator
    {
        public string code { get; set; }
        public string name { get; set; }
        public string thumbnail { get; set; }
    }

    public class Market
    {
        public string code { get; set; }
        public string name { get; set; }
        public string favicon { get; set; }
    }
}
