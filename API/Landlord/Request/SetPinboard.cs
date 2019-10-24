namespace API.Landlord.Request
{
    public class SetPinboard
    {
        //  Properties
        //  ==========

        public int HouseId { get; set; }
        public string Text { get; set; } = null!;
    }
}
