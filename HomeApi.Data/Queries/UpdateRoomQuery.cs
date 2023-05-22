namespace HomeApi.Data.Queries
{
    public class UpdateRoomQuery
    {
        public string NewName { get; set; }
      
        public UpdateRoomQuery (string newName = null)
        {
            NewName = newName;
        }
    }
}
