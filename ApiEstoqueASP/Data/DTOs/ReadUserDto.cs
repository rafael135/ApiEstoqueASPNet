namespace ApiEstoqueASP.Data.DTOs
{
    public class ReadUserDto
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }

        [AutoMapper.Configuration.Annotations.Ignore]
        public string Token { get; set; }
    }
}
