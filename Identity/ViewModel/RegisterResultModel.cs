namespace Identity.ViewModel
{
    public class RegisterResultModel
    {
        public bool IsSuccess { get; set; }
        public string ConfirmEmailToken { get; set; }
        public string UserId { get; set; }
    }
}
